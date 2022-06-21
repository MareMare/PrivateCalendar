// ReSharper disable IdentifierTypo

namespace CompanyCalendar
{
    /// <summary>
    /// 休日種別を示す列挙体を表します。
    /// </summary>
    public enum HolidayKind
    {
        /// <summary>出勤日を表します。</summary>
        Shukkimbi = 0,

        /// <summary>休日を表します。</summary>
        Kyujitsu = 1,

        /// <summary>祝日を表します。</summary>
        Shukujitsu = 2,

        /// <summary>有給充当基準日を表します。</summary>
        YukyuKijumbi = 4,
    }
}