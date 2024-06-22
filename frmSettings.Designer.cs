namespace IPAddressChanger {
	partial class frmSettings {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.lblHideWhenMinimized = new Label();
			this.chkHideWhenMinimized = new CheckBox();
			this.label2 = new Label();
			this.cboShortcutDoubleClick = new ComboBox();
			this.lblStartMinimized = new Label();
			this.chkStartMinimized = new CheckBox();
			this.label1 = new Label();
			this.tableLayoutPanel4 = new TableLayoutPanel();
			this.cmdControlPanelBrowse = new Button();
			this.txtControlPanelFile = new TextBox();
			this.label3 = new Label();
			this.tableLayoutPanel5 = new TableLayoutPanel();
			this.chkCtrl = new CheckBox();
			this.chkAlt = new CheckBox();
			this.chkShift = new CheckBox();
			this.cboHotkey = new ComboBox();
			this.helpProvider1 = new HelpProvider();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Margin = new Padding(2);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
			this.tableLayoutPanel1.Size = new Size(560, 270);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
			this.tableLayoutPanel2.Controls.Add(this.cmdCancel, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.cmdOK, 1, 0);
			this.tableLayoutPanel2.Dock = DockStyle.Fill;
			this.tableLayoutPanel2.Location = new Point(2, 236);
			this.tableLayoutPanel2.Margin = new Padding(2);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new Size(556, 32);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = AnchorStyles.Top;
			this.cmdCancel.Location = new Point(447, 2);
			this.cmdCancel.Margin = new Padding(2);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new Size(78, 24);
			this.cmdCancel.TabIndex = 0;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += this.cmdCancel_Click;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = AnchorStyles.Top;
			this.cmdOK.Enabled = false;
			this.cmdOK.Location = new Point(307, 2);
			this.cmdOK.Margin = new Padding(2);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new Size(78, 24);
			this.cmdOK.TabIndex = 3;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += this.cmdOK_Click;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.66499F));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.3350143F));
			this.tableLayoutPanel3.Controls.Add(this.lblHideWhenMinimized, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.chkHideWhenMinimized, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.cboShortcutDoubleClick, 1, 2);
			this.tableLayoutPanel3.Controls.Add(this.lblStartMinimized, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.chkStartMinimized, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.label1, 0, 3);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 3);
			this.tableLayoutPanel3.Controls.Add(this.label3, 0, 4);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 1, 4);
			this.tableLayoutPanel3.Dock = DockStyle.Fill;
			this.tableLayoutPanel3.Location = new Point(2, 2);
			this.tableLayoutPanel3.Margin = new Padding(2);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 6;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new Size(556, 230);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// lblHideWhenMinimized
			// 
			this.lblHideWhenMinimized.AutoSize = true;
			this.lblHideWhenMinimized.Dock = DockStyle.Fill;
			this.lblHideWhenMinimized.Location = new Point(2, 0);
			this.lblHideWhenMinimized.Margin = new Padding(2, 0, 2, 0);
			this.lblHideWhenMinimized.Name = "lblHideWhenMinimized";
			this.lblHideWhenMinimized.Size = new Size(210, 32);
			this.lblHideWhenMinimized.TabIndex = 0;
			this.lblHideWhenMinimized.Text = "Hide when minimized";
			this.lblHideWhenMinimized.TextAlign = ContentAlignment.MiddleLeft;
			this.lblHideWhenMinimized.Click += this.lblHideWhenMinimized_Click;
			// 
			// chkHideWhenMinimized
			// 
			this.chkHideWhenMinimized.Anchor = AnchorStyles.Left;
			this.chkHideWhenMinimized.AutoSize = true;
			this.chkHideWhenMinimized.Location = new Point(216, 9);
			this.chkHideWhenMinimized.Margin = new Padding(2);
			this.chkHideWhenMinimized.Name = "chkHideWhenMinimized";
			this.chkHideWhenMinimized.Size = new Size(15, 14);
			this.chkHideWhenMinimized.TabIndex = 1;
			this.chkHideWhenMinimized.UseVisualStyleBackColor = true;
			this.chkHideWhenMinimized.CheckedChanged += this.chkHideWhenMinimized_CheckedChanged;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = DockStyle.Fill;
			this.label2.Location = new Point(2, 64);
			this.label2.Margin = new Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(210, 32);
			this.label2.TabIndex = 2;
			this.label2.Text = "Double clicking a shortcut will";
			this.label2.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// cboShortcutDoubleClick
			// 
			this.cboShortcutDoubleClick.Anchor = AnchorStyles.Left;
			this.cboShortcutDoubleClick.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboShortcutDoubleClick.FormattingEnabled = true;
			this.cboShortcutDoubleClick.Items.AddRange(new object[] { "Edit the shortcut", "Recall the shortcut" });
			this.cboShortcutDoubleClick.Location = new Point(216, 68);
			this.cboShortcutDoubleClick.Margin = new Padding(2);
			this.cboShortcutDoubleClick.Name = "cboShortcutDoubleClick";
			this.cboShortcutDoubleClick.Size = new Size(310, 23);
			this.cboShortcutDoubleClick.TabIndex = 2;
			this.cboShortcutDoubleClick.SelectedIndexChanged += this.cboShortcutDoubleClick_SelectedIndexChanged;
			// 
			// lblStartMinimized
			// 
			this.lblStartMinimized.AutoSize = true;
			this.lblStartMinimized.Dock = DockStyle.Fill;
			this.lblStartMinimized.Location = new Point(2, 32);
			this.lblStartMinimized.Margin = new Padding(2, 0, 2, 0);
			this.lblStartMinimized.Name = "lblStartMinimized";
			this.lblStartMinimized.Size = new Size(210, 32);
			this.lblStartMinimized.TabIndex = 3;
			this.lblStartMinimized.Text = "Start minimized";
			this.lblStartMinimized.TextAlign = ContentAlignment.MiddleLeft;
			this.lblStartMinimized.Click += this.lblStartMinimized_Click;
			// 
			// chkStartMinimized
			// 
			this.chkStartMinimized.Anchor = AnchorStyles.Left;
			this.chkStartMinimized.AutoSize = true;
			this.chkStartMinimized.Location = new Point(216, 41);
			this.chkStartMinimized.Margin = new Padding(2);
			this.chkStartMinimized.Name = "chkStartMinimized";
			this.chkStartMinimized.Size = new Size(15, 14);
			this.chkStartMinimized.TabIndex = 4;
			this.chkStartMinimized.UseVisualStyleBackColor = true;
			this.chkStartMinimized.CheckedChanged += this.chkStartMinimized_CheckedChanged;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = DockStyle.Fill;
			this.label1.Location = new Point(3, 96);
			this.label1.Name = "label1";
			this.label1.Size = new Size(208, 32);
			this.label1.TabIndex = 5;
			this.label1.Text = "Control Panel file";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90.93567F));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.064327F));
			this.tableLayoutPanel4.Controls.Add(this.cmdControlPanelBrowse, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.txtControlPanelFile, 0, 0);
			this.tableLayoutPanel4.Dock = DockStyle.Fill;
			this.tableLayoutPanel4.Location = new Point(214, 96);
			this.tableLayoutPanel4.Margin = new Padding(0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			this.tableLayoutPanel4.Size = new Size(342, 32);
			this.tableLayoutPanel4.TabIndex = 6;
			// 
			// cmdControlPanelBrowse
			// 
			this.cmdControlPanelBrowse.Dock = DockStyle.Fill;
			this.cmdControlPanelBrowse.Location = new Point(313, 3);
			this.cmdControlPanelBrowse.Name = "cmdControlPanelBrowse";
			this.cmdControlPanelBrowse.Size = new Size(26, 26);
			this.cmdControlPanelBrowse.TabIndex = 0;
			this.cmdControlPanelBrowse.Text = "...";
			this.cmdControlPanelBrowse.UseVisualStyleBackColor = true;
			this.cmdControlPanelBrowse.Click += this.cmdControlPanelBrowse_Click;
			// 
			// txtControlPanelFile
			// 
			this.txtControlPanelFile.Anchor = AnchorStyles.Left;
			this.txtControlPanelFile.Location = new Point(3, 4);
			this.txtControlPanelFile.Name = "txtControlPanelFile";
			this.txtControlPanelFile.Size = new Size(304, 23);
			this.txtControlPanelFile.TabIndex = 1;
			this.txtControlPanelFile.TextChanged += this.txtControlPanelFile_TextChanged;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = DockStyle.Fill;
			this.label3.Location = new Point(3, 128);
			this.label3.Name = "label3";
			this.label3.Size = new Size(208, 32);
			this.label3.TabIndex = 7;
			this.label3.Text = "Hotkey";
			this.label3.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 4;
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel5.Controls.Add(this.chkCtrl, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.chkAlt, 1, 0);
			this.tableLayoutPanel5.Controls.Add(this.chkShift, 2, 0);
			this.tableLayoutPanel5.Controls.Add(this.cboHotkey, 3, 0);
			this.tableLayoutPanel5.Dock = DockStyle.Fill;
			this.tableLayoutPanel5.Location = new Point(214, 128);
			this.tableLayoutPanel5.Margin = new Padding(0);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel5.Size = new Size(342, 32);
			this.tableLayoutPanel5.TabIndex = 8;
			// 
			// chkCtrl
			// 
			this.chkCtrl.Anchor = AnchorStyles.Left;
			this.chkCtrl.AutoSize = true;
			this.chkCtrl.Location = new Point(3, 6);
			this.chkCtrl.Name = "chkCtrl";
			this.chkCtrl.Size = new Size(45, 19);
			this.chkCtrl.TabIndex = 0;
			this.chkCtrl.Text = "Ctrl";
			this.chkCtrl.UseVisualStyleBackColor = true;
			this.chkCtrl.CheckedChanged += this.chkCtrl_CheckedChanged;
			// 
			// chkAlt
			// 
			this.chkAlt.Anchor = AnchorStyles.Left;
			this.chkAlt.AutoSize = true;
			this.chkAlt.Location = new Point(73, 6);
			this.chkAlt.Name = "chkAlt";
			this.chkAlt.Size = new Size(41, 19);
			this.chkAlt.TabIndex = 1;
			this.chkAlt.Text = "Alt";
			this.chkAlt.UseVisualStyleBackColor = true;
			this.chkAlt.CheckedChanged += this.chkAlt_CheckedChanged;
			// 
			// chkShift
			// 
			this.chkShift.Anchor = AnchorStyles.Left;
			this.chkShift.AutoSize = true;
			this.chkShift.Location = new Point(143, 6);
			this.chkShift.Name = "chkShift";
			this.chkShift.Size = new Size(50, 19);
			this.chkShift.TabIndex = 2;
			this.chkShift.Text = "Shift";
			this.chkShift.UseVisualStyleBackColor = true;
			this.chkShift.CheckedChanged += this.chkShift_CheckedChanged;
			// 
			// cboHotkey
			// 
			this.cboHotkey.Anchor = AnchorStyles.Left;
			this.cboHotkey.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboHotkey.FormattingEnabled = true;
			this.cboHotkey.Items.AddRange(new object[] { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" });
			this.cboHotkey.Location = new Point(213, 4);
			this.cboHotkey.Name = "cboHotkey";
			this.cboHotkey.Size = new Size(121, 23);
			this.cboHotkey.TabIndex = 3;
			this.cboHotkey.SelectedIndexChanged += this.cboHotkey_SelectedIndexChanged;
			// 
			// helpProvider1
			// 
			this.helpProvider1.HelpNamespace = "https://spectrumintegrators.github.io/IPAddressChanger/";
			// 
			// frmSettings
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new Size(560, 270);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.helpProvider1.SetHelpKeyword(this, "settings-window");
			this.helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
			this.Margin = new Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSettings";
			this.helpProvider1.SetShowHelp(this, true);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Settings";
			this.FormClosing += this.frmSettings_FormClosing;
			this.FormClosed += this.frmSettings_FormClosed;
			this.Load += this.frmSettings_Load;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private TableLayoutPanel tableLayoutPanel2;
		private Button cmdCancel;
		private Button cmdOK;
		private TableLayoutPanel tableLayoutPanel3;
		private Label lblHideWhenMinimized;
		private CheckBox chkHideWhenMinimized;
		private Label label2;
		private ComboBox cboShortcutDoubleClick;
		private Label lblStartMinimized;
		private CheckBox chkStartMinimized;
		private Label label1;
		private TableLayoutPanel tableLayoutPanel4;
		private Button cmdControlPanelBrowse;
		private TextBox txtControlPanelFile;
		private HelpProvider helpProvider1;
		private Label label3;
		private TableLayoutPanel tableLayoutPanel5;
		private CheckBox chkCtrl;
		private CheckBox chkAlt;
		private CheckBox chkShift;
		private ComboBox cboHotkey;
	}
}