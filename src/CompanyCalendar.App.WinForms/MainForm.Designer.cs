namespace CompanyCalendar.App.WinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.radioButtonOfDb = new System.Windows.Forms.RadioButton();
            this.textBoxOfCsvPath = new System.Windows.Forms.TextBox();
            this.buttonToBrowseCsv = new System.Windows.Forms.Button();
            this.buttonToLoad = new System.Windows.Forms.Button();
            this.checkBoxOfGoogle = new System.Windows.Forms.CheckBox();
            this.checkBoxOfIcs = new System.Windows.Forms.CheckBox();
            this.buttonToSave = new System.Windows.Forms.Button();
            this.csvBrowsePanel = new System.Windows.Forms.Panel();
            this.icsBrowsePanel = new System.Windows.Forms.Panel();
            this.textBoxOfIcsPath = new System.Windows.Forms.TextBox();
            this.buttonToBrowseIcs = new System.Windows.Forms.Button();
            this.listBoxOfHolidayItems = new System.Windows.Forms.ListBox();
            this.loadButtonsPanel = new System.Windows.Forms.Panel();
            this.numericUpDownOfYear = new System.Windows.Forms.NumericUpDown();
            this.labelOfYear = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.radioButtonOfCsv = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOfSummaryPrefix = new System.Windows.Forms.TextBox();
            this.labelOfEventsInfo = new System.Windows.Forms.Label();
            this.csvBrowsePanel.SuspendLayout();
            this.icsBrowsePanel.SuspendLayout();
            this.loadButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOfYear)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonOfDb
            // 
            this.radioButtonOfDb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonOfDb.Location = new System.Drawing.Point(3, 32);
            this.radioButtonOfDb.Name = "radioButtonOfDb";
            this.radioButtonOfDb.Size = new System.Drawing.Size(102, 23);
            this.radioButtonOfDb.TabIndex = 1;
            this.radioButtonOfDb.TabStop = true;
            this.radioButtonOfDb.Text = "DBからの読込";
            this.radioButtonOfDb.UseVisualStyleBackColor = true;
            // 
            // textBoxOfCsvPath
            // 
            this.textBoxOfCsvPath.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxOfCsvPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOfCsvPath.Location = new System.Drawing.Point(0, 0);
            this.textBoxOfCsvPath.Name = "textBoxOfCsvPath";
            this.textBoxOfCsvPath.ReadOnly = true;
            this.textBoxOfCsvPath.Size = new System.Drawing.Size(334, 23);
            this.textBoxOfCsvPath.TabIndex = 0;
            // 
            // buttonToBrowseCsv
            // 
            this.buttonToBrowseCsv.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonToBrowseCsv.Location = new System.Drawing.Point(334, 0);
            this.buttonToBrowseCsv.Name = "buttonToBrowseCsv";
            this.buttonToBrowseCsv.Size = new System.Drawing.Size(26, 23);
            this.buttonToBrowseCsv.TabIndex = 1;
            this.buttonToBrowseCsv.Text = "...";
            this.buttonToBrowseCsv.UseVisualStyleBackColor = true;
            // 
            // buttonToLoad
            // 
            this.buttonToLoad.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonToLoad.Location = new System.Drawing.Point(95, 0);
            this.buttonToLoad.Name = "buttonToLoad";
            this.buttonToLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonToLoad.TabIndex = 2;
            this.buttonToLoad.Text = "抽出";
            this.buttonToLoad.UseVisualStyleBackColor = true;
            // 
            // checkBoxOfGoogle
            // 
            this.checkBoxOfGoogle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOfGoogle.Location = new System.Drawing.Point(3, 3);
            this.checkBoxOfGoogle.Name = "checkBoxOfGoogle";
            this.checkBoxOfGoogle.Size = new System.Drawing.Size(154, 23);
            this.checkBoxOfGoogle.TabIndex = 0;
            this.checkBoxOfGoogle.Text = "googleカレンダへの出力";
            this.checkBoxOfGoogle.UseVisualStyleBackColor = true;
            // 
            // checkBoxOfIcs
            // 
            this.checkBoxOfIcs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOfIcs.Location = new System.Drawing.Point(3, 32);
            this.checkBoxOfIcs.Name = "checkBoxOfIcs";
            this.checkBoxOfIcs.Size = new System.Drawing.Size(154, 23);
            this.checkBoxOfIcs.TabIndex = 1;
            this.checkBoxOfIcs.Text = "iCalendarファイルへの出力";
            this.checkBoxOfIcs.UseVisualStyleBackColor = true;
            // 
            // buttonToSave
            // 
            this.buttonToSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonToSave.Location = new System.Drawing.Point(396, 104);
            this.buttonToSave.Name = "buttonToSave";
            this.buttonToSave.Size = new System.Drawing.Size(75, 23);
            this.buttonToSave.TabIndex = 4;
            this.buttonToSave.Text = "出力";
            this.buttonToSave.UseVisualStyleBackColor = true;
            // 
            // csvBrowsePanel
            // 
            this.csvBrowsePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.csvBrowsePanel.Controls.Add(this.textBoxOfCsvPath);
            this.csvBrowsePanel.Controls.Add(this.buttonToBrowseCsv);
            this.csvBrowsePanel.Location = new System.Drawing.Point(111, 3);
            this.csvBrowsePanel.Name = "csvBrowsePanel";
            this.csvBrowsePanel.Size = new System.Drawing.Size(360, 23);
            this.csvBrowsePanel.TabIndex = 12;
            // 
            // icsBrowsePanel
            // 
            this.icsBrowsePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.icsBrowsePanel.Controls.Add(this.textBoxOfIcsPath);
            this.icsBrowsePanel.Controls.Add(this.buttonToBrowseIcs);
            this.icsBrowsePanel.Location = new System.Drawing.Point(163, 32);
            this.icsBrowsePanel.Name = "icsBrowsePanel";
            this.icsBrowsePanel.Size = new System.Drawing.Size(308, 23);
            this.icsBrowsePanel.TabIndex = 13;
            // 
            // textBoxOfIcsPath
            // 
            this.textBoxOfIcsPath.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxOfIcsPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOfIcsPath.Location = new System.Drawing.Point(0, 0);
            this.textBoxOfIcsPath.Name = "textBoxOfIcsPath";
            this.textBoxOfIcsPath.ReadOnly = true;
            this.textBoxOfIcsPath.Size = new System.Drawing.Size(282, 23);
            this.textBoxOfIcsPath.TabIndex = 0;
            // 
            // buttonToBrowseIcs
            // 
            this.buttonToBrowseIcs.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonToBrowseIcs.Location = new System.Drawing.Point(282, 0);
            this.buttonToBrowseIcs.Name = "buttonToBrowseIcs";
            this.buttonToBrowseIcs.Size = new System.Drawing.Size(26, 23);
            this.buttonToBrowseIcs.TabIndex = 1;
            this.buttonToBrowseIcs.Text = "...";
            this.buttonToBrowseIcs.UseVisualStyleBackColor = true;
            // 
            // listBoxOfHolidayItems
            // 
            this.listBoxOfHolidayItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxOfHolidayItems.FormattingEnabled = true;
            this.listBoxOfHolidayItems.ItemHeight = 15;
            this.listBoxOfHolidayItems.Location = new System.Drawing.Point(10, 133);
            this.listBoxOfHolidayItems.Name = "listBoxOfHolidayItems";
            this.listBoxOfHolidayItems.ScrollAlwaysVisible = true;
            this.listBoxOfHolidayItems.Size = new System.Drawing.Size(474, 68);
            this.listBoxOfHolidayItems.TabIndex = 1;
            // 
            // loadButtonsPanel
            // 
            this.loadButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButtonsPanel.Controls.Add(this.numericUpDownOfYear);
            this.loadButtonsPanel.Controls.Add(this.labelOfYear);
            this.loadButtonsPanel.Controls.Add(this.buttonToLoad);
            this.loadButtonsPanel.Location = new System.Drawing.Point(301, 74);
            this.loadButtonsPanel.Name = "loadButtonsPanel";
            this.loadButtonsPanel.Size = new System.Drawing.Size(170, 23);
            this.loadButtonsPanel.TabIndex = 13;
            // 
            // numericUpDownOfYear
            // 
            this.numericUpDownOfYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownOfYear.Location = new System.Drawing.Point(0, 0);
            this.numericUpDownOfYear.Maximum = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.numericUpDownOfYear.Minimum = new decimal(new int[] {
            2020,
            0,
            0,
            0});
            this.numericUpDownOfYear.Name = "numericUpDownOfYear";
            this.numericUpDownOfYear.Size = new System.Drawing.Size(54, 23);
            this.numericUpDownOfYear.TabIndex = 0;
            this.numericUpDownOfYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownOfYear.Value = new decimal(new int[] {
            2020,
            0,
            0,
            0});
            // 
            // labelOfYear
            // 
            this.labelOfYear.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelOfYear.Location = new System.Drawing.Point(54, 0);
            this.labelOfYear.Name = "labelOfYear";
            this.labelOfYear.Size = new System.Drawing.Size(41, 23);
            this.labelOfYear.TabIndex = 1;
            this.labelOfYear.Text = "年度";
            this.labelOfYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.csvBrowsePanel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.radioButtonOfDb, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.loadButtonsPanel, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.radioButtonOfCsv, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(474, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // radioButtonOfCsv
            // 
            this.radioButtonOfCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonOfCsv.Location = new System.Drawing.Point(3, 3);
            this.radioButtonOfCsv.Name = "radioButtonOfCsv";
            this.radioButtonOfCsv.Size = new System.Drawing.Size(102, 23);
            this.radioButtonOfCsv.TabIndex = 0;
            this.radioButtonOfCsv.TabStop = true;
            this.radioButtonOfCsv.Text = "CSVからの読込";
            this.radioButtonOfCsv.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.buttonToSave, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.icsBrowsePanel, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxOfIcs, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxOfGoogle, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.textBoxOfSummaryPrefix, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(10, 201);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(474, 130);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(3, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "イベント名の接頭語";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxOfSummaryPrefix
            // 
            this.textBoxOfSummaryPrefix.Location = new System.Drawing.Point(163, 61);
            this.textBoxOfSummaryPrefix.MaxLength = 10;
            this.textBoxOfSummaryPrefix.Name = "textBoxOfSummaryPrefix";
            this.textBoxOfSummaryPrefix.Size = new System.Drawing.Size(100, 23);
            this.textBoxOfSummaryPrefix.TabIndex = 3;
            this.textBoxOfSummaryPrefix.Text = "1234567890";
            // 
            // labelOfEventsInfo
            // 
            this.labelOfEventsInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelOfEventsInfo.Font = new System.Drawing.Font("Yu Gothic UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelOfEventsInfo.Location = new System.Drawing.Point(10, 110);
            this.labelOfEventsInfo.Name = "labelOfEventsInfo";
            this.labelOfEventsInfo.Size = new System.Drawing.Size(474, 23);
            this.labelOfEventsInfo.TabIndex = 2;
            this.labelOfEventsInfo.Text = "不定期イベントの件数";
            this.labelOfEventsInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 341);
            this.Controls.Add(this.listBoxOfHolidayItems);
            this.Controls.Add(this.labelOfEventsInfo);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(510, 380);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "PrivateCalendar";
            this.csvBrowsePanel.ResumeLayout(false);
            this.csvBrowsePanel.PerformLayout();
            this.icsBrowsePanel.ResumeLayout(false);
            this.icsBrowsePanel.PerformLayout();
            this.loadButtonsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOfYear)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private RadioButton radioButtonOfDb;
        private TextBox textBoxOfCsvPath;
        private Button buttonToBrowseCsv;
        private Button buttonToLoad;
        private CheckBox checkBoxOfGoogle;
        private CheckBox checkBoxOfIcs;
        private Button buttonToSave;
        private Panel csvBrowsePanel;
        private Panel icsBrowsePanel;
        private TextBox textBoxOfIcsPath;
        private Button buttonToBrowseIcs;
        private Panel loadButtonsPanel;
        private ListBox listBoxOfHolidayItems;
        private TableLayoutPanel tableLayoutPanel2;
        private RadioButton radioButtonOfCsv;
        private TableLayoutPanel tableLayoutPanel3;
        private NumericUpDown numericUpDownOfYear;
        private Label labelOfYear;
        private Label label1;
        private TextBox textBoxOfSummaryPrefix;
        private Label labelOfEventsInfo;
    }
}
