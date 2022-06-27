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
            ArgumentNullException.ThrowIfNull(hostBuilder);

            using var host = hostBuilder.Build();
            using var serviceScope = host.Services.CreateAsyncScope();
            var services = serviceScope.ServiceProvider;
            var env = services.GetRequiredService<IHostEnvironment>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(nameof(WinFormsApplication));
            //var serviceProviderIsService = services.GetService<IServiceProviderIsService>();
            try
            {
                logger.LogInformation("起動します。{env}", env.EnvironmentName);

                Application.Run(services.GetRequiredService<TMainForm>());

                logger.LogInformation("終了しました。{env}", env.EnvironmentName);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "起動中に例外が発生しました。{ex}", ex.Message);
                throw;
            }
        }
    }
}
