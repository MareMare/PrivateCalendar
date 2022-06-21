using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyCalendar.Importer.MsSql;
using Xunit;
using Xunit.Abstractions;

namespace CompanyCalendar.Tests.Importer.MsSql
{
    public class DbLoaderUnitTest : AppDbContextUnitTestBase
    {
        public DbLoaderUnitTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        public static IEnumerable<object?[]> LoadAsync_Range_TestDate()
        {
            yield return new object?[] { DateTime.Parse("2022/06/23"), null, 1, };
            yield return new object?[] { null, DateTime.Parse("2022/6/20"), 1, };
            yield return new object?[] { DateTime.Parse("2022/6/21"), DateTime.Parse("2022/6/22"), 2, };
            yield return new object?[] { null, null, 4, };
        }

        [Theory]
        [MemberData(nameof(LoadAsync_Range_TestDate))]
        public async Task LoadAsync_Range_Test(DateTime? lowerDate, DateTime? upperDate, int? expected)
        {
            await using var context = CreateAppDbContext();
            var loader = new DbLoader(context);
            var items = await loader.LoadAsync(lowerDate, upperDate).ToListAsync().ConfigureAwait(false);

            Assert.Equal(expected, items.Count);
        }
    }
}