﻿namespace CompanyCalendar.Exporter.Google
{
    public class CalendarExporterOptions
    {
        public static readonly string Key = "GoogleCalendarExporter";

        public CredentialKind CredentialKind { get; set; } = CredentialKind.OAuth;

        public string ApplicationName { get; set; } = null!;

        public string CalendarId { get; set; } = null!;

        /// <summary>Gets or sets the API key.</summary>
        public string ApiKey { get; set; } = null!;

        /// <summary>Gets or sets the client identifier.</summary>
        public string OAuthClientId { get; set; } = null!;

        /// <summary>Gets or sets the client Secret.</summary>
        public string OAuthClientSecret { get; set; } = null!;

        public string ServiceAccountClientEmail { get; set; } = null!;

        public string ServiceAccountPrivateKey { get; set; } = null!;
    }
}