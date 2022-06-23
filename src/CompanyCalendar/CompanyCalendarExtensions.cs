namespace CompanyCalendar
{
    /// <summary>
    /// 拡張メソッドを提供します。
    /// </summary>
    public static class CompanyCalendarExtensions
    {
        public static bool IsHoliday(this HolidayKind kind) =>
            kind switch
            {
                HolidayKind.Shukkimbi => false,
                _ => true,
            };

        public static bool IsWeekend(this DateTime dateTime) =>
            dateTime.DayOfWeek.ToHolidayKind().IsHoliday();

        public static HolidayKind ToHolidayKind(this DayOfWeek week) =>
            week switch
            {
                DayOfWeek.Monday => HolidayKind.Shukkimbi,
                DayOfWeek.Tuesday => HolidayKind.Shukkimbi,
                DayOfWeek.Wednesday => HolidayKind.Shukkimbi,
                DayOfWeek.Thursday => HolidayKind.Shukkimbi,
                DayOfWeek.Friday => HolidayKind.Shukkimbi,
                DayOfWeek.Saturday => HolidayKind.Kyujitsu,
                DayOfWeek.Sunday => HolidayKind.Kyujitsu,
                _ => throw new ArgumentOutOfRangeException(nameof(week), week, null)
            };

        public static string? ToEventSummary(this HolidayKind kind) =>
            kind switch
            {
                HolidayKind.Shukkimbi => "HSC出勤日",
                HolidayKind.Kyujitsu => "HSC休日",
                HolidayKind.Shukujitsu => null,
                HolidayKind.YukyuKijumbi => "HSC夏季休暇",
                _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
            };
    }
}