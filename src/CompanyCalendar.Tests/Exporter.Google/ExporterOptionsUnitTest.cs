using CompanyCalendar.Exporter.Google;
using Xunit;

namespace CompanyCalendar.Tests.Exporter.Google
{
    public class ExporterOptionsUnitTest
    {
        [Fact]
        public void ExporterOptions_Ctor_Test1()
        {
            var actual = new CalendarExporterOptions();
            Assert.Equal(CredentialKind.OAuth, actual.CredentialKind);
            Assert.Null(actual.ApplicationName);
            Assert.Null(actual.CalendarId);
            Assert.Null(actual.ApiKey);
            Assert.Null(actual.OAuthClientId);
            Assert.Null(actual.OAuthClientSecret);
            Assert.Null(actual.ServiceAccountClientEmail);
            Assert.Null(actual.ServiceAccountPrivateKey);
        }
    }
}