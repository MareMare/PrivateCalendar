namespace CompanyCalendar.Exporter.Ics
{
    public partial class IcsRuntimeTemplate
    {
        private IList<(DateTime lowerDate, DateTime upperDate, string summary)> _eventPairs;
        public IcsRuntimeTemplate(IEnumerable<(DateTime lowerDate, DateTime upperDate, string summary)>? pairs = null)
        {
            this._eventPairs = (pairs ?? Array.Empty<(DateTime lowerDate, DateTime upperDate, string summary)>()).ToList();
            this.Timestamp = $"{DateTime.Now.ToUniversalTime():yyyyMMddTHHmmssZ}";
        }

        public string ProductId { get; set; } = null!;
        public string CalendarName { get; set; } = null!;
        public string Timestamp { get; }
        public IList<(DateTime lowerDate, DateTime upperDate, string summary)> EventPairs => this._eventPairs;
    }
}
