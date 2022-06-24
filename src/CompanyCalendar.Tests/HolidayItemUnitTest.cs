using System;
using System.Collections.Generic;
using System.Linq;
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
            yield return new object?[] { HolidayKind.Shukkimbi, "出勤日", };
            yield return new object?[] { HolidayKind.Kyujitsu, "休日", };
            yield return new object?[] { HolidayKind.Shukujitsu, null, };
            yield return new object?[] { HolidayKind.YukyuKijumbi, "夏季休暇", };
            yield return new object?[] { null, null, };
        }

        [Theory]
        [MemberData(nameof(HolidayKind_ToEventSummary_TestData))]
        public void HolidayKind_ToEventSummary_Test(HolidayKind? kind, string? expected)
        {
            var actual = kind?.ToEventSummary();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object?[]> HolidayKind_ToEventSummaryWithPrefix_TestData()
        {
            var prefix = "PREFIX_";
            yield return new object?[] { HolidayKind.Shukkimbi, $"{prefix}出勤日", };
            yield return new object?[] { HolidayKind.Kyujitsu, $"{prefix}休日", };
            yield return new object?[] { HolidayKind.Shukujitsu, null, };
            yield return new object?[] { HolidayKind.YukyuKijumbi, $"{prefix}夏季休暇", };
            yield return new object?[] { null, null, };
        }

        [Theory]
        [MemberData(nameof(HolidayKind_ToEventSummaryWithPrefix_TestData))]
        public void HolidayKind_ToEventSummaryWithPrefix_Test(HolidayKind? kind, string? expected)
        {
            var actual = kind?.ToEventSummary("PREFIX_");
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object?[]> DateTime_IsIrregular_TestData()
        {
            static object?[] New(DateTime date, HolidayKind? kind, HolidayKind? irregularKind)
            {
                return kind == null
                    ? new object?[] { date, null, irregularKind } // 休日登録なし
                    : new object?[]
                        { date, new HolidayItem { Date = date, Kind = kind.Value }, irregularKind }; // 休日登録あり
            }

            var weekday = DateTime.Parse("2022/06/06");
            // 平日：登録＝なし　 → 出勤日、通常
            yield return New(weekday, null, null);
            // 平日：登録＝出勤日 → 出勤日、通常
            yield return New(weekday, HolidayKind.Shukkimbi, null);
            // 平日：登録＝休日　 → 休日、　不定期
            yield return New(weekday, HolidayKind.Kyujitsu, HolidayKind.Kyujitsu);
            // 平日：登録＝祝日　 → 祝日、　通常
            yield return New(weekday, HolidayKind.Shukujitsu, null);
            // 平日：登録＝有給　 → 有給、　不定期
            yield return New(weekday, HolidayKind.YukyuKijumbi, HolidayKind.YukyuKijumbi);

            var weekend = DateTime.Parse("2022/06/11");
            // 週末：登録＝なし　 → 出勤日、不定期
            yield return New(weekend, null, HolidayKind.Shukkimbi);
            // 週末：登録＝出勤日 → 出勤日、不定期
            yield return New(weekend, HolidayKind.Shukkimbi, HolidayKind.Shukkimbi);
            // 週末：登録＝休日　 → 休日、　通常
            yield return New(weekend, HolidayKind.Kyujitsu, null);
            // 週末：登録＝祝日　 → 祝日、　通常
            yield return New(weekend, HolidayKind.Shukujitsu, null);
            // 週末：登録＝有給　 → 有給、　不定期
            yield return New(weekend, HolidayKind.YukyuKijumbi, HolidayKind.YukyuKijumbi);
        }

        [Theory]
        [MemberData(nameof(DateTime_IsIrregular_TestData))]
        public void DateTime_IsIrregular_Test(DateTime? calendar, HolidayItem? holidayItem, HolidayKind? expected)
        {
            var actual = calendar?.IsIrregular(holidayItem);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Items_ToIrregularPairs_Test()
        {
            var items = new[]
            {
                new HolidayItem { Date = DateTime.Parse("2022/06/06"), Kind = HolidayKind.Kyujitsu },
            };
            var pairs = items.ToIrregularPairs();

            Assert.Single(pairs);
            Assert.Equal(HolidayKind.Kyujitsu, pairs.Single().IrregularKind);
        }
    }
}