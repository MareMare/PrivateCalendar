using System.Text;

namespace CompanyCalendar.Importer.Csv
{
    public class CsvLoaderOptions
    {
        public string FilePath { get; set; } = null!;

        public Encoding FileEncoding { get; set; } = Encoding.UTF8;
    }
}