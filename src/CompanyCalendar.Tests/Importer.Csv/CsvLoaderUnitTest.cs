using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CompanyCalendar.Importer.Csv;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Abstractions;

namespace CompanyCalendar.Tests.Importer.Csv
{
    public class CsvLoaderUnitTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CsvLoaderUnitTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task LoadAsync_ValidFileTest()
        {
            var options = Options.Create(new CsvLoaderOptions { FilePath = "Sample.csv" });
            var loader = new CsvLoader(options);

            var items = new List<HolidayItem>();
            await foreach (var item in loader.LoadAsync().ConfigureAwait(false))
            {
                Assert.NotNull(item);
                Assert.NotNull(item.Summary);
                items.Add(item);
            }

            Assert.NotEmpty(items);
            Assert.Equal(129, items.Count);
        }

        public static IEnumerable<object?[]> LoadAsync_Range_TestDate()
        {
            yield return new object?[] { DateTime.Parse("2023/03/04"), null, 9, };
            yield return new object?[] { null, DateTime.Parse("2022/4/30"), 10, };
            yield return new object?[] { DateTime.Parse("2022/6/6"), DateTime.Parse("2022/6/6"), 1, };
        }

        [Theory]
        [MemberData(nameof(LoadAsync_Range_TestDate))]
        public async Task LoadAsync_Range_Test(DateTime? lowerDate, DateTime? upperDate, int? expected)
        {
            var options = Options.Create(new CsvLoaderOptions { FilePath = "Sample.csv" });
            var loader = new CsvLoader(options);
            var items = await loader.LoadAsync(lowerDate, upperDate).ToListAsync().ConfigureAwait(false);

            Assert.Equal(expected, items.Count);
        }

        [Fact]
        public async Task LoadAsync_NotExistsFileTest1()
        {
            var options = Options.Create(new CsvLoaderOptions { FilePath = "not_found.csv" });
            var loader = new CsvLoader(options);

            var e = loader.LoadAsync().GetAsyncEnumerator();
            var _ = await Assert.ThrowsAsync<FileNotFoundException>(() => e.MoveNextAsync().AsTask());
        }

        [Fact]
        public async Task LoadAsync_NotExistsFileTest2()
        {
            var options = Options.Create(new CsvLoaderOptions { FilePath = "not_found.csv" });
            var loader = new CsvLoader(options);
            var _ = await Assert.ThrowsAsync<FileNotFoundException>(async () => await loader.LoadAsync().FirstAsync().ConfigureAwait(false));
        }

        [Fact]
        public async Task Sandbox1()
        {
            var options = Options.Create(new CsvLoaderOptions { FilePath = "Sample.csv" });
            var loader = new CsvLoader(options);

            var loadedItemsMapping = await loader.LoadAsync().ToDictionaryAsync(item => item.Date).ConfigureAwait(false);
            var loadedItems = loadedItemsMapping.Values;

            var minDateItem = loadedItems.MinBy(item => item.Date);
            var maxDateItem = loadedItems.MaxBy(item => item.Date);
            var lowerDate = minDateItem?.Date.ToFirstDayInMonth();
            var upperDate = maxDateItem?.Date.ToLastDayInMonth();

            Assert.Equal(DateTime.Parse("2022/04/01"), lowerDate);
            Assert.Equal(DateTime.Parse("2023/03/31"), upperDate);

            foreach (var calendar in lowerDate.YieldDates(upperDate))
            {
                (bool isIrregular, HolidayKind? kind, string? summary) = IsIrregular(calendar);
                if (isIrregular)
                {
                    _testOutputHelper.WriteLine($"date={calendar:yyyy/MM/dd} kind={kind} summary={summary}");
                    Assert.NotNull(kind);
                }
            }

            (bool isIrregular, HolidayKind? kind, string? summary) IsIrregular(DateTime calendar)
            {
                // ----------------------------------------
                // ƒJƒŒƒ“ƒ_   ‹x“úÝ’è  ”»’è      •s’èŠúH
                // ----------------------------------------
                // •½“ú       ‚È‚µ      o‹Î“ú    ‚¢‚¢‚¦
                // •½“ú       ‹x“ú      ‹x“ú      ‚Í‚¢
                // •½“ú       j“ú      ‹x“ú      ‚¢‚¢‚¦
                // •½“ú       —L‹‹      ‹x“ú      ‚¢‚¢‚¦
                // ----------------------------------------
                // T––       ‚È‚µ      o‹Î“ú    ‚Í‚¢
                // T––       ‚ ‚è      ‹x“ú      ‚¢‚¢‚¦
                // ----------------------------------------
                var isWeekend = calendar.IsWeekend();
                var holidayItem = loadedItemsMapping.TryGetValue(calendar, out var item) ? item : null;

                if (!isWeekend)
                {
                    if (holidayItem?.Kind == HolidayKind.Kyujitsu)
                    {
                        return (true, holidayItem.Kind, holidayItem.Summary);
                    }
                }
                else
                {
                    if (holidayItem == null)
                    {
                        return (true, HolidayKind.Shukkimbi, null);
                    }
                }

                return (false, null, null);
            }
        }

        [Fact]
        public async Task Sandbox2()
        {
            var options = Options.Create(new CsvLoaderOptions { FilePath = "Sample.csv" });
            var loader = new CsvLoader(options);

            var loadedItemsMapping = await loader.LoadAsync().ToDictionaryAsync(item => item.Date).ConfigureAwait(false);

            var loadedItems = loadedItemsMapping.Values;
            var minDateItem = loadedItems.MinBy(item => item.Date);
            var maxDateItem = loadedItems.MaxBy(item => item.Date);
            var lowerDate = minDateItem?.Date.ToFirstDayInMonth();
            var upperDate = maxDateItem?.Date.ToLastDayInMonth();

            var query = lowerDate.YieldDates(upperDate)
                .Select(calendar =>
                {
                    var holidayItem = loadedItemsMapping.TryGetValue(calendar, out var item) ? item : null;
                    var irregularKind = IsIrregular(calendar, holidayItem);
                    return new
                    {
                        Date = calendar,
                        IsWeekend = calendar.IsWeekend(),
                        holidayItem?.Kind,
                        IrregularKind = irregularKind,
                    };
                })
                .Where(pair => pair.IrregularKind != null)
                .ToArray();
            foreach (var item in query)
            {
                _testOutputHelper.WriteLine($"Date={item.Date:yyyy/MM/dd} IsWeekend={item.IsWeekend} Kind={item.Kind} IrregularKind={item.IrregularKind}");
            }

            static HolidayKind? IsIrregular(DateTime calendar, HolidayItem? holidayItem)
            {
                // ----------------------------------------
                // ƒJƒŒƒ“ƒ_   ‹x“úÝ’è  ”»’è      •s’èŠúH
                // ----------------------------------------
                // •½“ú       ‚È‚µ      o‹Î“ú    ‚¢‚¢‚¦
                // •½“ú       ‹x“ú      ‹x“ú      ‚Í‚¢
                // •½“ú       j“ú      ‹x“ú      ‚¢‚¢‚¦
                // •½“ú       —L‹‹      ‹x“ú      ‚Í‚¢
                // ----------------------------------------
                // T––       ‚È‚µ      o‹Î“ú    ‚Í‚¢
                // T––       ‚ ‚è      ‹x“ú      ‚¢‚¢‚¦
                // ----------------------------------------
                var isWeekend = calendar.IsWeekend();
                if (!isWeekend)
                {
                    return holidayItem?.Kind switch
                    {
                        HolidayKind.Kyujitsu => HolidayKind.Kyujitsu,
                        HolidayKind.YukyuKijumbi => HolidayKind.YukyuKijumbi,
                        _ => null,
                    };
                }

                if (holidayItem == null)
                {
                    return HolidayKind.Shukkimbi;
                }

                return null;
            }
        }
    }
}