using System.Collections.Generic;
using CompanyCalendar.Exporter.Ics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CompanyCalendar.Tests.Exporter.Ics
{
    public class ServiceCollectionExtensionsUnitTest
    {
        [Fact]
        public void AddIcsExporter_Test()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { $"{IcsExporterOptions.Key}", null },
                })
                .Build();
            var services = new ServiceCollection().AddIcsExporter(configuration);
            using var serviceProvider = services.BuildServiceProvider();

            var actual = serviceProvider.GetService<IIcsExporter>();
            Assert.NotNull(actual);
            Assert.IsType<IcsExporter>(actual);
        }   
    }
}