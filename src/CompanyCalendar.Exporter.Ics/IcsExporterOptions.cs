using System.Text;

namespace CompanyCalendar.Exporter.Ics
{
    public class IcsExporterOptions
    {
        public static readonly string Key = "IcsExporter";

        public Encoding FileEncoding { get; set; } = Encoding.UTF8;

        public string ProductId { get; set; } = null!;

        public string CalendarName { get; set; } = null!;
    }
}