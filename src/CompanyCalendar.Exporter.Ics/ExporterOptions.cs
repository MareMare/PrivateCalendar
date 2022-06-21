using System.Text;

namespace CompanyCalendar.Exporter.Ics
{
    public class ExporterOptions
    {
        public string FilePath { get; set; } = null!;

        public Encoding FileEncoding { get; set; } = Encoding.UTF8;
    }
}