using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CompanyCalendar.Tests
{
    public class DateTimeExtensionsUnitTest
    {
        /// <summary>2022/01/02 03:04:05.678</summary>
        private static readonly DateTime? TestDateTime = DateTime.Parse("2022/01/02 03:04:05.678");

        public static IEnumerable<object?[]> ToStringAsYyyyMMdd_TestData()
        {
            yield return new object?[] { TestDateTime, "20220102" };
            yield return new object?[] { null, null };
        }

        [Theory]
        [MemberData(nameof(ToStringAsYyyyMMdd_TestData))]
        public void ToStringAsYyyyMMdd_Test(DateTime? source, string? expected)
        {
            var actual = source?.ToStringAsYyyyMMdd();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object?[]> ToFirstDayInMonth_TestData()
        {
            yield return new object?[] { TestDateTime, DateTime.Parse("2022/01/01"), };
            yield return new object?[] { null, null, };
        }

        [Theory]
        [MemberData(nameof(ToFirstDayInMonth_TestData))]
        public void ToFirstDayInMonth_Test(DateTime? source, DateTime? expected)
        {
            var actual = source?.ToFirstDayInMonth();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object?[]> ToLastDayInMonth_TestData()
        {
            yield return new object?[] { TestDateTime, DateTime.Parse("2022/01/31"), };
            yield return new object?[] { null, null, };
        }

        [Theory]
        [MemberData(nameof(ToLastDayInMonth_TestData))]
        public void ToLastDayInMonth_Test(DateTime? source, DateTime? expected)
        {
            var actual = source?.ToLastDayInMonth();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object?[]> ToNextDay_TestData()
        {
            yield return new object?[] { TestDateTime, DateTime.Parse("2022/01/03 03:04:05.678"), };
            yield return new object?[] { null, null, };
        }

        [Theory]
        [MemberData(nameof(ToNextDay_TestData))]
        public void ToNextDay_Test(DateTime? source, DateTime? expected)
        {
            var actual = source?.ToNextDay();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object?[]> ToNextMinute_TestData()
        {
            yield return new object?[] { TestDateTime, DateTime.Parse("2022/01/02 03:05:05.678"), };
            yield return new object?[] { null, null, };
        }

        [Theory]
        [MemberData(nameof(ToNextMinute_TestData))]
        public void ToNextMinute_Test(DateTime? source, DateTime? expected)
        {
            var actual = source?.ToNextMinute();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object?[]> ToExceptedMilliseconds_TestData()
        {
            yield return new object?[] { TestDateTime, DateTime.Parse("2022/01/02 03:04:05"), };
            yield return new object?[] { null, null, };
        }

        [Theory]
        [MemberData(nameof(ToExceptedMilliseconds_TestData))]
        public void ToExceptedMilliseconds_Test(DateTime? source, DateTime? expected)
        {
            var actual = source?.ToExceptedMilliseconds();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object?[]> ToExceptedSeconds_TestData()
        {
            yield return new object?[] { TestDateTime, DateTime.Parse("2022/01/02 03:04:00"), };
            yield return new object?[] { null, null, };
        }

        [Theory]
        [MemberData(nameof(ToExceptedSeconds_TestData))]
        public void ToExceptedSeconds_Test(DateTime? source, DateTime? expected)
        {
            var actual = source?.ToExceptedSeconds();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void YieldDates_Test()
        {
            var lowerDateTime = DateTime.Parse("2022/01/01");
            var upperDateTime = DateTime.Parse("2022/01/03");
            
            var dateTimes = lowerDateTime.YieldDates(upperDateTime).ToArray();
            Assert.Equal(3, dateTimes.Length);

            Assert.Equal(DateTime.Parse("2022/01/01"), dateTimes[0]);
            Assert.Equal(DateTime.Parse("2022/01/02"), dateTimes[1]);
            Assert.Equal(DateTime.Parse("2022/01/03"), dateTimes[2]);
        }
    }
}