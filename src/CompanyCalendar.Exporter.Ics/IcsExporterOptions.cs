// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IcsExporterOptions.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Text;

namespace CompanyCalendar.Exporter.Ics
{
    /// <summary>
    /// オプション構成を表します。
    /// </summary>
    public class IcsExporterOptions
    {
        /// <summary>構成キーを表します。</summary>
        public static readonly string Key = "IcsExporter";

        /// <summary>
        /// エンコーディングを取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="Encoding" /> 型。
        /// <para>エンコーディング。既定値は <see cref="Encoding.UTF8" /> です。</para>
        /// </value>
        public Encoding FileEncoding { get; set; } = Encoding.UTF8;

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
    }
}
