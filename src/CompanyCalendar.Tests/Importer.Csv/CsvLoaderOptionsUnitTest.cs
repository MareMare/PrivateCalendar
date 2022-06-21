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
            Assert.Null(actual.FilePath);
            Assert.Equal(Encoding.UTF8, actual.FileEncoding);
        }

        [Fact]
        public void CsvLoaderOptions_Ctor_Test2()
        {
            var actual = new CsvLoaderOptions { FilePath = "a", FileEncoding = Encoding.UTF8 };
            Assert.Equal("a", actual.FilePath);
            Assert.Equal(Encoding.UTF8, actual.FileEncoding);
        }
    }
}