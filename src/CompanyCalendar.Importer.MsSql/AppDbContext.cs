// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDbContext.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace CompanyCalendar.Importer.MsSql
{
    /// <summary>
    /// データベースコンテキストを表します。
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// <see cref="AppDbContext" /> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="options">オプション構成。</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 年間休日表テーブルを取得または設定します。
        /// </summary>
        /// <value>
        /// 値を表す <see cref="DbSet{CompanyHolidays}" /> 型。
        /// <para>年間休日表テーブル。既定値は null です。</para>
        /// </value>
        public virtual DbSet<HolidayItem> CompanyHolidays => this.Set<HolidayItem>();

        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            modelBuilder.Entity<HolidayItem>(entity =>
            {
                entity.ToTable("CompanyHolidays")
                    .HasComment("年間休日表")
                    .HasKey(e => e.Date);

                entity.Property(e => e.Date)
                    .HasComment("日付。\r\n休日、祝日、有給充当基準日に対する日付。")
                    .HasColumnName("Holiday")
                    .HasColumnOrder(0)
                    .IsRequired();

                entity.Property(e => e.Kind)
                    .HasComment("休日種別。\r\n0:出勤日、1:休日、2:祝日、4:有給充当基準日")
                    .HasColumnName("HolidayKind")
                    .HasConversion<short>()
                    .HasColumnOrder(1)
                    .IsRequired();

                entity.Property(e => e.Summary)
                    .HasComment("概要。")
                    .HasColumnName("Summary")
                    .HasColumnOrder(2);

                entity.Ignore(e => e.IsHoliday);
            });
        }
    }
}
