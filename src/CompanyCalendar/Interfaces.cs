namespace CompanyCalendar
{
    public interface ICsvLoader
    {
        IAsyncEnumerable<HolidayItem> LoadAsync(
            string csvFilePath,
            DateTime? lowerDate = null,
            DateTime? upperDate = null,
            CancellationToken taskCancellationToken = default);
    }

    public interface IDbLoader
    {
        IAsyncEnumerable<HolidayItem> LoadAsync(
            DateTime? lowerDate = null,
            DateTime? upperDate = null,
            CancellationToken taskCancellationToken = default);
    }

    public interface IIcsExporter
    {
        Task ExportAsync(
            string icsFilePath,
            IEnumerable<(DateTime date, string summary)> eventPairs,
            CancellationToken taskCancellationToken = default);
    }

    public interface ICalendarExporter
    {
        Task ExportAsync(
            IEnumerable<(DateTime date, string summary)> eventPairs,
            CancellationToken taskCancellationToken = default);
    }
}