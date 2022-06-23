using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CompanyCalendar.Importer.MsSql
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMsSqlImporter(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            return services
                .AddDbContext<AppDbContext>(
                    options =>
                    {
                        var dbLoaderOptions = configuration.GetSection(DbLoaderOptions.Key).Get<DbLoaderOptions>();

                        options.UseSqlServer(
                            dbLoaderOptions.ConnectionString, // configuration.GetConnectionString("AppDatabase"), // ConnectionStrings:AppDatabase
                            sqlServerOptions => sqlServerOptions.CommandTimeout(dbLoaderOptions.CommandTimeoutSeconds));
                        options.EnableDetailedErrors();
                        if (environment.IsDevelopment())
                        {
                            options.EnableSensitiveDataLogging();
                        }
                    })
                .AddTransient<IDbLoader, DbLoader>();
        }
    }
}