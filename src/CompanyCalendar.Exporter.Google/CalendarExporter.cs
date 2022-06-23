using System.Diagnostics;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Options;

namespace CompanyCalendar.Exporter.Google
{
    public class CalendarExporter : ICalendarExporter
    {
        private static readonly string[] Scopes = { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarReadonly };

        private const string TzId = "Asia/Tokyo";

        private readonly CalendarExporterOptions _options;

        public CalendarExporter(IOptions<CalendarExporterOptions> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(options.Value);
            
            this._options = options.Value;
        }

        public async Task ExportAsync(IEnumerable<(DateTime date, string summary)> eventPairs, CancellationToken taskCancellationToken = default)
        {
            using var service = await CreateCalendarServiceAsync(taskCancellationToken);
            foreach ((DateTime date, string summary) in eventPairs)
            {
                var existingEvent = await this.FindEventAsync(service, date, taskCancellationToken).ConfigureAwait(false);
                if (existingEvent != null)
                {
                    UpdateEvent(existingEvent, date, summary);
                    var updatedEvent = await this.UpdateEventAsync(service, existingEvent, taskCancellationToken).ConfigureAwait(false);
                    Debug.Print($"HtmlLink={updatedEvent.HtmlLink}");
                }
                else
                {
                    var eventItem = CreateEvent(date, summary);
                    var createdEvent = await this.InsertEventAsync(service, eventItem, taskCancellationToken).ConfigureAwait(false);
                    Debug.Print($"HtmlLink={createdEvent.HtmlLink}");
                }
            }
        }

        private static Event CreateEvent(DateTime eventDateTime, string summary)
        {
            var eventItem = new Event
            {
                Summary = summary,
                Start = new EventDateTime { Date = $"{eventDateTime.Date:yyyy-MM-dd}", TimeZone = TzId, },
                End = new EventDateTime { Date = $"{eventDateTime.Date:yyyy-MM-dd}", TimeZone = TzId, },
            };
            return eventItem;
        }

        private static void UpdateEvent(Event eventItem, DateTime eventDateTime, string summary)
        {
            eventItem.Summary = summary;
            eventItem.Start = new EventDateTime { Date = $"{eventDateTime.Date:yyyy-MM-dd}", TimeZone = TzId, };
            eventItem.End = new EventDateTime { Date = $"{eventDateTime.Date:yyyy-MM-dd}", TimeZone = TzId, };
        }

        private async Task<CalendarService> CreateCalendarServiceAsync(CancellationToken taskCancellationToken = default)
        {
            switch (this._options.CredentialKind)
            {
                case CredentialKind.ApiKey:
                {
                    var initializer = new BaseClientService.Initializer // API キーのみでの認証
                    {
                        ApplicationName = this._options.ApplicationName,
                        ApiKey = this._options.ApiKey,
                    };
                    return new CalendarService(initializer);
                }

                case CredentialKind.ServiceAccount:
                {
                    var credential = await this.CreateServiceAccountCredentialAsync().ConfigureAwait(false);
                    var initializer = new BaseClientService.Initializer // サービスアカウントでの認証
                    {
                        ApplicationName = this._options.ApplicationName,
                        HttpClientInitializer = credential,
                    };
                    return new CalendarService(initializer);
                }

                case CredentialKind.OAuth:
                default:
                {
                    var credential = await this.CreateOAuthCredentialAsync(taskCancellationToken).ConfigureAwait(false);
                    var initializer = new BaseClientService.Initializer // OAuth2.0 での認証
                    {
                        ApplicationName = this._options.ApplicationName,
                        HttpClientInitializer = credential,
                    };
                    return new CalendarService(initializer);
                }
            }
        }

        private async Task<ICredential> CreateOAuthCredentialAsync(CancellationToken taskCancellationToken = default)
        {
            // OAuth 2.0 で対話形式の認証を行います。
            var clientSecrets = new ClientSecrets
            {
                ClientId = this._options.OAuthClientId,
                ClientSecret = this._options.OAuthClientSecret,
            };
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    clientSecrets,
                    Scopes,
                    "user",
                    taskCancellationToken)
                .ConfigureAwait(false);
            return credential;
        }

        private Task<ICredential> CreateServiceAccountCredentialAsync()
        {
            // サービスアカウントで認証を行います。
            var initializer =
                new ServiceAccountCredential.Initializer(this._options.ServiceAccountClientEmail) { Scopes = Scopes }
                    .FromPrivateKey(this._options.ServiceAccountPrivateKey);
            var credential = new ServiceAccountCredential(initializer);
            return Task.FromResult((ICredential)credential);
        }

        private async Task DumpThisMonth(IClientService service)
        {
            var request = new EventsResource.ListRequest(service, this._options.CalendarId)
            {
                TimeMin = DateTime.Today.ToFirstDayInMonth(),
                TimeMax = DateTime.Today.ToLastDayInMonth(),
                ShowDeleted = false,
                SingleEvents = true,
                OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime,
            };
            var events = await request.ExecuteAsync().ConfigureAwait(false);
            foreach (var eventItem in events.Items)
            {
                var lowerDateTime = eventItem.Start.DateTime ?? DateTime.Parse(eventItem.Start.Date);
                var upperDateTime = eventItem.End.DateTime ?? DateTime.Parse(eventItem.End.Date);

                Debug.Print($"{lowerDateTime} to {upperDateTime} {eventItem.Summary}");
            }
        }

        private async Task<Event?> FindEventAsync(IClientService service, DateTime eventDateTime, CancellationToken taskCancellationToken = default)
        {
            var request = new EventsResource.ListRequest(service, this._options.CalendarId)
            {
                TimeMin = eventDateTime.Date,
                TimeMax = eventDateTime.Date.ToNextDay(),
                ShowDeleted = false,
                SingleEvents = true,
                OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime,
            };

            var events = await request.ExecuteAsync(taskCancellationToken).ConfigureAwait(false);
            var foundEventItem = events.Items.FirstOrDefault(item => item.Summary.StartsWith("HSC"));
            return foundEventItem;
        }

        private async Task<Event> InsertEventAsync(IClientService service, Event eventItem, CancellationToken taskCancellationToken = default)
        {
            var request = new EventsResource.InsertRequest(service, eventItem, this._options.CalendarId);
            var createdEvent = await request.ExecuteAsync(taskCancellationToken).ConfigureAwait(false);
            return createdEvent;
        }

        private async Task<Event> UpdateEventAsync(IClientService service, Event eventItem, CancellationToken taskCancellationToken = default)
        {
            var request = new EventsResource.UpdateRequest(service, eventItem, this._options.CalendarId, eventItem.Id);
            var updatedEvent = await request.ExecuteAsync(taskCancellationToken).ConfigureAwait(false);
            return updatedEvent;
        }
    }
}