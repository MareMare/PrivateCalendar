using System.Collections.Generic;
using CompanyCalendar.Exporter.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CompanyCalendar.Tests.Exporter.Google
{
    public class ServiceCollectionExtensionsUnitTest
    {
        [Fact]
        public void AddGoogleExporter_Test()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { $"{CalendarExporterOptions.Key}", null },
                })
                .Build();

            var services = new ServiceCollection().AddGoogleExporter(configuration);
            using var serviceProvider = services.BuildServiceProvider();

            var actual = serviceProvider.GetService<ICalendarExporter>();
            Assert.NotNull(actual);
            Assert.IsType<CalendarExporter>(actual);
        }
    }
}