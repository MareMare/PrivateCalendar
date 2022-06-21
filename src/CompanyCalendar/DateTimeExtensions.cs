namespace CompanyCalendar
{
    /// <summary>
    /// <see cref="DateTime" /> クラスの拡張メソッドを提供します。
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// <see cref="DateTime" /> を日付文字列 (yyyyMMdd) へ変換します。
        /// </summary>
        /// <param name="dateTime">変換元の  <see cref="DateTime" />。</param>
        /// <returns>変換結果の日付文字列 (yyyyMMdd)。</returns>
        public static string ToStringAsYyyyMMdd(this DateTime dateTime) =>
            $"{dateTime:yyyyMMdd}";

        /// <summary>
        /// 指定された <see cref="DateTime" /> から当月 1 日の日付に変換します。
        /// </summary>
        /// <param name="dateTime">変換元の  <see cref="DateTime" />。</param>
        /// <returns>指定された <see cref="DateTime" /> の当月 1 日の日付。</returns>
        public static DateTime ToFirstDayInMonth(this DateTime dateTime) =>
            new(dateTime.Year, dateTime.Month, 1);

        /// <summary>
        /// 指定された <see cref="DateTime" /> から当月末日の日付に変換します。
        /// </summary>
        /// <param name="dateTime">変換元の  <see cref="DateTime" />。</param>
        /// <returns>指定された <see cref="DateTime" /> の当月末日の日付。</returns>
        public static DateTime ToLastDayInMonth(this DateTime dateTime) =>
            new(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));

        /// <summary>
        /// 指定された <see cref="DateTime" /> の翌日へ変換します。
        /// </summary>
        /// <param name="dateTime">変換元の  <see cref="DateTime" />。</param>
        /// <returns>指定された <see cref="DateTime" /> の翌日。</returns>
        public static DateTime ToNextDay(this DateTime dateTime) =>
            dateTime.AddDays(1);

        /// <summary>
        /// 指定された <see cref="DateTime" /> の翌分へ変換します。
        /// </summary>
        /// <param name="dateTime">変換元の  <see cref="DateTime" />。</param>
        /// <returns>指定された <see cref="DateTime" /> の翌分。</returns>
        public static DateTime ToNextMinute(this DateTime dateTime) =>
            dateTime.AddMinutes(1);

        /// <summary>
        /// ミリ秒を除外した (0 ミリ秒とした) 日時に変換します。
        /// </summary>
        /// <param name="dateTime">対象の日時。</param>
        /// <returns>ミリ秒を除外した日時。</returns>
        public static DateTime ToExceptedMilliseconds(this DateTime dateTime) =>
            new(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);

        /// <summary>
        /// 秒を除外した (0 秒 0 ミリ秒とした) 日時に変換します。
        /// </summary>
        /// <param name="dateTime">対象の日時。</param>
        /// <returns>秒を除外した日時。</returns>
        public static DateTime ToExceptedSeconds(this DateTime dateTime) =>
            new(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);

        /// <summary>
        /// 現在の日時にローカルタイムゾーンを設定します。
        /// </summary>
        /// <param name="dateTime">対象の日時。</param>
        /// <returns>ローカルタイムゾーンが設定された日時。</returns>
        public static DateTime SetLocalTimeZone(this DateTime dateTime) =>
            DateTime.SpecifyKind(dateTime, DateTimeKind.Local);

        public static IEnumerable<DateTime> YieldDates(this DateTime? lowerDate, DateTime? upperDate, Func<DateTime, DateTime>? getNextDate = null) =>
            lowerDate.HasValue && upperDate.HasValue
                ? lowerDate.Value.YieldDates(upperDate.Value, getNextDate)
                : Array.Empty<DateTime>();

        /// <summary>
        /// 指定された期間の各日付を列挙します。
        /// </summary>
        /// <param name="lowerDate">期間の開始日付。</param>
        /// <param name="upperDate">期間の終了日付。</param>
        /// <param name="getNextDate">次の日付を取得するメソッドのデリゲート。</param>
        /// <returns>日付の列挙。</returns>
        public static IEnumerable<DateTime> YieldDates(this DateTime lowerDate, DateTime upperDate, Func<DateTime, DateTime>? getNextDate = null)
        {
            var getNextDateAction = getNextDate ?? (date => date.AddDays(1));
            for (var date = lowerDate; date <= upperDate; date = getNextDateAction(date))
            {
                yield return date;
            }
        }
    }
}