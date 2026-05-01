using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CompanyCalendar.Tests
{
    public class XUnitLoggerProvider : ILoggerProvider
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public XUnitLoggerProvider(ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }

        public ILogger CreateLogger(string categoryName) => new XUnitLogger(this._testOutputHelper, categoryName);

        public void Dispose()
        {
        }

        [DebuggerStepThrough]
        private class XUnitLogger : ILogger
        {
            private readonly ITestOutputHelper _testOutputHelper;
            private readonly string _categoryName;

            public XUnitLogger(ITestOutputHelper testOutputHelper, string categoryName)
            {
                this._testOutputHelper = testOutputHelper;
                this._categoryName = categoryName;
            }

            public IDisposable BeginScope<TState>(TState state) where TState : notnull => NoopDisposable.Instance;

            public bool IsEnabled(LogLevel logLevel)
                => true;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
                Func<TState, Exception?, string> formatter)
            {
                this._testOutputHelper.WriteLine($"{this._categoryName} [{eventId}] {formatter(state, exception)}");
                if (exception != null)
                {
                    this._testOutputHelper.WriteLine(exception.ToString());
                }
            }
        }

        [DebuggerStepThrough]
        private class NoopDisposable : IDisposable
        {
            public static readonly NoopDisposable Instance = new();

            public void Dispose()
            {
            }
        }
    }
}
