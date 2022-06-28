// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalendarExporterOptions.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CompanyCalendar.Exporter.Google
{
    /// <summary>
    /// オプション構成を表します。
    /// </summary>
    public class CalendarExporterOptions
    {
        /// <summary>構成キーを表します。</summary>
        public static readonly string Key = "GoogleCalendarExporter";

        /// <summary>
        /// 認証方法を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="Google.CredentialKind" /> 型。
        /// <para>認証方法。既定値は <see cref="Google.CredentialKind.OAuth" /> です。</para>
        /// </value>
        public CredentialKind CredentialKind { get; set; } = CredentialKind.OAuth;

        /// <summary>
        /// アプリケーション名を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>アプリケーション名。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string ApplicationName { get; set; } = null!;

        /// <summary>
        /// カレンダー ID を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>カレンダー ID。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string CalendarId { get; set; } = null!;

        /// <summary>
        /// API キーを取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>API キー。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string ApiKey { get; set; } = null!;

        /// <summary>
        /// クライアント ID を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>クライアント ID。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string OAuthClientId { get; set; } = null!;

        /// <summary>
        /// クライアント シークレットを取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>クライアント シークレット。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string OAuthClientSecret { get; set; } = null!;

        /// <summary>
        /// サービスアカウントのメールアドレスを取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>サービスアカウントのメールアドレス。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string ServiceAccountClientEmail { get; set; } = null!;

        /// <summary>
        /// サービスアカウントの秘密鍵を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>サービスアカウントの秘密鍵。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string ServiceAccountPrivateKey { get; set; } = null!;
    }
}
