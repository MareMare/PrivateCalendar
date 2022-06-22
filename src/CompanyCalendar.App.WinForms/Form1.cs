namespace CompanyCalendar.App.WinForms
{
    public partial class Form1 : Form
    {
        private readonly IHolidaysLoader _loader = null!;

        public Form1(IHolidaysLoader loader)
            : this()
        {
            ArgumentNullException.ThrowIfNull(loader);
            _loader = loader;
        }

        protected Form1()
        {
            InitializeComponent();
        }

    }
}