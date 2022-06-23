using System.Text;

namespace CompanyCalendar.Importer.Csv
{
    public class CsvLoaderOptions
    {
        public static readonly string Key = "CsvLoader";

        public Encoding FileEncoding { get; set; } = Encoding.UTF8;
    }
}