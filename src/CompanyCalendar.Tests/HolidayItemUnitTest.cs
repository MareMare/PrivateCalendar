using System;
using System.Collections.Generic;
using Xunit;

namespace CompanyCalendar.Tests
{
    public class HolidayItemUnitTest
    {
        [Fact]
        public void HolidayItem_Ctor_Test1()
        {
            var actual = new HolidayItem();
            Assert.Equal(DateTime.MinValue, actual.Date);
            Assert.Equal(HolidayKind.Shukkimbi, actual.Kind);
            Assert.Null(actual.Summary);
            Assert.False(actual.IsHoliday);
        }

        public static IEnumerable<object?[]> HolidayKind_IsHoliday_TestData()
        {
            yield return new object?[] { HolidayKind.Shukkimbi, false, };
            yield return new object?[] { HolidayKind.Kyujitsu, true, };
            yield return new object?[] { HolidayKind.Shukujitsu, true, };
            yield return new object?[] { HolidayKind.YukyuKijumbi, true, };
        }

        [Theory]
        [MemberData(nameof(HolidayKind_IsHoliday_TestData))]
        public void HolidayKind_IsHoliday_Test(HolidayKind kind, bool? expected)
        {
            var actual = new HolidayItem { Kind = kind };
            Assert.Equal(expected, actual.IsHoliday);
        }

        public static IEnumerable<object?[]> DayOfWeek_ToHolidayKind_TestData()
        {
            yield return new object?[] { DayOfWeek.Monday, HolidayKind.Shukkimbi, };
            yield return new object?[] { DayOfWeek.Tuesday, HolidayKind.Shukkimbi, };
            yield return new object?[] { DayOfWeek.Wednesday, HolidayKind.Shukkimbi, };
            yield return new object?[] { DayOfWeek.Thursday, HolidayKind.Shukkimbi, };
            yield return new object?[] { DayOfWeek.Friday, HolidayKind.Shukkimbi, };
            yield return new object?[] { DayOfWeek.Saturday, HolidayKind.Kyujitsu, };
            yield return new object?[] { DayOfWeek.Sunday, HolidayKind.Kyujitsu, };
            yield return new object?[] { null, null };
        }

        [Theory]
        [MemberData(nameof(DayOfWeek_ToHolidayKind_TestData))]
        public void DayOfWeek_ToHolidayKind_Test(DayOfWeek? week, HolidayKind? expected)
        {
            var actual = week?.ToHolidayKind();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object?[]> DateTime_IsWeekend_TestData()
        {
            yield return new object?[] { DateTime.Parse("2022/01/01"), true, }; // Saturday
            yield return new object?[] { DateTime.Parse("2022/01/02"), true, }; // Sunday
            yield return new object?[] { DateTime.Parse("2022/01/03"), false, }; // Monday
            yield return new object?[] { DateTime.Parse("2022/01/04"), false, }; // Tuesday
            yield return new object?[] { DateTime.Parse("2022/01/05"), false, }; // Wednesday
            yield return new object?[] { DateTime.Parse("2022/01/06"), false, }; // Thursday
            yield return new object?[] { DateTime.Parse("2022/01/07"), false, }; // Friday
            yield return new object?[] { null, null, };
        }

        [Theory]
        [MemberData(nameof(DateTime_IsWeekend_TestData))]
        public void DateTime_IsWeekend_Test(DateTime? dt, bool? expected)
        {
            var actual = dt?.IsWeekend();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object?[]> HolidayKind_ToEventSummary_TestData()
        {
            yield return new object?[] { HolidayKind.Shukkimbi, "HSC出勤日", };
            yield return new object?[] { HolidayKind.Kyujitsu, "HSC休日", };
            yield return new object?[] { HolidayKind.Shukujitsu, null, };
            yield return new object?[] { HolidayKind.YukyuKijumbi, "HSC夏季休暇", };
            yield return new object?[] { null, null, };
        }

        [Theory]
        [MemberData(nameof(HolidayKind_ToEventSummary_TestData))]
        public void HolidayKind_ToEventSummary_Test(HolidayKind? kind, string? expected)
        {
            var actual = kind?.ToEventSummary();
            Assert.Equal(expected, actual);
        }
    }
}