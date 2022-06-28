// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbLoader.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace CompanyCalendar.Importer.MsSql
{
    /// <summary>
    /// DB から読み込みを提供します。
    /// </summary>
    public class DbLoader : IDbLoader
    {
        /// <summary>データベースコンテキストを表します。</summary>
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// <see cref="DbLoader" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="dbContext">データベースコンテキスト。</param>
        public DbLoader(AppDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext);

            this._dbContext = dbContext;
        }

        /// <inheritdoc />
        public IAsyncEnumerable<HolidayItem> LoadAsync(
            DateTime? lowerDate = null,
            DateTime? upperDate = null,
            CancellationToken taskCancellationToken = default) =>
            this._dbContext.CompanyHolidays
                .AsNoTracking()
                .Where(item => !lowerDate.HasValue || item.Date >= lowerDate.Value)
                .Where(item => !upperDate.HasValue || item.Date <= upperDate.Value)
                .OrderBy(item => item.Date)
                .AsAsyncEnumerable();
    }
}
