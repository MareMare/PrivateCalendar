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

        public static string? ToEventSummary(this HolidayKind kind, string? prefix = null) =>
            kind switch
            {
                HolidayKind.Shukkimbi => $"{prefix}出勤日",
                HolidayKind.Kyujitsu => $"{prefix}休日",
                HolidayKind.Shukujitsu => null,
                HolidayKind.YukyuKijumbi => $"{prefix}夏季休暇",
                _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
            };

        public static HolidayKind? IsIrregular(this DateTime calendar, HolidayItem? holidayItem)
        {
            // ----------------------------------------
            // カレンダ   休日設定  判定      不定期？
            // ----------------------------------------
            // 平日       なし      出勤日    いいえ
            // 平日       休日      休日      はい
            // 平日       祝日      休日      いいえ
            // 平日       有給      休日      はい
            // ----------------------------------------
            // 週末       なし      出勤日    はい
            // 週末       あり      休日      いいえ
            // ----------------------------------------
            var isWeekend = calendar.IsWeekend();
            if (!isWeekend)
            {
                // 平日での検出を行います。
                return holidayItem?.Kind switch
                {
                    HolidayKind.Kyujitsu => HolidayKind.Kyujitsu,
                    HolidayKind.YukyuKijumbi => HolidayKind.YukyuKijumbi,
                    _ => null,
                };
            }

            // 週末での検出を行います。
            return holidayItem?.Kind switch
            {
                null => HolidayKind.Shukkimbi,
                HolidayKind.Shukkimbi => HolidayKind.Shukkimbi,
                HolidayKind.YukyuKijumbi => HolidayKind.YukyuKijumbi,
                _ => null,
            };
        }

        public static ICollection<(DateTime Date, HolidayKind IrregularKind)> ToIrregularPairs(
            this ICollection<HolidayItem> holidayItems,
            DateTime? lowerDate = null,
            DateTime? upperDate = null) =>
            holidayItems.ToDictionary(item => item.Date).ToIrregularPairs(lowerDate, upperDate);

        public static ICollection<(DateTime Date, HolidayKind IrregularKind)> ToIrregularPairs(
            this IDictionary<DateTime, HolidayItem> holidayItemsMapping,
            DateTime? lowerDate = null,
            DateTime? upperDate = null)
        {
            var holidayItems = holidayItemsMapping.Values;
            var minDateItem = holidayItems.MinBy(item => item.Date);
            var maxDateItem = holidayItems.MaxBy(item => item.Date);
            var resolvedLowerDate = lowerDate ?? minDateItem?.Date;
            var resolvedUpperDate = upperDate ?? maxDateItem?.Date;
            return holidayItemsMapping.ToIrregularPairsCore(resolvedLowerDate, resolvedUpperDate);
        }

        private static ICollection<(DateTime Date, HolidayKind IrregularKind)> ToIrregularPairsCore(
            this IDictionary<DateTime, HolidayItem> holidayItemsMapping,
            DateTime? lowerDate,
            DateTime? upperDate)
        {
            var query = lowerDate.YieldDates(upperDate)
                .Select(calendar =>
                {
                    var holidayItem = holidayItemsMapping.TryGetValue(calendar, out var item) ? item : null;
                    var irregularKind = calendar.IsIrregular(holidayItem);
                    return new { Date = calendar, IrregularKind = irregularKind, };
                })
                .Where(pair => pair.IrregularKind.HasValue)
                .Select(pair => (pair.Date, pair.IrregularKind!.Value))
                .ToArray();
            return query;
        }
    }
}