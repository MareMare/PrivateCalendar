using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace CompanyCalendar.Tests.Importer.MsSql
{
    public class AppDbContextUnitTest : AppDbContextUnitTestBase
    {
        public AppDbContextUnitTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task TestAsync()
        {
            await using var context = this.CreateAppDbContext();
            var items = await context.CompanyHolidays
                .AsNoTracking()
                .OrderBy(item => item.Date)
                .ToListAsync()
                .ConfigureAwait(false);

            Assert.NotNull(items);
            Assert.Equal(4, items.Count);
        }
    }
}