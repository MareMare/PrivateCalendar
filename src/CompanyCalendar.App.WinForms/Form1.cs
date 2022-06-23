namespace CompanyCalendar.App.WinForms
{
    public partial class Form1 : Form
    {
        private readonly ICalendarExporter _calendarExporter = null!;
        private readonly IIcsExporter _icsExporter = null!;
        private readonly ICsvLoader _csvLoader = null!;
        private readonly IDbLoader _dbLoader = null!;

        public Form1(
            ICalendarExporter calendarExporter,
            IIcsExporter icsExporter,
            ICsvLoader csvLoader,
            IDbLoader dbLoader)
            : this()
        {
            ArgumentNullException.ThrowIfNull(calendarExporter);
            ArgumentNullException.ThrowIfNull(icsExporter);
            ArgumentNullException.ThrowIfNull(csvLoader);
            ArgumentNullException.ThrowIfNull(dbLoader);

            _calendarExporter = calendarExporter;
            _icsExporter = icsExporter;
            _csvLoader = csvLoader;
            _dbLoader = dbLoader;
        }

        protected Form1()
        {
            InitializeComponent();
        }
    }
}