using Microsoft.Extensions.Options;

namespace CompanyCalendar.Exporter.Ics
{
    public class CalendarExporter : ICalendarExporter
    {
        private readonly ExporterOptions _options;

        public CalendarExporter(IOptions<ExporterOptions> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(options.Value);
            ArgumentNullException.ThrowIfNull(options.Value.FilePath);

            this._options = options.Value;
        }

        public async Task ExportAsync(IEnumerable<(DateTime date, string summary)> eventPairs, CancellationToken taskCancellationToken = default)
        {
            // [asp\.net \- How to create \.ics file using c\#? \- Stack Overflow](https://stackoverflow.com/questions/46033843/how-to-create-ics-file-using-c/46042482#46042482)
            var generator = new IcsRuntimeTemplate
            {
                CalendarName = "HSC 社内カレンダー",
                ProductId = "hsw.co.jp",
                EventPairs = eventPairs
                    .Select(pair =>
                        (loweDate: pair.date, upperDate: pair.date.ToNextDay(), pair.summary))
                    .ToArray(),
            };
            var ics = generator.TransformText();

            var path = this._options.FilePath;
            var encoding = this._options.FileEncoding;
            //await File.WriteAllTextAsync(path, ics, encoding, taskCancellationToken).ConfigureAwait(false);
            await using var stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            await using var writer = new StreamWriter(stream, encoding);
            await writer.WriteAsync(ics.AsMemory(), taskCancellationToken).ConfigureAwait(false);
        }
   }
}