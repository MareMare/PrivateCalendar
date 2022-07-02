// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDbLoader.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CompanyCalendar
{
    /// <summary>
    /// DB から読み込みを行うインターフェイスを表します。
    /// </summary>
    public interface IDbLoader
    {
        /// <summary>
        /// 非同期操作として、DB の所定テーブルを読み込みます。
        /// </summary>
        /// <param name="lowerDate">読み込む開始日付。</param>
        /// <param name="upperDate">読み込む終了日付。</param>
        /// <param name="taskCancellationToken"><see cref="CancellationToken" />。</param>
        /// <returns><see cref="HolidayItem" /> の非同期イテレーションを提供する列挙子。</returns>
        IAsyncEnumerable<HolidayItem> LoadAsync(
            DateTime? lowerDate = null,
            DateTime? upperDate = null,
            CancellationToken taskCancellationToken = default);
    }
}
