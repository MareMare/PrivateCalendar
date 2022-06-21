using System;
using System.Threading.Tasks;
using CompanyCalendar.Exporter.Ics;
using Microsoft.Extensions.Options;
using Xunit;

namespace CompanyCalendar.Tests.Exporter.Ics
{
    public class CalendarExporterUnitTest
    {
        [Fact]
        public async Task Test_Ics_ExportAsync()
        {
            var options = Options.Create(new ExporterOptions { FilePath = "sample.ics" });
            var exporter = new CalendarExporter(options);
            var pairs = new[]
            {
                (DateTime.Today, "HSC でばっぐ１"),
                (DateTime.Today.ToNextDay(), "HSC デバッグ２"),
            };
            await exporter.ExportAsync(pairs).ConfigureAwait(false);
        }
    }
}