// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompanyCalendarExtensions.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CompanyCalendar
{
    /// <summary>
    /// 拡張メソッドを提供します。
    /// </summary>
    public static class CompanyCalendarExtensions
    {
        /// <summary>
        /// 指定された <see cref="HolidayKind" /> が休日かどうかを判断します。
        /// </summary>
        /// <param name="kind"><see cref="HolidayKind" />。</param>
        /// <returns>休日の場合 <see langword="true" />。それ以外は <see langword="false" />。</returns>
        public static bool IsHoliday(this HolidayKind kind) =>
            kind switch
            {
                HolidayKind.Shukkimbi => false,
                _ => true,
            };

        /// <summary>
        /// 指定された <see cref="DateTime" /> が週末かどうかを判断します。
        /// </summary>
        /// <param name="dateTime"><see cref="DateTime" />。</param>
        /// <returns>週末の場合 <see langword="true" />。それ以外は <see langword="false" />。</returns>
        public static bool IsWeekend(this DateTime dateTime) => dateTime.DayOfWeek.ToHolidayKind().IsHoliday();

        /// <summary>
        /// 指定された <see cref="DayOfWeek" /> を <see cref="HolidayKind" /> へ変換します。
        /// </summary>
        /// <param name="week"><see cref="DayOfWeek" />。</param>
        /// <returns>変換結果の <see cref="HolidayKind" />。</returns>
        /// <exception cref="ArgumentOutOfRangeException">変換できません。</exception>
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
                _ => throw new ArgumentOutOfRangeException(nameof(week), week, null),
            };

        /// <summary>
        /// <see cref="HolidayKind" /> をイベント概要を示す文字列へ変換します。
        /// </summary>
        /// <param name="kind"><see cref="HolidayKind" />。</param>
        /// <param name="prefix">接頭語。</param>
        /// <returns>イベント概要を示す文字列。</returns>
        /// <exception cref="ArgumentOutOfRangeException">変換できません。</exception>
        public static string? ToEventSummary(this HolidayKind kind, string? prefix = null) =>
            kind switch
            {
                HolidayKind.Shukkimbi => $"{prefix}出勤日",
                HolidayKind.Kyujitsu => $"{prefix}休日",
                HolidayKind.Shukujitsu => null,
                HolidayKind.YukyuKijumbi => $"{prefix}夏季休暇",
                _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null),
            };

        /// <summary>
        /// 不定期なイベントかどうかを判断します。
        /// </summary>
        /// <param name="calendar"><see cref="DateTime" />。</param>
        /// <param name="holidayItem">
        /// <paramref name="calendar" /> に紐付けられた <see cref="HolidayItem" />。
        /// <paramref name="calendar" /> に紐付かない場合は <see langword="null" />。
        /// </param>
        /// <returns>不定期なイベントの場合 <see cref="HolidayKind" />。それ以外は <see langword="null" />。</returns>
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

        /// <summary>
        /// <see cref="HolidayItem" /> のコレクションから不定期イベントのタプルコレクションへ変換します。
        /// </summary>
        /// <param name="holidayItems"><see cref="HolidayItem" /> のコレクション。</param>
        /// <param name="lowerDate">開始日付。</param>
        /// <param name="upperDate">終了日付。</param>
        /// <returns><see cref="DateTime" /> と <see cref="HolidayKind" /> のタプルコレクション。</returns>
        public static IReadOnlyCollection<(DateTime Date, HolidayKind IrregularKind)> ToIrregularPairs(
            this IEnumerable<HolidayItem> holidayItems,
            DateTime? lowerDate = null,
            DateTime? upperDate = null) =>
            holidayItems.ToDictionary(item => item.Date).ToIrregularPairs(lowerDate, upperDate);

        /// <summary>
        /// <see cref="HolidayItem" /> のコレクションから不定期イベントのタプルコレクションへ変換します。
        /// </summary>
        /// <param name="holidayItemsMapping"><see cref="DateTime" /> をキーとした <see cref="HolidayItem" /> のコレクション。</param>
        /// <param name="lowerDate">開始日付。</param>
        /// <param name="upperDate">終了日付。</param>
        /// <returns><see cref="DateTime" /> と <see cref="HolidayKind" /> のタプルコレクション。</returns>
        public static IReadOnlyCollection<(DateTime Date, HolidayKind IrregularKind)> ToIrregularPairs(
            this IDictionary<DateTime, HolidayItem> holidayItemsMapping,
            DateTime? lowerDate = null,
            DateTime? upperDate = null)
        {
            ArgumentNullException.ThrowIfNull(holidayItemsMapping);

            var holidayItems = holidayItemsMapping.Values;
            var minDateItem = holidayItems.MinBy(item => item.Date);
            var maxDateItem = holidayItems.MaxBy(item => item.Date);
            var resolvedLowerDate = lowerDate ?? minDateItem?.Date;
            var resolvedUpperDate = upperDate ?? maxDateItem?.Date;
            return holidayItemsMapping.ToIrregularPairsCore(resolvedLowerDate, resolvedUpperDate);
        }

        /// <summary>
        /// <see cref="HolidayItem" /> のコレクションから不定期イベントのタプルコレクションへ変換します。
        /// </summary>
        /// <param name="holidayItemsMapping"><see cref="DateTime" /> をキーとした <see cref="HolidayItem" /> のコレクション。</param>
        /// <param name="lowerDate">開始日付。</param>
        /// <param name="upperDate">終了日付。</param>
        /// <returns><see cref="DateTime" /> と <see cref="HolidayKind" /> のタプルコレクション。</returns>
        private static IReadOnlyCollection<(DateTime Date, HolidayKind IrregularKind)> ToIrregularPairsCore(
            this IDictionary<DateTime, HolidayItem> holidayItemsMapping,
            DateTime? lowerDate,
            DateTime? upperDate)
        {
            var query = lowerDate.YieldDates(upperDate)
                .Select(calendar =>
                {
                    var holidayItem = holidayItemsMapping.TryGetValue(calendar, out var item) ? item : null;
                    var irregularKind = calendar.IsIrregular(holidayItem);
                    return new { Date = calendar, IrregularKind = irregularKind };
                })
                .Where(pair => pair.IrregularKind.HasValue)
                .Select(pair => (pair.Date, pair.IrregularKind!.Value))
                .ToList();
            return query.AsReadOnly();
        }
    }
}
