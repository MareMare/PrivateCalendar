using Microsoft.EntityFrameworkCore;

namespace CompanyCalendar.Importer.MsSql
{
    public class DbLoader : IDbLoader
    {
        private readonly AppDbContext _dbContext;

        public DbLoader(AppDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext);

            this._dbContext = dbContext;
        }

        public IAsyncEnumerable<HolidayItem> LoadAsync(
            DateTime? lowerDate = null,
            DateTime? upperDate = null,
            CancellationToken taskCancellationToken = default)
        {
            return this._dbContext.CompanyHolidays
                .AsNoTracking()
                .Where(item => !lowerDate.HasValue || item.Date >= lowerDate.Value)
                .Where(item => !upperDate.HasValue || item.Date <= upperDate.Value)
                .OrderBy(item => item.Date)
                .AsAsyncEnumerable();
        }
    }
}