using System.Runtime.CompilerServices;

namespace CompanyCalendar
{
    public interface IHolidaysLoader
    {
        IAsyncEnumerable<HolidayItem> LoadAsync(DateTime? lowerDate = null, DateTime? upperDate = null, CancellationToken taskCancellationToken = default);
    }

    public interface ICalendarExporter
    {
        Task ExportAsync(IEnumerable<(DateTime date, string summary)> eventPairs, CancellationToken taskCancellationToken = default);
    }
}