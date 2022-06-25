using System.Collections.Generic;
using CompanyCalendar.Importer.Csv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CompanyCalendar.Tests.Importer.Csv
{
    public class ServiceCollectionExtensionsUnitTest
    {
        [Fact]
        public void AddCsvImporter_Test()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { $"{CsvLoaderOptions.Key}", null },
                })
                .Build();

            var services = new ServiceCollection().AddCsvImporter(configuration);
            using var serviceProvider = services.BuildServiceProvider();

            var actual = serviceProvider.GetService<ICsvLoader>();
            Assert.NotNull(actual);
            Assert.IsType<CsvLoader>(actual);
        }
    }
}