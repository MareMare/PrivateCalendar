using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyCalendar.Exporter.Ics
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIcsExporter(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<IcsExporterOptions>(configuration.GetSection(IcsExporterOptions.Key))
                .AddTransient<IIcsExporter, IcsExporter>();
        }
    }
}