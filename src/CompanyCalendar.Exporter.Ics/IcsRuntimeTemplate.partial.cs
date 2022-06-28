// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IcsRuntimeTemplate.partial.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CompanyCalendar.Exporter.Ics
{
    /// <summary>
    /// ICS の実行時テンプレートを表します。
    /// </summary>
    public partial class IcsRuntimeTemplate
    {
        /// <summary>
        /// <see cref="IcsRuntimeTemplate" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="pairs">開始日時、終了日時、イベント概要のタプルコレクション。</param>
        public IcsRuntimeTemplate(IEnumerable<(DateTime lowerDate, DateTime upperDate, string summary)>? pairs = null)
        {
            this.EventPairs = (pairs ?? Array.Empty<(DateTime lowerDate, DateTime upperDate, string summary)>())
                .ToList();
            this.Timestamp = $"{DateTime.Now.ToUniversalTime():yyyyMMddTHHmmssZ}";
        }

        /// <summary>
        /// プロダクト ID を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>プロダクト ID。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string ProductId { get; set; } = null!;

        /// <summary>
        /// カレンダー名を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>カレンダー名。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string CalendarName { get; set; } = null!;

        /// <summary>
        /// タイムスタンプの文字列を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>タイムスタンプの文字列。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string Timestamp { get; }

        /// <summary>
        /// イベントペアのコレクションを取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す 開始日付、終了日付、サマリのタプル 型。
        /// <para>イベントペアのコレクション。既定値は <see langword="null" /> です。</para>
        /// </value>
        public IList<(DateTime lowerDate, DateTime upperDate, string summary)> EventPairs { get; }
    }
}
