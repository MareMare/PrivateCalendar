// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvLoader.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.Extensions.Options;

namespace CompanyCalendar.Importer.Csv
{
    /// <summary>
    /// CSV ファイルから読み込みを提供します。
    /// </summary>
    public class CsvLoader : ICsvLoader
    {
        /// <summary>オプション構成を表します。</summary>
        private readonly CsvLoaderOptions _options;

        /// <summary>
        /// <see cref="CsvLoader" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="options">オプション構成。</param>
        public CsvLoader(IOptions<CsvLoaderOptions> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(options.Value);

            this._options = options.Value;
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<HolidayItem> LoadAsync(
            string csvFilePath,
            DateTime? lowerDate = null,
            DateTime? upperDate = null,
            [EnumeratorCancellation] CancellationToken taskCancellationToken = default)
        {
            var encoding = this._options.FileEncoding;
            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = true,
                DetectColumnCountChanges = true,
                Encoding = encoding,

                // NOTE: [c\# \- How to ignore empty rows in CSV when reading \- Stack Overflow](https://stackoverflow.com/a/57994196)
                // NOTE: [c\# \- How to skip blank rows in CsvHelper >28\.0\.0? \- Stack Overflow](https://stackoverflow.com/questions/72937181/how-to-skip-blank-rows-in-csvhelper-28-0-0/72937276#72937276)
                // NOTE: [CsvHelper/index\.md at 3cb8507932c62e1e39f09026999bcfca864ad0a6 · JoshClose/CsvHelper](https://github.com/JoshClose/CsvHelper/blob/3cb8507932c62e1e39f09026999bcfca864ad0a6/src/CsvHelper.Website/input/migration/v28/index.md)
                ShouldSkipRecord = records => records.Row.Parser.Record?.All(string.IsNullOrEmpty) ?? false,
            };

            var stream = File.Open(csvFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            await using (stream.ConfigureAwait(false))
            {
                using var reader = new StreamReader(stream, encoding);
                using var csv = new CsvReader(reader, csvConfig);
                var records = CsvLoader.GetRecordsAsync<HolidayItemRecord>(csv, taskCancellationToken);
                await foreach (var record in records.ConfigureAwait(false))
                {
                    var item = record.ToHolidayItem();
                    if (item is null)
                    {
                        continue;
                    }

                    var isOutOfRange = false;
                    isOutOfRange |= lowerDate.HasValue && item.Date < lowerDate.Value;
                    isOutOfRange |= upperDate.HasValue && item.Date > upperDate.Value;
                    if (!isOutOfRange)
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        /// 非同期操作として、<see cref="IReader" /> からレコードを読み込みます。
        /// </summary>
        /// <typeparam name="T">レコードの型。</typeparam>
        /// <param name="csv"><see cref="IReader" />。</param>
        /// <param name="taskCancellationToken"><see cref="CancellationToken" />。</param>
        /// <returns><typeparamref name="T" /> の非同期イテレーションを提供する列挙子。</returns>
        private static IAsyncEnumerable<T> GetRecordsAsync<T>(CsvReader csv, CancellationToken taskCancellationToken)
            where T : new() =>
            csv.GetRecordsAsync<T>(taskCancellationToken);

        /// <summary>
        /// CSV ファイルのレコードを表します。
        /// </summary>
        [DebuggerDisplay("{Date}(Kind={Kind}) {Summary}")]
        private sealed class HolidayItemRecord
        {
            /// <summary>
            /// 日付を取得または設定します。
            /// </summary>
            /// <value>
            /// 値を表す <see cref="DateTime" /> 型。
            /// <para>休日、祝日、有給充当基準日に対する日付。既定値は null です。</para>
            /// </value>
            [Index(0)]
            [Format("yyyy/M/d")]
            public DateTime? Date { get; set; }

            /// <summary>
            /// 休日種別を取得または設定します。
            /// </summary>
            /// <value>
            /// 値を表す <see cref="short" /> 型。
            /// <para>休日種別。0:出勤日、1:休日、2:祝日、4:有給充当基準日。既定値は 0 です。</para>
            /// </value>
            [Index(1)]
            public HolidayKind? Kind { get; set; }

            /// <summary>
            /// 概要を取得または設定します。
            /// </summary>
            /// <value>
            /// 値を表す <see cref="string" /> 型。
            /// <para>概要。既定値は null です。</para>
            /// </value>
            [Index(2)]
            public string? Summary { get; set; }

            /// <summary>
            /// <see cref="HolidayItem" /> へ変換します。
            /// </summary>
            /// <returns><see cref="HolidayItem" />。変換できない場合は <see langword="null" />。</returns>
            public HolidayItem? ToHolidayItem() =>
                this.Date.HasValue
                    ? new HolidayItem
                    {
                        Date = this.Date.Value,
                        Kind = this.Kind ?? HolidayKind.Shukkimbi,
                        Summary = this.Summary,
                    }
                    : null;
        }
    }
}
