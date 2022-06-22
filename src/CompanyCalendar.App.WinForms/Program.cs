using CompanyCalendar.Hosting.WinForms;
using CompanyCalendar.Importer.Csv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CompanyCalendar.App.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            WinFormsApplication.Run<Form1>(CreateHostBuilder(args));
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                    services.AddConfiguredServices(context.Configuration, context.HostingEnvironment));

        private static IServiceCollection AddConfiguredServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment? environment = null)
        {
            return services
                .Configure<CsvLoaderOptions>(configuration.GetSection(CsvLoaderOptions.Key)) // Option Pattern 1
                .Configure<CsvLoaderOptions>(CsvLoaderOptions.Key, configuration)            // Option Pattern 2
                .Configure<CsvLoaderOptions>(options => options.FilePath = "ほげ")           // Option Pattern 3
                .AddTransient<IHolidaysLoader, CsvLoader>()
                .AddTransient<Form1>();
        }
    }
}