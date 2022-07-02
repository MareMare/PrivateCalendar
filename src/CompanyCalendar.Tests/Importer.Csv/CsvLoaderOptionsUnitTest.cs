using System.Text;
using CompanyCalendar.Importer.Csv;
using Xunit;

namespace CompanyCalendar.Tests.Importer.Csv
{
    public class CsvLoaderOptionsUnitTest
    {
        [Fact]
        public void CsvLoaderOptions_Ctor_Test1()
        {
            var actual = new CsvLoaderOptions();
            Assert.Equal(Encoding.UTF8, actual.FileEncoding);
        }
    }
}