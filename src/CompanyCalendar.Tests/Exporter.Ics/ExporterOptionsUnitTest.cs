using System.Text;
using CompanyCalendar.Exporter.Ics;
using Xunit;

namespace CompanyCalendar.Tests.Exporter.Ics
{
    public class ExporterOptionsUnitTest
    {
        [Fact]
        public void ExporterOptions_Ctor_Test1()
        {
            var actual = new ExporterOptions();
            Assert.Null(actual.FilePath);
            Assert.Equal(Encoding.UTF8, actual.FileEncoding);
        }

        [Fact]
        public void ExporterOptions_Ctor_Test2()
        {
            var actual = new ExporterOptions { FilePath = "a", FileEncoding = Encoding.UTF8 };
            Assert.Equal("a", actual.FilePath);
            Assert.Equal(Encoding.UTF8, actual.FileEncoding);
        }
    }
}