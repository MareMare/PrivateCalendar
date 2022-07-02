// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICalendarExporter.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CompanyCalendar
{
    /// <summary>
    /// google カレンダへのエクスポートを行うインターフェイスを表します。
    /// </summary>
    public interface ICalendarExporter
    {
        /// <summary>
        /// 非同期操作として、google カレンダへエクスポートします。
        /// </summary>
        /// <param name="eventPairs">不定期イベントのコレクション。</param>
        /// <param name="taskCancellationToken"><see cref="CancellationToken" />。</param>
        /// <returns>完了を表す <see cref="Task" />。</returns>
        Task ExportAsync(
            IEnumerable<(DateTime date, string summary)> eventPairs,
            CancellationToken taskCancellationToken = default);
    }
}
