using System.Diagnostics;

namespace CompanyCalendar
{
    /// <summary>
    /// 休日項目を表します。
    /// </summary>
    [DebuggerDisplay("{Date}(Kind={Kind}) {Summary}")]
    public class HolidayItem
    {
        /// <summary>
        /// 日付を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="DateTime" /> 型。
        /// <para>休日、祝日、有給充当基準日に対する日付。既定値は <see cref="DateTime.MinValue" /> です。</para>
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// 休日種別を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="HolidayKind" /> 型。
        /// <para>休日種別。0:出勤日、1:休日、2:祝日、4:有給充当基準日。既定値は <see cref="HolidayKind.Shukkimbi" /> です。</para>
        /// </value>
        public HolidayKind Kind { get; set; }

        /// <summary>
        /// 概要を取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="string" /> 型。
        /// <para>概要。既定値は <see langword="null" /> です。</para>
        /// </value>
        public string? Summary { get; set; }

        /// <summary>
        /// 休日として扱うかどうかを取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="bool" /> 型。
        /// <para>休日の場合 <see langword="true" />。それ以外は <see langword="false" />。</para>
        /// </value>
        public bool IsHoliday
        {
            get => this.Kind.IsHoliday();
        }
    }
}
