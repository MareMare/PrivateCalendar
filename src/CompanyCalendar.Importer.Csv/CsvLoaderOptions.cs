// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvLoaderOptions.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Text;

namespace CompanyCalendar.Importer.Csv
{
    /// <summary>
    /// オプション構成を表します。
    /// </summary>
    public class CsvLoaderOptions
    {
        /// <summary>構成キーを表します。</summary>
        public static readonly string Key = "CsvLoader";

        /// <summary>
        /// エンコーディングを取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="Encoding" /> 型。
        /// <para>エンコーディング。既定値は <see cref="Encoding.UTF8" /> です。</para>
        /// </value>
        public Encoding FileEncoding { get; set; } = Encoding.UTF8;
    }
}
