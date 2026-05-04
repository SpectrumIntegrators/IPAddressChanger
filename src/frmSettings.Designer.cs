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
			Label lblHideWhenMinimized;
			Label label2;
			Label lblStartMinimized;
			Label label1;
			Label label3;
			Label lblStartAtLogon;
			Label label4;
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.chkHideWhenMinimized = new CheckBox();
			this.cboShortcutDoubleClick = new ComboBox();
			this.chkStartMinimized = new CheckBox();
			this.tableLayoutPanel4 = new TableLayoutPanel();
			this.cmdControlPanelBrowse = new Button();
			this.txtControlPanelFile = new TextBox();
			this.tableLayoutPanel5 = new TableLayoutPanel();
			this.chkCtrl = new CheckBox();
			this.chkAlt = new CheckBox();
			this.chkShift = new CheckBox();
			this.cboHotkey = new ComboBox();
			this.chkStartAtLogon = new CheckBox();
			this.cboSaveLeases = new ComboBox();
			this.helpProvider1 = new HelpProvider();
			lblHideWhenMinimized = new Label();
			label2 = new Label();
			lblStartMinimized = new Label();
			label1 = new Label();
			label3 = new Label();
			lblStartAtLogon = new Label();
			label4 = new Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblHideWhenMinimized
			// 
			lblHideWhenMinimized.AutoSize = true;
			lblHideWhenMinimized.Dock = DockStyle.Fill;
			lblHideWhenMinimized.Location = new Point(2, 0);
			lblHideWhenMinimized.Margin = new Padding(2, 0, 2, 0);
			lblHideWhenMinimized.Name = "lblHideWhenMinimized";
			lblHideWhenMinimized.Size = new Size(210, 32);
			lblHideWhenMinimized.TabIndex = 0;
			lblHideWhenMinimized.Text = "Hide when minimized";
			lblHideWhenMinimized.TextAlign = ContentAlignment.MiddleLeft;
			lblHideWhenMinimized.Click += this.lblHideWhenMinimized_Click;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Dock = DockStyle.Fill;
			label2.Location = new Point(2, 64);
			label2.Margin = new Padding(2, 0, 2, 0);
			label2.Name = "label2";
			label2.Size = new Size(210, 32);
			label2.TabIndex = 2;
			label2.Text = "Double clicking a shortcut will";
			label2.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblStartMinimized
			// 
			lblStartMinimized.AutoSize = true;
			lblStartMinimized.Dock = DockStyle.Fill;
			lblStartMinimized.Location = new Point(2, 32);
			lblStartMinimized.Margin = new Padding(2, 0, 2, 0);
			lblStartMinimized.Name = "lblStartMinimized";
			lblStartMinimized.Size = new Size(210, 32);
			lblStartMinimized.TabIndex = 3;
			lblStartMinimized.Text = "Start minimized";
			lblStartMinimized.TextAlign = ContentAlignment.MiddleLeft;
			lblStartMinimized.Click += this.lblStartMinimized_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Dock = DockStyle.Fill;
			label1.Location = new Point(3, 96);
			label1.Name = "label1";
			label1.Size = new Size(208, 32);
			label1.TabIndex = 5;
			label1.Text = "Control Panel file";
			label1.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Dock = DockStyle.Fill;
			label3.Location = new Point(3, 128);
			label3.Name = "label3";
			label3.Size = new Size(208, 32);
			label3.TabIndex = 7;
			label3.Text = "Hotkey";
			label3.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblStartAtLogon
			// 
			lblStartAtLogon.AutoSize = true;
			lblStartAtLogon.Dock = DockStyle.Fill;
			lblStartAtLogon.Location = new Point(3, 160);
			lblStartAtLogon.Name = "lblStartAtLogon";
			lblStartAtLogon.Size = new Size(208, 32);
			lblStartAtLogon.TabIndex = 9;
			lblStartAtLogon.Text = "Start at log on";
			lblStartAtLogon.TextAlign = ContentAlignment.MiddleLeft;
			lblStartAtLogon.Click += this.lblStartAtLogon_Click;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Dock = DockStyle.Fill;
			label4.Location = new Point(3, 192);
			label4.Name = "label4";
			label4.Size = new Size(208, 38);
			label4.TabIndex = 11;
			label4.Text = "Save DHCP addresses";
			label4.TextAlign = ContentAlignment.MiddleLeft;
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
			this.tableLayoutPanel3.Controls.Add(lblHideWhenMinimized, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.chkHideWhenMinimized, 1, 0);
			this.tableLayoutPanel3.Controls.Add(label2, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.cboShortcutDoubleClick, 1, 2);
			this.tableLayoutPanel3.Controls.Add(lblStartMinimized, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.chkStartMinimized, 1, 1);
			this.tableLayoutPanel3.Controls.Add(label1, 0, 3);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 3);
			this.tableLayoutPanel3.Controls.Add(label3, 0, 4);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 1, 4);
			this.tableLayoutPanel3.Controls.Add(lblStartAtLogon, 0, 5);
			this.tableLayoutPanel3.Controls.Add(this.chkStartAtLogon, 1, 5);
			this.tableLayoutPanel3.Controls.Add(label4, 0, 6);
			this.tableLayoutPanel3.Controls.Add(this.cboSaveLeases, 1, 6);
			this.tableLayoutPanel3.Dock = DockStyle.Fill;
			this.tableLayoutPanel3.Location = new Point(2, 2);
			this.tableLayoutPanel3.Margin = new Padding(2);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 7;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new Size(556, 230);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// chkHideWhenMinimized
			// 
			this.chkHideWhenMinimized.Anchor = AnchorStyles.Left;
			this.chkHideWhenMinimized.AutoSize = true;
			this.helpProvider1.SetHelpKeyword(this.chkHideWhenMinimized, "hide-when-minimized");
			this.helpProvider1.SetHelpNavigator(this.chkHideWhenMinimized, HelpNavigator.Topic);
			this.chkHideWhenMinimized.Location = new Point(216, 9);
			this.chkHideWhenMinimized.Margin = new Padding(2);
			this.chkHideWhenMinimized.Name = "chkHideWhenMinimized";
			this.helpProvider1.SetShowHelp(this.chkHideWhenMinimized, true);
			this.chkHideWhenMinimized.Size = new Size(15, 14);
			this.chkHideWhenMinimized.TabIndex = 1;
			this.chkHideWhenMinimized.UseVisualStyleBackColor = true;
			this.chkHideWhenMinimized.CheckedChanged += this.chkHideWhenMinimized_CheckedChanged;
			// 
			// cboShortcutDoubleClick
			// 
			this.cboShortcutDoubleClick.Anchor = AnchorStyles.Left;
			this.cboShortcutDoubleClick.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboShortcutDoubleClick.FormattingEnabled = true;
			this.helpProvider1.SetHelpKeyword(this.cboShortcutDoubleClick, "double-clicking-a-shortcut-will");
			this.helpProvider1.SetHelpNavigator(this.cboShortcutDoubleClick, HelpNavigator.Topic);
			this.cboShortcutDoubleClick.Items.AddRange(new object[] { "Edit the shortcut", "Recall the shortcut" });
			this.cboShortcutDoubleClick.Location = new Point(216, 68);
			this.cboShortcutDoubleClick.Margin = new Padding(2);
			this.cboShortcutDoubleClick.Name = "cboShortcutDoubleClick";
			this.helpProvider1.SetShowHelp(this.cboShortcutDoubleClick, true);
			this.cboShortcutDoubleClick.Size = new Size(302, 23);
			this.cboShortcutDoubleClick.TabIndex = 2;
			this.cboShortcutDoubleClick.SelectedIndexChanged += this.cboShortcutDoubleClick_SelectedIndexChanged;
			// 
			// chkStartMinimized
			// 
			this.chkStartMinimized.Anchor = AnchorStyles.Left;
			this.chkStartMinimized.AutoSize = true;
			this.helpProvider1.SetHelpKeyword(this.chkStartMinimized, "start-minimized");
			this.helpProvider1.SetHelpNavigator(this.chkStartMinimized, HelpNavigator.Topic);
			this.chkStartMinimized.Location = new Point(216, 41);
			this.chkStartMinimized.Margin = new Padding(2);
			this.chkStartMinimized.Name = "chkStartMinimized";
			this.helpProvider1.SetShowHelp(this.chkStartMinimized, true);
			this.chkStartMinimized.Size = new Size(15, 14);
			this.chkStartMinimized.TabIndex = 4;
			this.chkStartMinimized.UseVisualStyleBackColor = true;
			this.chkStartMinimized.CheckedChanged += this.chkStartMinimized_CheckedChanged;
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
			this.helpProvider1.SetHelpKeyword(this.cmdControlPanelBrowse, "control-panel-file");
			this.helpProvider1.SetHelpNavigator(this.cmdControlPanelBrowse, HelpNavigator.Topic);
			this.cmdControlPanelBrowse.Location = new Point(313, 3);
			this.cmdControlPanelBrowse.Name = "cmdControlPanelBrowse";
			this.helpProvider1.SetShowHelp(this.cmdControlPanelBrowse, true);
			this.cmdControlPanelBrowse.Size = new Size(26, 26);
			this.cmdControlPanelBrowse.TabIndex = 0;
			this.cmdControlPanelBrowse.Text = "...";
			this.cmdControlPanelBrowse.UseVisualStyleBackColor = true;
			this.cmdControlPanelBrowse.Click += this.cmdControlPanelBrowse_Click;
			// 
			// txtControlPanelFile
			// 
			this.txtControlPanelFile.Anchor = AnchorStyles.Left;
			this.helpProvider1.SetHelpKeyword(this.txtControlPanelFile, "control-panel-file");
			this.helpProvider1.SetHelpNavigator(this.txtControlPanelFile, HelpNavigator.Topic);
			this.txtControlPanelFile.Location = new Point(3, 4);
			this.txtControlPanelFile.Name = "txtControlPanelFile";
			this.helpProvider1.SetShowHelp(this.txtControlPanelFile, true);
			this.txtControlPanelFile.Size = new Size(304, 23);
			this.txtControlPanelFile.TabIndex = 1;
			this.txtControlPanelFile.TextChanged += this.txtControlPanelFile_TextChanged;
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
			this.helpProvider1.SetHelpKeyword(this.chkCtrl, "hotkey");
			this.helpProvider1.SetHelpNavigator(this.chkCtrl, HelpNavigator.Topic);
			this.chkCtrl.Location = new Point(3, 6);
			this.chkCtrl.Name = "chkCtrl";
			this.helpProvider1.SetShowHelp(this.chkCtrl, true);
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
			this.helpProvider1.SetHelpKeyword(this.chkAlt, "hotkey");
			this.helpProvider1.SetHelpNavigator(this.chkAlt, HelpNavigator.Topic);
			this.chkAlt.Location = new Point(73, 6);
			this.chkAlt.Name = "chkAlt";
			this.helpProvider1.SetShowHelp(this.chkAlt, true);
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
			this.helpProvider1.SetHelpKeyword(this.chkShift, "hotkey");
			this.helpProvider1.SetHelpNavigator(this.chkShift, HelpNavigator.Topic);
			this.chkShift.Location = new Point(143, 6);
			this.chkShift.Name = "chkShift";
			this.helpProvider1.SetShowHelp(this.chkShift, true);
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
			this.helpProvider1.SetHelpKeyword(this.cboHotkey, "hotkey");
			this.helpProvider1.SetHelpNavigator(this.cboHotkey, HelpNavigator.Topic);
			this.cboHotkey.Items.AddRange(new object[] { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" });
			this.cboHotkey.Location = new Point(213, 4);
			this.cboHotkey.Name = "cboHotkey";
			this.helpProvider1.SetShowHelp(this.cboHotkey, true);
			this.cboHotkey.Size = new Size(121, 23);
			this.cboHotkey.TabIndex = 3;
			this.cboHotkey.SelectedIndexChanged += this.cboHotkey_SelectedIndexChanged;
			// 
			// chkStartAtLogon
			// 
			this.chkStartAtLogon.Anchor = AnchorStyles.Left;
			this.chkStartAtLogon.AutoSize = true;
			this.helpProvider1.SetHelpKeyword(this.chkStartAtLogon, "start-at-log-on");
			this.helpProvider1.SetHelpNavigator(this.chkStartAtLogon, HelpNavigator.Topic);
			this.chkStartAtLogon.Location = new Point(217, 169);
			this.chkStartAtLogon.Name = "chkStartAtLogon";
			this.helpProvider1.SetShowHelp(this.chkStartAtLogon, true);
			this.chkStartAtLogon.Size = new Size(15, 14);
			this.chkStartAtLogon.TabIndex = 10;
			this.chkStartAtLogon.UseVisualStyleBackColor = true;
			this.chkStartAtLogon.CheckedChanged += this.chkStartAtLogon_CheckedChanged;
			// 
			// cboSaveLeases
			// 
			this.cboSaveLeases.Anchor = AnchorStyles.Left;
			this.cboSaveLeases.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboSaveLeases.FormattingEnabled = true;
			this.cboSaveLeases.Items.AddRange(new object[] { "Only save reserved addresses", "Save reserved and automatic addresses" });
			this.cboSaveLeases.Location = new Point(216, 199);
			this.cboSaveLeases.Margin = new Padding(2);
			this.cboSaveLeases.Name = "cboSaveLeases";
			this.cboSaveLeases.Size = new Size(302, 23);
			this.cboSaveLeases.TabIndex = 12;
			this.cboSaveLeases.SelectedIndexChanged += this.cboSaveLeases_SelectedIndexChanged;
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
		private CheckBox chkHideWhenMinimized;
		private ComboBox cboShortcutDoubleClick;
		private CheckBox chkStartMinimized;
		private TableLayoutPanel tableLayoutPanel4;
		private Button cmdControlPanelBrowse;
		private TextBox txtControlPanelFile;
		private HelpProvider helpProvider1;
		private TableLayoutPanel tableLayoutPanel5;
		private CheckBox chkCtrl;
		private CheckBox chkAlt;
		private CheckBox chkShift;
		private ComboBox cboHotkey;
		private CheckBox chkStartAtLogon;
		private ComboBox cboSaveLeases;
	}
}