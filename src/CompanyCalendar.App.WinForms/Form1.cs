// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form1.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CompanyCalendar.App.WinForms
{
    /// <summary>
    /// メインフォームを表します。
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>google カレンダへのエクスポートを行う機能を表します。</summary>
        private readonly ICalendarExporter _calendarExporter = null!;

        /// <summary>iCalendar ファイルへのエクスポートを行う機能を表します。</summary>
        private readonly IIcsExporter _icsExporter = null!;

        /// <summary>CSV ファイルから読み込みを行う機能を表します。</summary>
        private readonly ICsvLoader _csvLoader = null!;

        /// <summary>DB から読み込みを行う機能を表します。</summary>
        private readonly IDbLoader _dbLoader = null!;

        /// <summary>
        /// <see cref="Form1" /> クラスの新しいインスタンスを生成します。
        /// </summary>
        /// <param name="calendarExporter"><see cref="ICalendarExporter" />。</param>
        /// <param name="icsExporter"><see cref="IIcsExporter" />。</param>
        /// <param name="csvLoader"><see cref="ICsvLoader" />。</param>
        /// <param name="dbLoader"><see cref="IDbLoader" />。</param>
        public Form1(
            ICalendarExporter calendarExporter,
            IIcsExporter icsExporter,
            ICsvLoader csvLoader,
            IDbLoader dbLoader)
            : this()
        {
            ArgumentNullException.ThrowIfNull(calendarExporter);
            ArgumentNullException.ThrowIfNull(icsExporter);
            ArgumentNullException.ThrowIfNull(csvLoader);
            ArgumentNullException.ThrowIfNull(dbLoader);

            this._calendarExporter = calendarExporter;
            this._icsExporter = icsExporter;
            this._csvLoader = csvLoader;
            this._dbLoader = dbLoader;
        }

        /// <summary>
        /// <see cref="Form1" /> クラスの新しいインスタンスを生成します。
        /// </summary>
        protected Form1()
        {
            this.InitializeComponent();
        }
    }
}
