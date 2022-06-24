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
            var path = "Sample.csv";
            var options = Options.Create(new CsvLoaderOptions());
            var loader = new CsvLoader(options);

            var items = new List<HolidayItem>();
            await foreach (var item in loader.LoadAsync(path).ConfigureAwait(false))
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
            var path = "Sample.csv";
            var options = Options.Create(new CsvLoaderOptions());
            var loader = new CsvLoader(options);
            var items = await loader.LoadAsync(path, lowerDate, upperDate).ToListAsync().ConfigureAwait(false);

            Assert.Equal(expected, items.Count);
        }

        [Fact]
        public async Task LoadAsync_NotExistsFileTest1()
        {
            var path = "not_found.csv";
            var options = Options.Create(new CsvLoaderOptions());
            var loader = new CsvLoader(options);

            var e = loader.LoadAsync(path).GetAsyncEnumerator();
            var _ = await Assert.ThrowsAsync<FileNotFoundException>(() => e.MoveNextAsync().AsTask());
        }

        [Fact]
        public async Task LoadAsync_NotExistsFileTest2()
        {
            var path = "not_found.csv";
            var options = Options.Create(new CsvLoaderOptions());
            var loader = new CsvLoader(options);
            var _ = await Assert.ThrowsAsync<FileNotFoundException>(async () => await loader.LoadAsync(path).FirstAsync().ConfigureAwait(false));
        }

        [Fact]
        public async Task Sandbox()
        {
            var path = "Sample.csv";
            var options = Options.Create(new CsvLoaderOptions());
            var loader = new CsvLoader(options);

            var loadedItemsMapping = await loader.LoadAsync(path).ToDictionaryAsync(item => item.Date).ConfigureAwait(false);
            var lowerDate = loadedItemsMapping.Keys.Min().ToFirstDayInMonth();
            var upperDate = loadedItemsMapping.Keys.Max().ToLastDayInMonth();

            Assert.Equal(DateTime.Parse("2022/04/01"), lowerDate);
            Assert.Equal(DateTime.Parse("2023/03/31"), upperDate);

            var query = loadedItemsMapping.ToIrregularPairs(lowerDate, upperDate);
            foreach (var pair in query)
            {
                _testOutputHelper.WriteLine($"Date={pair.Date:yyyy/MM/dd} IrregularKind={pair.IrregularKind}");
            }
        }
   }
}