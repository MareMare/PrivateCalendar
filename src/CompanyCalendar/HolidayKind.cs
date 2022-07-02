// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HolidayKind.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace CompanyCalendar
{
    /// <summary>
    /// 休日種別を示す列挙体を表します。
    /// </summary>
    [Flags]
    public enum HolidayKind
    {
        /// <summary>出勤日を表します。</summary>
        [SuppressMessage("Design", "CA1008:列挙型は 0 値を含んでいなければなりません", Justification = "取込元の値と意味に合わせます。")]
        Shukkimbi = 0,

        /// <summary>休日を表します。</summary>
        Kyujitsu = 1,

        /// <summary>祝日を表します。</summary>
        Shukujitsu = 2,

        /// <summary>有給充当基準日を表します。</summary>
        YukyuKijumbi = 4,
    }
}
