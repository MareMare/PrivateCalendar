using System.Text;
using CompanyCalendar.Exporter.Ics;
using Xunit;

namespace CompanyCalendar.Tests.Exporter.Ics
{
    public class IcsExporterOptionsUnitTest
    {
        [Fact]
        public void ExporterOptions_Ctor_Test1()
        {
            var actual = new IcsExporterOptions();
            Assert.Equal(Encoding.UTF8, actual.FileEncoding);
            Assert.Null(actual.CalendarName);
            Assert.Null(actual.ProductId);
        }
    }
}