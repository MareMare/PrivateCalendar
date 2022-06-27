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

        /// <summary>
        /// <see cref="ILoggerFactory" /> を取得します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="ILoggerFactory" /> 型。
        /// <para><see cref="ILoggerFactory" />。既定値は <see langword="null" /> です。</para>
        /// </value>
        protected ILoggerFactory XUnitLoggerFactory { get; }
    }
}