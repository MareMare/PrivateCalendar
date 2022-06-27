using CompanyCalendar.Exporter.Google;
using CompanyCalendar.Exporter.Ics;
using CompanyCalendar.Hosting.WinForms;
using CompanyCalendar.Importer.Csv;
using CompanyCalendar.Importer.MsSql;
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
            WinFormsApplication.Run<Form1>(Program.CreateHostBuilder(args));
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                    services.AddConfiguredServices(hostingContext.Configuration, hostingContext.HostingEnvironment));

        private static IServiceCollection AddConfiguredServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            return services
                .AddGoogleExporter(configuration)
                .AddIcsExporter(configuration)
                .AddCsvImporter(configuration)
                .AddMsSqlImporter(configuration, environment)
                .AddTransient<Form1>();
        }
    }
}