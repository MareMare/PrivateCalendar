// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarExporter.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Globalization;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Options;

namespace CompanyCalendar.Exporter.Google
{
    /// <summary>
    /// google カレンダへのエクスポートを提供します。
    /// </summary>
    public class CalendarExporter : ICalendarExporter
    {
        /// <summary>TimeZoneId を表します。</summary>
        private const string TzId = "Asia/Tokyo";

        /// <summary>スコープの配列を表します。</summary>
        private static readonly string[] Scopes =
        {
            CalendarService.Scope.Calendar,
            CalendarService.Scope.CalendarReadonly,
        };

        /// <summary>オプション構成を表します。</summary>
        private readonly CalendarExporterOptions _options;

        /// <summary>
        /// <see cref="CalendarExporter" /> クラスの新しいインスタンスを生成します。
        /// </summary>
        /// <param name="options">オプション構成。</param>
        public CalendarExporter(IOptions<CalendarExporterOptions> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(options.Value);

            this._options = options.Value;
        }

        /// <inheritdoc />
        public async Task ExportAsync(
            IEnumerable<(DateTime date, string summary)> eventPairs,
            CancellationToken taskCancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(eventPairs);

            using var service = await this.CreateCalendarServiceAsync(taskCancellationToken).ConfigureAwait(false);
            foreach (var (date, summary) in eventPairs)
            {
                var existingEvent = await this.FindEventAsync(service, date, taskCancellationToken).ConfigureAwait(false);
                if (existingEvent != null)
                {
                    CalendarExporter.UpdateEvent(existingEvent, date, summary);
                    var updatedEvent = await this.UpdateEventAsync(service, existingEvent, taskCancellationToken).ConfigureAwait(false);
                    Debug.Print($"HtmlLink={updatedEvent.HtmlLink}");
                }
                else
                {
                    var eventItem = CalendarExporter.CreateEvent(date, summary);
                    var createdEvent = await this.InsertEventAsync(service, eventItem, taskCancellationToken).ConfigureAwait(false);
                    Debug.Print($"HtmlLink={createdEvent.HtmlLink}");
                }
            }
        }

        /// <summary>
        /// イベントを生成します。
        /// </summary>
        /// <param name="eventDateTime">イベントの日付。</param>
        /// <param name="summary">イベントの概要。</param>
        /// <returns><see cref="Event" />。</returns>
        private static Event CreateEvent(DateTime eventDateTime, string summary)
        {
            var eventItem = new Event
            {
                Summary = summary,
                Start = new EventDateTime
                {
                    Date = $"{eventDateTime.Date:yyyy-MM-dd}",
                    TimeZone = CalendarExporter.TzId,
                },
                End = new EventDateTime
                {
                    Date = $"{eventDateTime.Date:yyyy-MM-dd}",
                    TimeZone = CalendarExporter.TzId,
                },
            };
            return eventItem;
        }

        /// <summary>
        /// イベントを更新します。
        /// </summary>
        /// <param name="eventItem"><see cref="Event" />。</param>
        /// <param name="eventDateTime">イベントの日付。</param>
        /// <param name="summary">イベントの概要。</param>
        private static void UpdateEvent(Event eventItem, DateTime eventDateTime, string summary)
        {
            eventItem.Summary = summary;
            eventItem.Start = new EventDateTime
            {
                Date = $"{eventDateTime.Date:yyyy-MM-dd}",
                TimeZone = CalendarExporter.TzId,
            };
            eventItem.End = new EventDateTime
            {
                Date = $"{eventDateTime.Date:yyyy-MM-dd}",
                TimeZone = CalendarExporter.TzId,
            };
        }

        /// <summary>
        /// 非同期操作として、<see cref="CalendarService" /> を生成します。
        /// </summary>
        /// <param name="taskCancellationToken"><see cref="CancellationToken" />。</param>
        /// <returns><see cref="CalendarService" />。</returns>
        private async Task<CalendarService> CreateCalendarServiceAsync(
            CancellationToken taskCancellationToken = default)
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

        /// <summary>
        /// OAuth 2.0 での <see cref="ICredential" /> を生成します。
        /// </summary>
        /// <param name="taskCancellationToken"><see cref="CancellationToken" />。</param>
        /// <returns><see cref="ICredential" />。</returns>
        private async Task<ICredential> CreateOAuthCredentialAsync(
            CancellationToken taskCancellationToken = default)
        {
            // OAuth 2.0 で対話形式の認証を行います。
            var clientSecrets = new ClientSecrets
            {
                ClientId = this._options.OAuthClientId,
                ClientSecret = this._options.OAuthClientSecret,
            };
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    clientSecrets,
                    CalendarExporter.Scopes,
                    "user",
                    taskCancellationToken)
                .ConfigureAwait(false);
            return credential;
        }

        /// <summary>
        /// サービスアカウントでの <see cref="ICredential" /> を生成します。
        /// </summary>
        /// <returns><see cref="ICredential" />。</returns>
        private Task<ICredential> CreateServiceAccountCredentialAsync()
        {
            // サービスアカウントで認証を行います。
            var initializer =
                new ServiceAccountCredential.Initializer(this._options.ServiceAccountClientEmail)
                        { Scopes = CalendarExporter.Scopes }
                    .FromPrivateKey(this._options.ServiceAccountPrivateKey);
            var credential = new ServiceAccountCredential(initializer);
            return Task.FromResult((ICredential)credential);
        }

        /// <summary>
        /// 非同期操作として、ダンプします。
        /// </summary>
        /// <param name="service"><see cref="IClientService" />。</param>
        /// <returns>完了を表す <see cref="Task" />。</returns>
        // ReSharper disable once UnusedMember.Local
        private async Task DumpThisMonth(IClientService service)
        {
            var request = new EventsResource.ListRequest(service, this._options.CalendarId)
            {
                TimeMinDateTimeOffset = DateTime.Today.ToFirstDayInMonth(),
                TimeMaxDateTimeOffset = DateTime.Today.ToLastDayInMonth(),
                ShowDeleted = false,
                SingleEvents = true,
                OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime,
            };
            var events = await request.ExecuteAsync().ConfigureAwait(false);
            foreach (var eventItem in events.Items)
            {
                var lowerDateTime = eventItem.Start.DateTimeDateTimeOffset ??
                                    DateTime.Parse(eventItem.Start.Date, CultureInfo.InvariantCulture);
                var upperDateTime = eventItem.End.DateTimeDateTimeOffset ??
                                    DateTime.Parse(eventItem.End.Date, CultureInfo.InvariantCulture);

                Debug.Print($"{lowerDateTime} to {upperDateTime} {eventItem.Summary}");
            }
        }

        /// <summary>
        /// 非同期操作として、イベントを検索します。
        /// </summary>
        /// <param name="service"><see cref="IClientService" />。</param>
        /// <param name="eventDateTime">イベント日付。</param>
        /// <param name="taskCancellationToken"><see cref="CancellationToken" />。</param>
        /// <returns>見つかった <see cref="Event" />。見つからない場合は <see langword="null" />。</returns>
        private async Task<Event?> FindEventAsync(
            IClientService service,
            DateTime eventDateTime,
            CancellationToken taskCancellationToken = default)
        {
            var request = new EventsResource.ListRequest(service, this._options.CalendarId)
            {
                TimeMinDateTimeOffset = eventDateTime.Date,
                TimeMaxDateTimeOffset = eventDateTime.Date.ToNextDay(),
                ShowDeleted = false,
                SingleEvents = true,
                OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime,
            };

            var events = await request.ExecuteAsync(taskCancellationToken).ConfigureAwait(false);
            var foundEventItem = events.Items.FirstOrDefault(item =>
                item.Summary.StartsWith("HSC", StringComparison.InvariantCulture));
            return foundEventItem;
        }

        /// <summary>
        /// 非同期操作として、イベントを追加します。
        /// </summary>
        /// <param name="service"><see cref="IClientService" />。</param>
        /// <param name="eventItem">イベント。</param>
        /// <param name="taskCancellationToken"><see cref="CancellationToken" />。</param>
        /// <returns>追加された <see cref="Event" />。</returns>
        private async Task<Event> InsertEventAsync(
            IClientService service,
            Event eventItem,
            CancellationToken taskCancellationToken = default)
        {
            var request = new EventsResource.InsertRequest(service, eventItem, this._options.CalendarId);
            var createdEvent = await request.ExecuteAsync(taskCancellationToken).ConfigureAwait(false);
            return createdEvent;
        }

        /// <summary>
        /// 非同期操作として、イベントを更新します。
        /// </summary>
        /// <param name="service"><see cref="IClientService" />。</param>
        /// <param name="eventItem">イベント。</param>
        /// <param name="taskCancellationToken"><see cref="CancellationToken" />。</param>
        /// <returns>更新された <see cref="Event" />。</returns>
        private async Task<Event> UpdateEventAsync(
            IClientService service,
            Event eventItem,
            CancellationToken taskCancellationToken = default)
        {
            var request = new EventsResource.UpdateRequest(service, eventItem, this._options.CalendarId, eventItem.Id);
            var updatedEvent = await request.ExecuteAsync(taskCancellationToken).ConfigureAwait(false);
            return updatedEvent;
        }
    }
}
