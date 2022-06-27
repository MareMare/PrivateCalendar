using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.Extensions.Options;

namespace CompanyCalendar.Importer.Csv
{
    public class CsvLoader : ICsvLoader
    {
        private readonly CsvLoaderOptions _options;

        public CsvLoader(IOptions<CsvLoaderOptions> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(options.Value);

            this._options = options.Value;
        }

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
                ShouldSkipRecord = records => records.Record.All(string.IsNullOrEmpty),
                // NOTE: [c\# \- How to ignore empty rows in CSV when reading \- Stack Overflow](https://stackoverflow.com/a/57994196)
            };

            using var stream = File.Open(csvFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(stream, encoding);
            using var csv = new CsvReader(reader, csvConfig);
            await foreach (var record in CsvLoader.GetRecordsAsync<HolidayItemRecord>(csv, taskCancellationToken).ConfigureAwait(false))
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

        private static IAsyncEnumerable<T> GetRecordsAsync<T>(IReader csv, CancellationToken taskCancellationToken) where T : new() =>
            csv.GetRecordsAsync<T>(taskCancellationToken);

        /// <summary>
        /// CSV ファイルのレコードを表します。
        /// </summary>
        [DebuggerDisplay("{Date}(Kind={Kind}) {Summary}")]
        private class HolidayItemRecord
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
            /// <see cref="HolidayItem"/> へ変換します。
            /// </summary>
            /// <returns><see cref="HolidayItem"/>。変換できない場合は <see langword="null" />。</returns>
            public HolidayItem? ToHolidayItem()
            {
                return this.Date.HasValue
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
}
