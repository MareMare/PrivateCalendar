using System.Text;

namespace CompanyCalendar.Exporter.Ics
{
    public class ExporterOptions
    {
        public static readonly string Key = "IcsExporter";

        public string FilePath { get; set; } = null!;

        public Encoding FileEncoding { get; set; } = Encoding.UTF8;
    }
}