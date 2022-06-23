using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyCalendar.Importer.Csv
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCsvImporter(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<CsvLoaderOptions>(configuration.GetSection(CsvLoaderOptions.Key))
                .Configure<CsvLoaderOptions>(options => options.FileEncoding = Encoding.UTF8)
                .AddTransient<ICsvLoader, CsvLoader>();
        }
    }
}