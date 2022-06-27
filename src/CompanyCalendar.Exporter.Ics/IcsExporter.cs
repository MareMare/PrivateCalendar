using Microsoft.Extensions.Options;

namespace CompanyCalendar.Exporter.Ics
{
    public class IcsExporter : IIcsExporter
    {
        private readonly IcsExporterOptions _options;

        public IcsExporter(IOptions<IcsExporterOptions> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(options.Value);

            this._options = options.Value;
        }

        public async Task ExportAsync(string icsFilePath, IEnumerable<(DateTime date, string summary)> eventPairs, CancellationToken taskCancellationToken = default)
        {
            // [asp\.net \- How to create \.ics file using c\#? \- Stack Overflow](https://stackoverflow.com/questions/46033843/how-to-create-ics-file-using-c/46042482#46042482)
            var pairs = eventPairs.Select(pair => (loweDate: pair.date, upperDate: pair.date.ToNextDay(), pair.summary)).ToArray();
            var generator = new IcsRuntimeTemplate(pairs)
            {
                CalendarName = this._options.CalendarName,
                ProductId = this._options.ProductId,
            };
            var ics = generator.TransformText();

            var path = icsFilePath;
            var encoding = this._options.FileEncoding;
            //await File.WriteAllTextAsync(path, ics, encoding, taskCancellationToken).ConfigureAwait(false);
            using var stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            using var writer = new StreamWriter(stream, encoding);
            await writer.WriteAsync(ics.AsMemory(), taskCancellationToken).ConfigureAwait(false);
        }
   }
}
