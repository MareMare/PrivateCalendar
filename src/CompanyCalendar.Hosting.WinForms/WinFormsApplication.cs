using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CompanyCalendar.Hosting.WinForms
{
    public static class WinFormsApplication
    {
        public static void Run<TMainForm>(IHostBuilder hostBuilder)
            where TMainForm : Form
        {
            using var host = hostBuilder.Build();
            using var serviceScope = host.Services.CreateAsyncScope();
            var services = serviceScope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(nameof(WinFormsApplication));
            //var serviceProviderIsService = services.GetService<IServiceProviderIsService>();
            try
            {
                logger.LogInformation("起動します。{a}", 10);

                Application.Run(services.GetRequiredService<TMainForm>());

                logger.LogInformation("終了しました。{a}", 10);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "起動中に例外が発生しました。{ex}", ex.Message);
            }
        }
    }
}