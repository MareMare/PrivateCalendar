namespace CompanyCalendar.Exporter.Ics
{
    public partial class IcsRuntimeTemplate
    {
        public string ProductId { get; set; } = null!;
        public string CalendarName { get; set; } = null!;
        public string Timestamp
        {
            get => $"{DateTime.Now.ToUniversalTime():yyyyMMddTHHmmssZ}";
        }
        public IList<(DateTime lowerDate, DateTime upperDate, string summary)> EventPairs { get; set; } = null!;
    }
}