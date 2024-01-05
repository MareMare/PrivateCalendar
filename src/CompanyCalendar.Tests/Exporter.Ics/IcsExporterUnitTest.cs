using System;
using System.Threading.Tasks;
using CompanyCalendar.Exporter.Ics;
using Microsoft.Extensions.Options;
using Xunit;

namespace CompanyCalendar.Tests.Exporter.Ics
{
    public class IcsExporterUnitTest
    {
        [Fact]
        public async Task Test_Ics_ExportAsync()
        {
            var path = "sample.ics";
            var options = Options.Create(new IcsExporterOptions { CalendarName = "おためしカレンダー", ProductId = "おためし" });
            var exporter = new IcsExporter(options);
            var pairs = new[]
            {
                (DateTime.Today, "HSC でばっぐ１"),
                (DateTime.Today.ToNextDay(), "HSC デバッグ２"),
            };
            await exporter.ExportAsync(path, pairs).ConfigureAwait(true);
        }
    }
}
