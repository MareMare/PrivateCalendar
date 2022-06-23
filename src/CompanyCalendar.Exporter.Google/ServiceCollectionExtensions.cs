using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyCalendar.Exporter.Google
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGoogleExporter(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<CalendarExporterOptions>(configuration.GetSection(CalendarExporterOptions.Key))
                .AddTransient<ICalendarExporter, CalendarExporter>();
        }
    }
}