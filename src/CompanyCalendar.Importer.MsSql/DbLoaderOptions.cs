// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbLoaderOptions.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CompanyCalendar.Importer.MsSql
{
    /// <summary>
    /// オプション構成を表します。
    /// </summary>
    public class DbLoaderOptions
    {
        /// <summary>構成キーを表します。</summary>
        public static readonly string Key = "MsSqlDbLoader";

        /// <summary>
        /// 接続文字列内容を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>接続文字列内容。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string ConnectionString { get; set; } = null!;

        /// <summary>
        /// コマンドタイムアウト秒を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="int" /> 型。
        /// <para>コマンドタイムアウト秒。既定値は <see langword="10" /> です。</para>
        /// </value>
        public int CommandTimeoutSeconds { get; set; } = 10;
    }
}
