// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="MareMare">
// Copyright © 2022 MareMare. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using CompanyCalendar.App.WinForms.Progress;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyCalendar.App.WinForms
{
    /// <summary>
    /// メインフォームを表します。
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>非同期操作の実行時間としての最低時間を表します。</summary>
        private static readonly TimeSpan DelayTimeSpan = TimeSpan.FromSeconds(2);

        /// <summary><see cref="IServiceProvider" /> を表します。</summary>
        private readonly IServiceProvider _serviceProvider = null!;

        /// <summary>google カレンダへのエクスポートを行う機能を表します。</summary>
        private readonly ICalendarExporter _calendarExporter = null!;

        /// <summary>iCalendar ファイルへのエクスポートを行う機能を表します。</summary>
        private readonly IIcsExporter _icsExporter = null!;

        /// <summary>CSV ファイルから読み込みを行う機能を表します。</summary>
        private readonly ICsvLoader _csvLoader = null!;

        /// <summary>DB から読み込みを行う機能を表します。</summary>
        private readonly IDbLoader _dbLoader = null!;

        /// <summary>
        /// <see cref="MainForm" /> クラスの新しいインスタンスを生成します。
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider" />。</param>
        /// <param name="calendarExporter"><see cref="ICalendarExporter" />。</param>
        /// <param name="icsExporter"><see cref="IIcsExporter" />。</param>
        /// <param name="csvLoader"><see cref="ICsvLoader" />。</param>
        /// <param name="dbLoader"><see cref="IDbLoader" />。</param>
        public MainForm(
            IServiceProvider serviceProvider,
            ICalendarExporter calendarExporter,
            IIcsExporter icsExporter,
            ICsvLoader csvLoader,
            IDbLoader dbLoader)
            : this()
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);
            ArgumentNullException.ThrowIfNull(calendarExporter);
            ArgumentNullException.ThrowIfNull(icsExporter);
            ArgumentNullException.ThrowIfNull(csvLoader);
            ArgumentNullException.ThrowIfNull(dbLoader);

            this._serviceProvider = serviceProvider;
            this._calendarExporter = calendarExporter;
            this._icsExporter = icsExporter;
            this._csvLoader = csvLoader;
            this._dbLoader = dbLoader;
        }

        /// <summary>
        /// <see cref="MainForm" /> クラスの新しいインスタンスを生成します。
        /// </summary>
        protected MainForm()
        {
            this.InitializeComponent();
            this.InitializeHandlers();
            this.SetTextBoxTexts();
            this.SetControlsEnabled();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ReleaseHandlers();
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// 年度の日付範囲を求めます。
        /// </summary>
        /// <param name="year">年度。</param>
        /// <returns>年度の開始日付と終了日付のタプル。</returns>
        private static (DateTime lowerDate, DateTime upperDate) GetDateRangeOf(int year)
        {
            // {yyyy}/04/01 ～ {yyyy + 1}/03/31
            var lowerDate = new DateTime(year, 4, 1);
            var upperDate = lowerDate.AddYears(1).AddDays(-1);
            return (lowerDate, upperDate);
        }

        /// <summary>
        /// イベントハンドラを登録します。
        /// </summary>
        private void InitializeHandlers()
        {
            this.radioButtonOfCsv.CheckedChanged += this.RadioButtonOfCsvOnCheckedChanged;
            this.checkBoxOfIcs.CheckedChanged += this.CheckBoxOfIcsOnCheckedChanged;
            this.textBoxOfCsvPath.TextChanged += this.TextBoxOfCsvPathOnTextChanged;
            this.textBoxOfIcsPath.TextChanged += this.TextBoxOfIcsPathOnTextChanged;

            this.buttonToBrowseCsv.Click += this.ButtonToBrowseCsvOnClick;
            this.buttonToBrowseIcs.Click += this.ButtonToBrowseIcsOnClick;
            this.buttonToLoad.Click += this.ButtonToLoadOnClick;
            this.buttonToSave.Click += this.ButtonToSaveOnClick;
        }

        /// <summary>
        /// イベントハンドラを登録解除します。
        /// </summary>
        private void ReleaseHandlers()
        {
            this.radioButtonOfCsv.CheckedChanged -= this.RadioButtonOfCsvOnCheckedChanged;
            this.checkBoxOfIcs.CheckedChanged -= this.CheckBoxOfIcsOnCheckedChanged;
            this.textBoxOfCsvPath.TextChanged -= this.TextBoxOfCsvPathOnTextChanged;
            this.textBoxOfIcsPath.TextChanged -= this.TextBoxOfIcsPathOnTextChanged;

            this.buttonToBrowseCsv.Click -= this.ButtonToBrowseCsvOnClick;
            this.buttonToBrowseIcs.Click -= this.ButtonToBrowseIcsOnClick;
            this.buttonToLoad.Click -= this.ButtonToLoadOnClick;
            this.buttonToSave.Click -= this.ButtonToSaveOnClick;
        }

        /// <summary>
        /// <see cref="Control.Enabled" /> を設定します。
        /// </summary>
        private void SetControlsEnabled()
        {
            var enabled1 = this.radioButtonOfCsv.Checked;
            var hasPath1 = !string.IsNullOrEmpty(this.textBoxOfCsvPath.Text);
            this.textBoxOfCsvPath.Enabled = enabled1;
            this.buttonToBrowseCsv.Enabled = enabled1;
            this.buttonToLoad.Enabled = hasPath1;

            var enabled2 = this.checkBoxOfIcs.Checked;
            var hasPath2 = !string.IsNullOrEmpty(this.textBoxOfIcsPath.Text);
            this.textBoxOfIcsPath.Enabled = enabled2;
            this.buttonToBrowseIcs.Enabled = enabled2;
            this.buttonToSave.Enabled = hasPath2;
        }

        /// <summary>
        /// 各ファイルパスのテキストボックスを設定します。
        /// </summary>
        private void SetTextBoxTexts()
        {
            this.textBoxOfCsvPath.Text = string.Empty;
            this.textBoxOfIcsPath.Text = string.Empty;
            this.textBoxOfSummaryPrefix.Text = string.Empty;
        }

        /// <summary>
        /// 不定期イベントのコレクションを取得します。
        /// </summary>
        /// <param name="prefix">イベント概要の接頭語。</param>
        /// <returns>不定期イベントのコレクション。</returns>
        private IReadOnlyCollection<(DateTime date, string summary)> GetEventPairs(string prefix)
        {
            var items = this.listBoxOfHolidayItems.Items.OfType<IrregularDisplayItem>().ToArray();
            var pairs = items
                .Select(item => new { item.Date, Summary = item.IrregularKind.ToEventSummary(prefix) })
                .Where(item => item.Summary != null)
                .Select(item => (date: item.Date, summary: item.Summary!))
                .ToList()
                .AsReadOnly();
            return pairs;
        }

        /// <summary>
        /// 非同期操作として、読み込みを行います。
        /// </summary>
        /// <param name="actionTask">読み込みを行うメソッドのデリゲート。</param>
        /// <returns>読み込み結果のコレクション。</returns>
        private async Task<IReadOnlyCollection<HolidayItem>> LoadWithProgressAsync(
            Func<IAsyncEnumerable<HolidayItem>> actionTask)
        {
            var asyncScope = this._serviceProvider.CreateAsyncScope();
            await using var asyncScopeDisposable = asyncScope.ConfigureAwait(true);

            var progress = asyncScope.ServiceProvider.UseProgressForm(this, form => form.Title = "読み込み");
            await using var progressDisposable = progress.ConfigureAwait(true);

            progress.Reporter.ReportStarting("読み込み中です。しばらくお待ちください。");
            try
            {
                var holidayItems = await actionTask().ToListAsync().ConfigureAwait(true);
                await progress.Reporter.ReportCompletedAsync("読み込みが完了しました。").ConfigureAwait(true);
                return holidayItems.AsReadOnly();
            }
            catch
            {
                await progress.Reporter
                    .ReportFailedAsync("読み込み中に例外が発生しました。", MainForm.DelayTimeSpan)
                    .ConfigureAwait(true);
            }

            return Array.Empty<HolidayItem>();
        }

        /// <summary>
        /// 非同期操作として、書き込みを行います。
        /// </summary>
        /// <param name="tasks">書き込みを行う <see cref="Task" /> の配列。</param>
        /// <returns>完了を表す <see cref="ValueTask" />。</returns>
        private async ValueTask SaveWithProgressAsync(Task[] tasks)
        {
            var asyncScope = this._serviceProvider.CreateAsyncScope();
            await using var asyncScopeDisposable = asyncScope.ConfigureAwait(true);

            var progress = asyncScope.ServiceProvider.UseProgressForm(this, form => form.Title = "書き込み");
            await using var progressDisposable = progress.ConfigureAwait(true);

            progress.Reporter.ReportStarting("書き込み中です。しばらくお待ちください。");
            try
            {
                await Task.WhenAll(tasks).ConfigureAwait(true);
                await progress.Reporter.ReportCompletedAsync("書き込みが完了しました。").ConfigureAwait(true);
            }
            catch
            {
                await progress.Reporter
                    .ReportFailedAsync("書き込み中に例外が発生しました。", MainForm.DelayTimeSpan)
                    .ConfigureAwait(true);
            }
        }

        /// <summary>
        /// [CSVからの読込] ラジオボタンがクリックされた場合に発生するイベントのイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベントのソースを表す <see cref="object" />。</param>
        /// <param name="e">イベントデータを格納している <see cref="EventArgs" />。</param>
        private void RadioButtonOfCsvOnCheckedChanged(object? sender, EventArgs e) => this.SetControlsEnabled();

        /// <summary>
        /// [iCalendarファイルへの出力] チェックボックスがクリックされた場合に発生するイベントのイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベントのソースを表す <see cref="object" />。</param>
        /// <param name="e">イベントデータを格納している <see cref="EventArgs" />。</param>
        private void CheckBoxOfIcsOnCheckedChanged(object? sender, EventArgs e) => this.SetControlsEnabled();

        /// <summary>
        /// [CSVファイルパス] テキストボックスのテキストが変更されたされた場合に発生するイベントのイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベントのソースを表す <see cref="object" />。</param>
        /// <param name="e">イベントデータを格納している <see cref="EventArgs" />。</param>
        private void TextBoxOfCsvPathOnTextChanged(object? sender, EventArgs e) => this.SetControlsEnabled();

        /// <summary>
        /// [ICSファイルパス] テキストボックスのテキストが変更されたされた場合に発生するイベントのイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベントのソースを表す <see cref="object" />。</param>
        /// <param name="e">イベントデータを格納している <see cref="EventArgs" />。</param>
        private void TextBoxOfIcsPathOnTextChanged(object? sender, EventArgs e) => this.SetControlsEnabled();

        /// <summary>
        /// [CSV参照] ボタンがクリックされた場合に発生するイベントのイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベントのソースを表す <see cref="object" />。</param>
        /// <param name="e">イベントデータを格納している <see cref="EventArgs" />。</param>
        private void ButtonToBrowseCsvOnClick(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
#pragma warning disable CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
            dialog.Filter = @"CSV(*.csv)|*.csv";
            dialog.Title = @"CSVファイルを選択してください。";
#pragma warning restore CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.ReadOnlyChecked = true;
            dialog.RestoreDirectory = true;
            dialog.ShowReadOnly = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxOfCsvPath.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// [ISC参照] ボタンがクリックされた場合に発生するイベントのイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベントのソースを表す <see cref="object" />。</param>
        /// <param name="e">イベントデータを格納している <see cref="EventArgs" />。</param>
        private void ButtonToBrowseIcsOnClick(object? sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog();
#pragma warning disable CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
            dialog.Filter = @"ICS(*.ics)|*.ics";
            dialog.Title = @"ICSファイルを選択してください。";
#pragma warning restore CA1303 // ローカライズされるパラメーターとしてリテラルを渡さない
            dialog.CreatePrompt = false;
            dialog.OverwritePrompt = true;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxOfIcsPath.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// [抽出] ボタンがクリックされた場合に発生するイベントのイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベントのソースを表す <see cref="object" />。</param>
        /// <param name="e">イベントデータを格納している <see cref="EventArgs" />。</param>
        private async void ButtonToLoadOnClick(object? sender, EventArgs e)
        {
            this.buttonToLoad.Enabled = false;

            var path = this.textBoxOfCsvPath.Text;
            var (lowerDate, upperDate) = MainForm.GetDateRangeOf((int)this.numericUpDownOfYear.Value);

            Func<IAsyncEnumerable<HolidayItem>> getHolidayItems = this.radioButtonOfCsv.Checked
                ? () => this._csvLoader.LoadAsync(path, lowerDate, upperDate)
                : () => this._dbLoader.LoadAsync(lowerDate, upperDate);
            var holidayItems = await this.LoadWithProgressAsync(getHolidayItems).ConfigureAwait(true);

            var irregularItems = holidayItems
                .ToIrregularPairs()
                .Select(pair =>
                    new IrregularDisplayItem(pair.Date, pair.IrregularKind))
                .OfType<object>()
                .ToArray();

            this.listBoxOfHolidayItems.Items.Clear();
            this.listBoxOfHolidayItems.Items.AddRange(irregularItems);
            this.labelOfEventsInfo.Text = $@"不定期イベントの件数:{irregularItems.Length}";

            this.buttonToLoad.Enabled = true;
        }

        /// <summary>
        /// [出力] ボタンがクリックされた場合に発生するイベントのイベントハンドラです。
        /// </summary>
        /// <param name="sender">イベントのソースを表す <see cref="object" />。</param>
        /// <param name="e">イベントデータを格納している <see cref="EventArgs" />。</param>
        private async void ButtonToSaveOnClick(object? sender, EventArgs e)
        {
            this.buttonToSave.Enabled = false;

            var path = this.textBoxOfIcsPath.Text;
            var prefix = this.textBoxOfSummaryPrefix.Text.Trim();
            var eventPairs = this.GetEventPairs(prefix);

            var tasks = new List<Task>();
            if (this.checkBoxOfGoogle.Checked)
            {
                tasks.Add(this._calendarExporter.ExportAsync(eventPairs));
            }

            if (this.checkBoxOfIcs.Checked)
            {
                tasks.Add(this._icsExporter.ExportAsync(path, eventPairs));
            }

            await this.SaveWithProgressAsync(tasks.ToArray()).ConfigureAwait(true);

            this.buttonToSave.Enabled = true;
        }

        /// <summary>
        /// リストボックス項目を表します。
        /// </summary>
        private class IrregularDisplayItem
        {
            /// <summary>
            /// <see cref="IrregularDisplayItem" /> クラスの新しいインスタンスを初期化します。
            /// </summary>
            /// <param name="date"><see cref="DateTime" />。</param>
            /// <param name="irregularKind"><see cref="HolidayKind" />。</param>
            public IrregularDisplayItem(DateTime date, HolidayKind irregularKind)
            {
                this.Date = date;
                this.IrregularKind = irregularKind;
            }

            /// <summary>
            /// 日付を取得します。
            /// </summary>
            /// <value>
            /// 値を表す <see cref="DateTime" /> 型。
            /// <para>日付。既定値は <see cref="DateTime.MinValue" /> です。</para>
            /// </value>
            public DateTime Date { get; }

            /// <summary>
            /// <see cref="HolidayKind" /> を取得します。
            /// </summary>
            /// <value>
            /// 値を表す <see cref="HolidayKind" /> 型。
            /// <para><see cref="HolidayKind" /> 。既定値は <see cref="HolidayKind.Shukkimbi" /> です。</para>
            /// </value>
            public HolidayKind IrregularKind { get; }

            /// <inheritdoc />
            public override string ToString() => $"{this.Date:yyyy/MM/dd ggg}: {this.IrregularKind.ToEventSummary()}";
        }
    }
}
