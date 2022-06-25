using System.Collections.Generic;
using CompanyCalendar.Importer.MsSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Xunit;

namespace CompanyCalendar.Tests.Importer.MsSql
{
    public class ServiceCollectionExtensionsUnitTest
    {
        [Fact]
        public void AddMsSqlImporter_Test()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { $"{DbLoaderOptions.Key}:{nameof(DbLoaderOptions.ConnectionString)}", "接続文字列" },
                })
                .Build();
            var environment = new HostingEnvironment { EnvironmentName = Environments.Development, };
            var services = new ServiceCollection().AddMsSqlImporter(configuration, environment);
            using var serviceProvider = services.BuildServiceProvider();

            var actual = serviceProvider.GetService<IDbLoader>();
            Assert.NotNull(actual);
            Assert.IsType<DbLoader>(actual);
        }
    }
}