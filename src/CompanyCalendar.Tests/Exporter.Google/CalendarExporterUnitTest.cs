using System;
using System.Threading.Tasks;
using CompanyCalendar.Exporter.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Xunit;

namespace CompanyCalendar.Tests.Exporter.Google
{
    [Trait("Category", "local")]
    public class CalendarExporterUnitTest
    {
        private readonly IConfiguration _config;

        public CalendarExporterUnitTest()
        {
            // 
            // {
            //    "ApplicationName": "...",
            //    "CalendarId": "...",
            //    "ApiKey": "...",
            //    "OAuthClientId": "...",
            //    "OAuthClientSecret": "...",
            //    "ServiceAccountClientEmail": "...",
            //    "ServiceAccountPrivateKey": "..."
            // }
            this._config = new ConfigurationBuilder()
                .AddUserSecrets<CalendarExporterUnitTest>() // (A) for local
                .AddEnvironmentVariables()                  // (B) for dotnet test env in github actions
                .Build();
        }

        [Fact]
        public async Task Test_Google_ExportAsync()
        {
            var options = this._config.Get<CalendarExporterOptions>() ?? new CalendarExporterOptions();
            options.CredentialKind = CredentialKind.ServiceAccount;

            var exporter = new CalendarExporter(Options.Create(options));
            var pairs = new[]
            {
                (DateTime.Today, "HSC Debug"),
                (DateTime.Today, "HSC Debug"),
            };
            await exporter.ExportAsync(pairs).ConfigureAwait(true);
        }
    }
}
