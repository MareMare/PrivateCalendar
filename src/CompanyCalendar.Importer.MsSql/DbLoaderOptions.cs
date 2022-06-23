namespace CompanyCalendar.Importer.MsSql
{
    public class DbLoaderOptions
    {
        public static readonly string Key = "MsSqlDbLoader";

        public string ConnectionString { get; set; } = null!;

        public int CommandTimeoutSeconds { get; set; } = 10;
    }
}