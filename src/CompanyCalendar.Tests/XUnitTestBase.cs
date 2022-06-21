using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace CompanyCalendar.Tests
{
    public abstract class XUnitTestBase
    {
        protected XUnitTestBase(ITestOutputHelper testOutputHelper)
        {
            this.XUnitLoggerFactory = LoggerFactory.Create(builder => builder.AddProvider(new XUnitLoggerProvider(testOutputHelper)));
        }

        protected ILoggerFactory XUnitLoggerFactory { get; }
    }
}