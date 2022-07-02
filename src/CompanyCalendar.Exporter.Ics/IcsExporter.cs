// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IcsExporter.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Options;

namespace CompanyCalendar.Exporter.Ics
{
    /// <summary>
    /// iCalendar ファイルへのエクスポートを提供します。
    /// </summary>
    public class IcsExporter : IIcsExporter
    {
        /// <summary>オプション構成を表します。</summary>
        private readonly IcsExporterOptions _options;

        /// <summary>
        /// <see cref="IcsExporter" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="options">オプション構成。</param>
        public IcsExporter(IOptions<IcsExporterOptions> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(options.Value);

            this._options = options.Value;
        }

        /// <inheritdoc />
        public async Task ExportAsync(
            string icsFilePath,
            IEnumerable<(DateTime date, string summary)> eventPairs,
            CancellationToken taskCancellationToken = default)
        {
            // [asp\.net \- How to create \.ics file using c\#? \- Stack Overflow](https://stackoverflow.com/questions/46033843/how-to-create-ics-file-using-c/46042482#46042482)
            var pairs = eventPairs.Select(pair => (loweDate: pair.date, upperDate: pair.date.ToNextDay(), pair.summary))
                .ToArray();
            var generator = new IcsRuntimeTemplate(pairs)
            {
                CalendarName = this._options.CalendarName,
                ProductId = this._options.ProductId,
            };
            var ics = generator.TransformText();

            var path = icsFilePath;
            var encoding = this._options.FileEncoding;

            // await File.WriteAllTextAsync(path, ics, encoding, taskCancellationToken).ConfigureAwait(false);
            using var stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            using var writer = new StreamWriter(stream, encoding);
            await writer.WriteAsync(ics.AsMemory(), taskCancellationToken).ConfigureAwait(false);
        }
    }
}
