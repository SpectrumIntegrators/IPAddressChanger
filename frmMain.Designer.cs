﻿namespace IPAddressChanger {
	partial class frmMain {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			lsbAdapters = new ListBox();
			ssStatus = new StatusStrip();
			tsslStatus = new ToolStripStatusLabel();
			splitContainer1 = new SplitContainer();
			splitContainer3 = new SplitContainer();
			tableLayoutPanel3 = new TableLayoutPanel();
			label6 = new Label();
			tableLayoutPanel2 = new TableLayoutPanel();
			lsbShortcuts = new ListBox();
			label5 = new Label();
			tsAdapters = new ToolStrip();
			tsbRefresh = new ToolStripButton();
			tsbOnlineOnly = new ToolStripButton();
			toolStripSeparator3 = new ToolStripSeparator();
			tsbNewShortcut = new ToolStripButton();
			tsbDeleteShortcut = new ToolStripButton();
			tsbEditShortcut = new ToolStripButton();
			tsbRecallShortcut = new ToolStripButton();
			toolStripSeparator4 = new ToolStripSeparator();
			tsbSettings = new ToolStripButton();
			tsbDebug = new ToolStripButton();
			splitContainer2 = new SplitContainer();
			tableLayoutPanel1 = new TableLayoutPanel();
			label4 = new Label();
			txtDeviceID = new TextBox();
			txtDriver = new TextBox();
			label1 = new Label();
			label2 = new Label();
			txtHardwareAddress = new TextBox();
			txtSpeed = new TextBox();
			label3 = new Label();
			lsvAddresses = new ListView();
			chAddress = new ColumnHeader();
			chPrefixLength = new ColumnHeader();
			chAddressFamily = new ColumnHeader();
			chPrefixOrigin = new ColumnHeader();
			chSuffixOrigin = new ColumnHeader();
			notifyIcon1 = new NotifyIcon(components);
			cmsNotifyIconMenu = new ContextMenuStrip(components);
			tsmiShow = new ToolStripMenuItem();
			tsmiHide = new ToolStripMenuItem();
			toolStripSeparator1 = new ToolStripSeparator();
			tsmiExit = new ToolStripMenuItem();
			toolStripSeparator2 = new ToolStripSeparator();
			tsmiShortcuts = new ToolStripMenuItem();
			ssStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
			splitContainer3.Panel1.SuspendLayout();
			splitContainer3.Panel2.SuspendLayout();
			splitContainer3.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			tsAdapters.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
			splitContainer2.Panel1.SuspendLayout();
			splitContainer2.Panel2.SuspendLayout();
			splitContainer2.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			cmsNotifyIconMenu.SuspendLayout();
			SuspendLayout();
			// 
			// lsbAdapters
			// 
			lsbAdapters.Dock = DockStyle.Fill;
			lsbAdapters.FormattingEnabled = true;
			lsbAdapters.IntegralHeight = false;
			lsbAdapters.ItemHeight = 25;
			lsbAdapters.Location = new Point(4, 45);
			lsbAdapters.Margin = new Padding(4, 5, 4, 5);
			lsbAdapters.Name = "lsbAdapters";
			lsbAdapters.Size = new Size(439, 292);
			lsbAdapters.TabIndex = 2;
			lsbAdapters.SelectedIndexChanged += lsbAdapters_SelectedIndexChanged;
			lsbAdapters.DoubleClick += lsbAdapters_DoubleClick;
			// 
			// ssStatus
			// 
			ssStatus.ImageScalingSize = new Size(24, 24);
			ssStatus.Items.AddRange(new ToolStripItem[] { tsslStatus });
			ssStatus.Location = new Point(0, 718);
			ssStatus.Name = "ssStatus";
			ssStatus.Padding = new Padding(1, 0, 20, 0);
			ssStatus.Size = new Size(1143, 32);
			ssStatus.TabIndex = 3;
			ssStatus.Text = "statusStrip1";
			// 
			// tsslStatus
			// 
			tsslStatus.Name = "tsslStatus";
			tsslStatus.Size = new Size(179, 25);
			tsslStatus.Text = "toolStripStatusLabel1";
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = DockStyle.Fill;
			splitContainer1.Location = new Point(0, 0);
			splitContainer1.Margin = new Padding(4, 5, 4, 5);
			splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(splitContainer3);
			splitContainer1.Panel1.Controls.Add(tsAdapters);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(splitContainer2);
			splitContainer1.Size = new Size(1143, 718);
			splitContainer1.SplitterDistance = 447;
			splitContainer1.SplitterWidth = 6;
			splitContainer1.TabIndex = 4;
			// 
			// splitContainer3
			// 
			splitContainer3.Dock = DockStyle.Fill;
			splitContainer3.Location = new Point(0, 33);
			splitContainer3.Name = "splitContainer3";
			splitContainer3.Orientation = Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			splitContainer3.Panel1.Controls.Add(tableLayoutPanel3);
			// 
			// splitContainer3.Panel2
			// 
			splitContainer3.Panel2.Controls.Add(tableLayoutPanel2);
			splitContainer3.Size = new Size(447, 685);
			splitContainer3.SplitterDistance = 342;
			splitContainer3.TabIndex = 3;
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.ColumnCount = 1;
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel3.Controls.Add(label6, 0, 0);
			tableLayoutPanel3.Controls.Add(lsbAdapters, 0, 1);
			tableLayoutPanel3.Dock = DockStyle.Fill;
			tableLayoutPanel3.Location = new Point(0, 0);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 2;
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel3.Size = new Size(447, 342);
			tableLayoutPanel3.TabIndex = 3;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Dock = DockStyle.Bottom;
			label6.Location = new Point(3, 15);
			label6.Name = "label6";
			label6.Size = new Size(441, 25);
			label6.TabIndex = 3;
			label6.Text = "Adapters";
			label6.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 1;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.Controls.Add(lsbShortcuts, 0, 1);
			tableLayoutPanel2.Controls.Add(label5, 0, 0);
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new Point(0, 0);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 2;
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.Size = new Size(447, 339);
			tableLayoutPanel2.TabIndex = 1;
			// 
			// lsbShortcuts
			// 
			lsbShortcuts.Dock = DockStyle.Fill;
			lsbShortcuts.FormattingEnabled = true;
			lsbShortcuts.IntegralHeight = false;
			lsbShortcuts.ItemHeight = 25;
			lsbShortcuts.Location = new Point(3, 43);
			lsbShortcuts.Name = "lsbShortcuts";
			lsbShortcuts.Size = new Size(441, 293);
			lsbShortcuts.TabIndex = 0;
			lsbShortcuts.SelectedIndexChanged += lsbShortcuts_SelectedIndexChanged;
			lsbShortcuts.DoubleClick += lsbShortcuts_DoubleClick;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Dock = DockStyle.Bottom;
			label5.Location = new Point(3, 15);
			label5.Name = "label5";
			label5.Size = new Size(441, 25);
			label5.TabIndex = 1;
			label5.Text = "Shortcuts";
			label5.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// tsAdapters
			// 
			tsAdapters.ImageScalingSize = new Size(24, 24);
			tsAdapters.Items.AddRange(new ToolStripItem[] { tsbRefresh, tsbOnlineOnly, toolStripSeparator3, tsbNewShortcut, tsbDeleteShortcut, tsbEditShortcut, tsbRecallShortcut, toolStripSeparator4, tsbSettings, tsbDebug });
			tsAdapters.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			tsAdapters.Location = new Point(0, 0);
			tsAdapters.Name = "tsAdapters";
			tsAdapters.Padding = new Padding(0, 0, 3, 0);
			tsAdapters.Size = new Size(447, 33);
			tsAdapters.TabIndex = 0;
			tsAdapters.Text = "toolStrip1";
			// 
			// tsbRefresh
			// 
			tsbRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
			tsbRefresh.Image = (Image)resources.GetObject("tsbRefresh.Image");
			tsbRefresh.ImageTransparentColor = Color.Magenta;
			tsbRefresh.Name = "tsbRefresh";
			tsbRefresh.Size = new Size(34, 28);
			tsbRefresh.Text = "Refresh";
			tsbRefresh.ToolTipText = "Refresh the list of adapters";
			tsbRefresh.Click += tsbRefresh_Click;
			// 
			// tsbOnlineOnly
			// 
			tsbOnlineOnly.Checked = true;
			tsbOnlineOnly.CheckOnClick = true;
			tsbOnlineOnly.CheckState = CheckState.Checked;
			tsbOnlineOnly.DisplayStyle = ToolStripItemDisplayStyle.Image;
			tsbOnlineOnly.Image = (Image)resources.GetObject("tsbOnlineOnly.Image");
			tsbOnlineOnly.ImageTransparentColor = Color.Magenta;
			tsbOnlineOnly.Name = "tsbOnlineOnly";
			tsbOnlineOnly.Size = new Size(34, 28);
			tsbOnlineOnly.Text = "Hide Offline";
			tsbOnlineOnly.ToolTipText = "Hide offline adapters";
			tsbOnlineOnly.Click += tsbOnlineOnly_Click;
			// 
			// toolStripSeparator3
			// 
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new Size(6, 33);
			// 
			// tsbNewShortcut
			// 
			tsbNewShortcut.DisplayStyle = ToolStripItemDisplayStyle.Image;
			tsbNewShortcut.Enabled = false;
			tsbNewShortcut.Image = (Image)resources.GetObject("tsbNewShortcut.Image");
			tsbNewShortcut.ImageTransparentColor = Color.Magenta;
			tsbNewShortcut.Name = "tsbNewShortcut";
			tsbNewShortcut.Size = new Size(34, 28);
			tsbNewShortcut.Text = "New Shortcut";
			tsbNewShortcut.ToolTipText = "Create a new configuration shortcut for this adapter";
			tsbNewShortcut.Click += tsbNewShortcut_Click;
			// 
			// tsbDeleteShortcut
			// 
			tsbDeleteShortcut.DisplayStyle = ToolStripItemDisplayStyle.Image;
			tsbDeleteShortcut.Enabled = false;
			tsbDeleteShortcut.Image = (Image)resources.GetObject("tsbDeleteShortcut.Image");
			tsbDeleteShortcut.ImageTransparentColor = Color.Magenta;
			tsbDeleteShortcut.Name = "tsbDeleteShortcut";
			tsbDeleteShortcut.Size = new Size(34, 28);
			tsbDeleteShortcut.Text = "Delete Shortcut";
			tsbDeleteShortcut.ToolTipText = "Delete the selected shortcut";
			tsbDeleteShortcut.Click += tsbDeleteShortcut_Click;
			// 
			// tsbEditShortcut
			// 
			tsbEditShortcut.DisplayStyle = ToolStripItemDisplayStyle.Image;
			tsbEditShortcut.Enabled = false;
			tsbEditShortcut.Image = (Image)resources.GetObject("tsbEditShortcut.Image");
			tsbEditShortcut.ImageTransparentColor = Color.Magenta;
			tsbEditShortcut.Name = "tsbEditShortcut";
			tsbEditShortcut.Size = new Size(34, 28);
			tsbEditShortcut.Text = "Edit Shortcut";
			tsbEditShortcut.ToolTipText = "Edit selected shortcut";
			tsbEditShortcut.Click += tsbEditShortcut_Click;
			// 
			// tsbRecallShortcut
			// 
			tsbRecallShortcut.DisplayStyle = ToolStripItemDisplayStyle.Image;
			tsbRecallShortcut.Enabled = false;
			tsbRecallShortcut.Image = (Image)resources.GetObject("tsbRecallShortcut.Image");
			tsbRecallShortcut.ImageTransparentColor = Color.Magenta;
			tsbRecallShortcut.Name = "tsbRecallShortcut";
			tsbRecallShortcut.Size = new Size(34, 28);
			tsbRecallShortcut.Text = "Recall Shortcut";
			tsbRecallShortcut.ToolTipText = "Recalls the selected shortcut";
			tsbRecallShortcut.Click += tsbRecallShortcut_Click;
			// 
			// toolStripSeparator4
			// 
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new Size(6, 33);
			// 
			// tsbSettings
			// 
			tsbSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
			tsbSettings.Image = (Image)resources.GetObject("tsbSettings.Image");
			tsbSettings.ImageTransparentColor = Color.Magenta;
			tsbSettings.Name = "tsbSettings";
			tsbSettings.Size = new Size(34, 28);
			tsbSettings.Text = "Settings";
			tsbSettings.ToolTipText = "Open the application settings dialog";
			tsbSettings.Click += tsbSettings_Click;
			// 
			// tsbDebug
			// 
			tsbDebug.DisplayStyle = ToolStripItemDisplayStyle.Image;
			tsbDebug.Image = (Image)resources.GetObject("tsbDebug.Image");
			tsbDebug.ImageTransparentColor = Color.Magenta;
			tsbDebug.Name = "tsbDebug";
			tsbDebug.Size = new Size(34, 28);
			tsbDebug.Text = "Debug";
			tsbDebug.ToolTipText = "Show the debug window";
			tsbDebug.Click += tsbDebug_Click;
			// 
			// splitContainer2
			// 
			splitContainer2.Dock = DockStyle.Fill;
			splitContainer2.Location = new Point(0, 0);
			splitContainer2.Margin = new Padding(4, 5, 4, 5);
			splitContainer2.Name = "splitContainer2";
			splitContainer2.Orientation = Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			splitContainer2.Panel1.Controls.Add(tableLayoutPanel1);
			// 
			// splitContainer2.Panel2
			// 
			splitContainer2.Panel2.Controls.Add(lsvAddresses);
			splitContainer2.Size = new Size(690, 718);
			splitContainer2.SplitterDistance = 248;
			splitContainer2.SplitterWidth = 7;
			splitContainer2.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.05176F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 74.94824F));
			tableLayoutPanel1.Controls.Add(label4, 0, 3);
			tableLayoutPanel1.Controls.Add(txtDeviceID, 1, 3);
			tableLayoutPanel1.Controls.Add(txtDriver, 1, 2);
			tableLayoutPanel1.Controls.Add(label1, 0, 0);
			tableLayoutPanel1.Controls.Add(label2, 0, 1);
			tableLayoutPanel1.Controls.Add(txtHardwareAddress, 1, 0);
			tableLayoutPanel1.Controls.Add(txtSpeed, 1, 1);
			tableLayoutPanel1.Controls.Add(label3, 0, 2);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Margin = new Padding(4, 5, 4, 5);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 4;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
			tableLayoutPanel1.Size = new Size(690, 248);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Left;
			label4.AutoSize = true;
			label4.Location = new Point(4, 204);
			label4.Margin = new Padding(4, 0, 4, 0);
			label4.Name = "label4";
			label4.Size = new Size(87, 25);
			label4.TabIndex = 8;
			label4.Text = "Device ID";
			// 
			// txtDeviceID
			// 
			txtDeviceID.Anchor = AnchorStyles.Left;
			txtDeviceID.Font = new Font("Consolas", 9F);
			txtDeviceID.Location = new Point(176, 202);
			txtDeviceID.Margin = new Padding(4, 5, 4, 5);
			txtDeviceID.Name = "txtDeviceID";
			txtDeviceID.PlaceholderText = "Select an Adapter";
			txtDeviceID.ReadOnly = true;
			txtDeviceID.Size = new Size(507, 29);
			txtDeviceID.TabIndex = 7;
			// 
			// txtDriver
			// 
			txtDriver.Anchor = AnchorStyles.Left;
			txtDriver.Font = new Font("Consolas", 9F);
			txtDriver.Location = new Point(176, 140);
			txtDriver.Margin = new Padding(4, 5, 4, 5);
			txtDriver.Name = "txtDriver";
			txtDriver.PlaceholderText = "Select an Adapter";
			txtDriver.ReadOnly = true;
			txtDriver.Size = new Size(507, 29);
			txtDriver.TabIndex = 5;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Left;
			label1.AutoSize = true;
			label1.Location = new Point(4, 18);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(158, 25);
			label1.TabIndex = 0;
			label1.Text = "Hardware Address";
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Left;
			label2.AutoSize = true;
			label2.Location = new Point(4, 80);
			label2.Margin = new Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new Size(62, 25);
			label2.TabIndex = 1;
			label2.Text = "Speed";
			// 
			// txtHardwareAddress
			// 
			txtHardwareAddress.Anchor = AnchorStyles.Left;
			txtHardwareAddress.Font = new Font("Consolas", 9F);
			txtHardwareAddress.Location = new Point(176, 16);
			txtHardwareAddress.Margin = new Padding(4, 5, 4, 5);
			txtHardwareAddress.Name = "txtHardwareAddress";
			txtHardwareAddress.PlaceholderText = "Select an Adapter";
			txtHardwareAddress.ReadOnly = true;
			txtHardwareAddress.Size = new Size(507, 29);
			txtHardwareAddress.TabIndex = 2;
			// 
			// txtSpeed
			// 
			txtSpeed.Anchor = AnchorStyles.Left;
			txtSpeed.Font = new Font("Consolas", 9F);
			txtSpeed.Location = new Point(176, 78);
			txtSpeed.Margin = new Padding(4, 5, 4, 5);
			txtSpeed.Name = "txtSpeed";
			txtSpeed.PlaceholderText = "Select an Adapter";
			txtSpeed.ReadOnly = true;
			txtSpeed.Size = new Size(507, 29);
			txtSpeed.TabIndex = 3;
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Left;
			label3.AutoSize = true;
			label3.Location = new Point(4, 142);
			label3.Margin = new Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new Size(154, 25);
			label3.TabIndex = 4;
			label3.Text = "Driver Description";
			// 
			// lsvAddresses
			// 
			lsvAddresses.AllowColumnReorder = true;
			lsvAddresses.Columns.AddRange(new ColumnHeader[] { chAddress, chPrefixLength, chAddressFamily, chPrefixOrigin, chSuffixOrigin });
			lsvAddresses.Dock = DockStyle.Fill;
			lsvAddresses.Location = new Point(0, 0);
			lsvAddresses.Margin = new Padding(4, 5, 4, 5);
			lsvAddresses.Name = "lsvAddresses";
			lsvAddresses.Size = new Size(690, 463);
			lsvAddresses.TabIndex = 0;
			lsvAddresses.UseCompatibleStateImageBehavior = false;
			lsvAddresses.View = View.Details;
			// 
			// chAddress
			// 
			chAddress.Text = "Address";
			chAddress.Width = 120;
			// 
			// chPrefixLength
			// 
			chPrefixLength.Text = "Prefix Length";
			// 
			// chAddressFamily
			// 
			chAddressFamily.Text = "Family";
			// 
			// chPrefixOrigin
			// 
			chPrefixOrigin.Text = "Prefix Origin";
			chPrefixOrigin.Width = 100;
			// 
			// chSuffixOrigin
			// 
			chSuffixOrigin.Text = "Suffix Origin";
			chSuffixOrigin.Width = 100;
			// 
			// notifyIcon1
			// 
			notifyIcon1.ContextMenuStrip = cmsNotifyIconMenu;
			notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
			notifyIcon1.Text = "IP Address Changer";
			notifyIcon1.Visible = true;
			notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
			// 
			// cmsNotifyIconMenu
			// 
			cmsNotifyIconMenu.ImageScalingSize = new Size(24, 24);
			cmsNotifyIconMenu.Items.AddRange(new ToolStripItem[] { tsmiShow, tsmiHide, toolStripSeparator1, tsmiExit, toolStripSeparator2, tsmiShortcuts });
			cmsNotifyIconMenu.Name = "contextMenuStrip1";
			cmsNotifyIconMenu.Size = new Size(160, 144);
			// 
			// tsmiShow
			// 
			tsmiShow.Name = "tsmiShow";
			tsmiShow.Size = new Size(159, 32);
			tsmiShow.Text = "&Show";
			tsmiShow.ToolTipText = "Show the application";
			tsmiShow.Click += tsmiShow_Click;
			// 
			// tsmiHide
			// 
			tsmiHide.Name = "tsmiHide";
			tsmiHide.Size = new Size(159, 32);
			tsmiHide.Text = "&Hide";
			tsmiHide.ToolTipText = "Hide the application";
			tsmiHide.Click += tsmiHide_Click;
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new Size(156, 6);
			// 
			// tsmiExit
			// 
			tsmiExit.Name = "tsmiExit";
			tsmiExit.Size = new Size(159, 32);
			tsmiExit.Text = "E&xit";
			tsmiExit.ToolTipText = "Exit the application";
			tsmiExit.Click += tsmiExit_Click;
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new Size(156, 6);
			// 
			// tsmiShortcuts
			// 
			tsmiShortcuts.DoubleClickEnabled = true;
			tsmiShortcuts.Name = "tsmiShortcuts";
			tsmiShortcuts.Size = new Size(159, 32);
			tsmiShortcuts.Text = "Shortcuts";
			tsmiShortcuts.ToolTipText = "IP Address Shortcuts List";
			// 
			// frmMain
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1143, 750);
			Controls.Add(splitContainer1);
			Controls.Add(ssStatus);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(4, 5, 4, 5);
			Name = "frmMain";
			Text = "IP Address Changer";
			FormClosing += frmMain_FormClosing;
			Load += frmMain_Load;
			SizeChanged += frmMain_SizeChanged;
			ssStatus.ResumeLayout(false);
			ssStatus.PerformLayout();
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel1.PerformLayout();
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			splitContainer3.Panel1.ResumeLayout(false);
			splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
			splitContainer3.ResumeLayout(false);
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			tsAdapters.ResumeLayout(false);
			tsAdapters.PerformLayout();
			splitContainer2.Panel1.ResumeLayout(false);
			splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
			splitContainer2.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			cmsNotifyIconMenu.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private ListBox lsbAdapters;
		private StatusStrip ssStatus;
		private ToolStripStatusLabel tsslStatus;
		private SplitContainer splitContainer1;
		private ToolStrip tsAdapters;
		private ToolStripButton tsbRefresh;
		private ToolStripButton tsbOnlineOnly;
		private NotifyIcon notifyIcon1;
		private ContextMenuStrip cmsNotifyIconMenu;
		private ToolStripMenuItem tsmiShow;
		private ToolStripMenuItem tsmiExit;
		private ToolStripMenuItem tsmiHide;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem tsmiShortcuts;
		private ListView lsvAddresses;
		private ColumnHeader chAddress;
		private ColumnHeader chAddressFamily;
		private ColumnHeader chPrefixOrigin;
		private ColumnHeader chSuffixOrigin;
		private ColumnHeader chPrefixLength;
		private SplitContainer splitContainer2;
		private TableLayoutPanel tableLayoutPanel1;
		private Label label1;
		private Label label2;
		private TextBox txtHardwareAddress;
		private TextBox txtSpeed;
		private TextBox txtDriver;
		private Label label3;
		private Label label4;
		private TextBox txtDeviceID;
		private ToolStripButton tsbNewShortcut;
		private SplitContainer splitContainer3;
		private ListBox lsbShortcuts;
		private ToolStripButton tsbDeleteShortcut;
		private ToolStripButton tsbEditShortcut;
		private TableLayoutPanel tableLayoutPanel3;
		private Label label6;
		private TableLayoutPanel tableLayoutPanel2;
		private Label label5;
		private ToolStripButton tsbRecallShortcut;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripButton tsbSettings;
		private ToolStripButton tsbDebug;
	}
}
