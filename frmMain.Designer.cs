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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.ssStatus = new StatusStrip();
			this.tsslStatus = new ToolStripStatusLabel();
			this.tsslVersion = new ToolStripStatusLabel();
			this.splitContainer1 = new SplitContainer();
			this.splitContainer3 = new SplitContainer();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.lsvAdapters = new ListView();
			this.chAdapterStatus = new ColumnHeader();
			this.chAdapterName = new ColumnHeader();
			this.netAdapterIcons = new ImageList(this.components);
			this.tsAdapters = new ToolStrip();
			this.tslAdapters = new ToolStripSpringLabel();
			this.tsbRefresh = new ToolStripButton();
			this.tsbOnlineOnly = new ToolStripButton();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.lsbShortcuts = new ListBox();
			this.tsShortcuts = new ToolStrip();
			this.tslShortcuts = new ToolStripSpringLabel();
			this.tsbNewShortcut = new ToolStripButton();
			this.tsbDeleteShortcut = new ToolStripButton();
			this.tsbEditShortcut = new ToolStripButton();
			this.tsbRecallShortcut = new ToolStripButton();
			this.tsbMoveShortcutUp = new ToolStripButton();
			this.tsbMoveShortcutDown = new ToolStripButton();
			this.splitContainer2 = new SplitContainer();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.label4 = new Label();
			this.txtDeviceID = new TextBox();
			this.txtDriver = new TextBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.txtHardwareAddress = new TextBox();
			this.txtSpeed = new TextBox();
			this.label3 = new Label();
			this.lsvAddresses = new ListView();
			this.chAddress = new ColumnHeader();
			this.chPrefixLength = new ColumnHeader();
			this.chAddressFamily = new ColumnHeader();
			this.chPrefixOrigin = new ColumnHeader();
			this.chSuffixOrigin = new ColumnHeader();
			this.tsMain = new ToolStrip();
			this.tsbSettings = new ToolStripButton();
			this.tsbDebug = new ToolStripButton();
			this.tsbControlPanel = new ToolStripButton();
			this.tsbHelp = new ToolStripButton();
			this.tsbBugReport = new ToolStripButton();
			this.notifyIcon1 = new NotifyIcon(this.components);
			this.cmsNotifyIconMenu = new ContextMenuStrip(this.components);
			this.tsmiShow = new ToolStripMenuItem();
			this.tsmiHide = new ToolStripMenuItem();
			this.tsmiExit = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tsmiControlPanel = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.tsmiShortcuts = new ToolStripMenuItem();
			this.helpProvider1 = new HelpProvider();
			this.ssStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.splitContainer3).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tsAdapters.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tsShortcuts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tsMain.SuspendLayout();
			this.cmsNotifyIconMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// ssStatus
			// 
			this.ssStatus.ImageScalingSize = new Size(24, 24);
			this.ssStatus.Items.AddRange(new ToolStripItem[] { this.tsslStatus, this.tsslVersion });
			this.ssStatus.Location = new Point(0, 430);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new Size(1067, 22);
			this.ssStatus.TabIndex = 3;
			this.ssStatus.Text = "statusStrip1";
			// 
			// tsslStatus
			// 
			this.tsslStatus.Name = "tsslStatus";
			this.tsslStatus.Size = new Size(1007, 17);
			this.tsslStatus.Spring = true;
			this.tsslStatus.Text = "Status";
			this.tsslStatus.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// tsslVersion
			// 
			this.tsslVersion.Name = "tsslVersion";
			this.tsslVersion.Size = new Size(45, 17);
			this.tsslVersion.Text = "Version";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(0, 31);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new Size(1067, 399);
			this.splitContainer1.SplitterDistance = 467;
			this.splitContainer1.TabIndex = 4;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = DockStyle.Fill;
			this.splitContainer3.Location = new Point(0, 0);
			this.splitContainer3.Margin = new Padding(2);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.tableLayoutPanel3);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.tableLayoutPanel2);
			this.splitContainer3.Size = new Size(467, 399);
			this.splitContainer3.SplitterDistance = 198;
			this.splitContainer3.SplitterWidth = 2;
			this.splitContainer3.TabIndex = 3;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.lsvAdapters, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.tsAdapters, 0, 0);
			this.tableLayoutPanel3.Dock = DockStyle.Fill;
			this.tableLayoutPanel3.Location = new Point(0, 0);
			this.tableLayoutPanel3.Margin = new Padding(2);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new Size(467, 198);
			this.tableLayoutPanel3.TabIndex = 3;
			// 
			// lsvAdapters
			// 
			this.lsvAdapters.Columns.AddRange(new ColumnHeader[] { this.chAdapterStatus, this.chAdapterName });
			this.lsvAdapters.Dock = DockStyle.Fill;
			this.lsvAdapters.FullRowSelect = true;
			this.lsvAdapters.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.helpProvider1.SetHelpKeyword(this.lsvAdapters, "adapters-list");
			this.helpProvider1.SetHelpNavigator(this.lsvAdapters, HelpNavigator.TopicId);
			this.lsvAdapters.Location = new Point(2, 26);
			this.lsvAdapters.Margin = new Padding(2);
			this.lsvAdapters.MultiSelect = false;
			this.lsvAdapters.Name = "lsvAdapters";
			this.helpProvider1.SetShowHelp(this.lsvAdapters, true);
			this.lsvAdapters.Size = new Size(463, 170);
			this.lsvAdapters.SmallImageList = this.netAdapterIcons;
			this.lsvAdapters.TabIndex = 4;
			this.lsvAdapters.UseCompatibleStateImageBehavior = false;
			this.lsvAdapters.View = View.Details;
			this.lsvAdapters.SelectedIndexChanged += this.lsvAdapters_SelectedIndexChanged;
			this.lsvAdapters.DoubleClick += this.lsvAdapters_DoubleClick;
			// 
			// chAdapterStatus
			// 
			this.chAdapterStatus.Text = "";
			// 
			// chAdapterName
			// 
			this.chAdapterName.Text = "";
			this.chAdapterName.Width = 400;
			// 
			// netAdapterIcons
			// 
			this.netAdapterIcons.ColorDepth = ColorDepth.Depth32Bit;
			this.netAdapterIcons.ImageStream = (ImageListStreamer)resources.GetObject("netAdapterIcons.ImageStream");
			this.netAdapterIcons.TransparentColor = Color.Transparent;
			this.netAdapterIcons.Images.SetKeyName(0, "disabled");
			this.netAdapterIcons.Images.SetKeyName(1, "up");
			this.netAdapterIcons.Images.SetKeyName(2, "down");
			// 
			// tsAdapters
			// 
			this.tsAdapters.GripStyle = ToolStripGripStyle.Hidden;
			this.tsAdapters.Items.AddRange(new ToolStripItem[] { this.tslAdapters, this.tsbRefresh, this.tsbOnlineOnly });
			this.tsAdapters.Location = new Point(0, 0);
			this.tsAdapters.Name = "tsAdapters";
			this.tsAdapters.RenderMode = ToolStripRenderMode.System;
			this.helpProvider1.SetShowHelp(this.tsAdapters, true);
			this.tsAdapters.Size = new Size(467, 24);
			this.tsAdapters.TabIndex = 5;
			this.tsAdapters.Text = "toolStrip2";
			// 
			// tslAdapters
			// 
			this.tslAdapters.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			this.tslAdapters.Name = "tslAdapters";
			this.tslAdapters.Size = new Size(389, 21);
			this.tslAdapters.Text = "Adapters";
			this.tslAdapters.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// tsbRefresh
			// 
			this.tsbRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbRefresh.Image = (Image)resources.GetObject("tsbRefresh.Image");
			this.tsbRefresh.ImageTransparentColor = Color.Magenta;
			this.tsbRefresh.Name = "tsbRefresh";
			this.tsbRefresh.Size = new Size(23, 21);
			this.tsbRefresh.Text = "Refresh";
			this.tsbRefresh.ToolTipText = "Refresh the list of adapters";
			this.tsbRefresh.Click += this.tsbRefresh_Click;
			// 
			// tsbOnlineOnly
			// 
			this.tsbOnlineOnly.Checked = true;
			this.tsbOnlineOnly.CheckOnClick = true;
			this.tsbOnlineOnly.CheckState = CheckState.Checked;
			this.tsbOnlineOnly.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbOnlineOnly.Image = (Image)resources.GetObject("tsbOnlineOnly.Image");
			this.tsbOnlineOnly.ImageTransparentColor = Color.Magenta;
			this.tsbOnlineOnly.Name = "tsbOnlineOnly";
			this.tsbOnlineOnly.Size = new Size(23, 21);
			this.tsbOnlineOnly.Text = "Hide Offline";
			this.tsbOnlineOnly.ToolTipText = "Hide offline adapters";
			this.tsbOnlineOnly.Click += this.tsbOnlineOnly_Click;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.lsbShortcuts, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.tsShortcuts, 0, 0);
			this.tableLayoutPanel2.Dock = DockStyle.Fill;
			this.tableLayoutPanel2.Location = new Point(0, 0);
			this.tableLayoutPanel2.Margin = new Padding(2);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new Size(467, 199);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// lsbShortcuts
			// 
			this.lsbShortcuts.Dock = DockStyle.Fill;
			this.lsbShortcuts.FormattingEnabled = true;
			this.helpProvider1.SetHelpKeyword(this.lsbShortcuts, "shortcuts-list");
			this.helpProvider1.SetHelpNavigator(this.lsbShortcuts, HelpNavigator.Topic);
			this.lsbShortcuts.IntegralHeight = false;
			this.lsbShortcuts.ItemHeight = 15;
			this.lsbShortcuts.Location = new Point(2, 26);
			this.lsbShortcuts.Margin = new Padding(2);
			this.lsbShortcuts.Name = "lsbShortcuts";
			this.helpProvider1.SetShowHelp(this.lsbShortcuts, true);
			this.lsbShortcuts.Size = new Size(463, 171);
			this.lsbShortcuts.TabIndex = 0;
			this.lsbShortcuts.SelectedIndexChanged += this.lsbShortcuts_SelectedIndexChanged;
			this.lsbShortcuts.DoubleClick += this.lsbShortcuts_DoubleClick;
			// 
			// tsShortcuts
			// 
			this.tsShortcuts.GripStyle = ToolStripGripStyle.Hidden;
			this.tsShortcuts.Items.AddRange(new ToolStripItem[] { this.tslShortcuts, this.tsbNewShortcut, this.tsbDeleteShortcut, this.tsbEditShortcut, this.tsbRecallShortcut, this.tsbMoveShortcutUp, this.tsbMoveShortcutDown });
			this.tsShortcuts.Location = new Point(0, 0);
			this.tsShortcuts.Name = "tsShortcuts";
			this.tsShortcuts.RenderMode = ToolStripRenderMode.System;
			this.helpProvider1.SetShowHelp(this.tsShortcuts, true);
			this.tsShortcuts.Size = new Size(467, 24);
			this.tsShortcuts.TabIndex = 1;
			this.tsShortcuts.Text = "toolStrip1";
			// 
			// tslShortcuts
			// 
			this.tslShortcuts.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			this.tslShortcuts.Name = "tslShortcuts";
			this.tslShortcuts.Size = new Size(297, 21);
			this.tslShortcuts.Text = "Shortcuts";
			this.tslShortcuts.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// tsbNewShortcut
			// 
			this.tsbNewShortcut.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbNewShortcut.Enabled = false;
			this.tsbNewShortcut.Image = (Image)resources.GetObject("tsbNewShortcut.Image");
			this.tsbNewShortcut.ImageTransparentColor = Color.Magenta;
			this.tsbNewShortcut.Name = "tsbNewShortcut";
			this.tsbNewShortcut.Size = new Size(23, 21);
			this.tsbNewShortcut.Text = "New Shortcut";
			this.tsbNewShortcut.ToolTipText = "Create a new configuration shortcut for this adapter";
			this.tsbNewShortcut.Click += this.tsbNewShortcut_Click;
			// 
			// tsbDeleteShortcut
			// 
			this.tsbDeleteShortcut.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbDeleteShortcut.Enabled = false;
			this.tsbDeleteShortcut.Image = (Image)resources.GetObject("tsbDeleteShortcut.Image");
			this.tsbDeleteShortcut.ImageTransparentColor = Color.Magenta;
			this.tsbDeleteShortcut.Name = "tsbDeleteShortcut";
			this.tsbDeleteShortcut.Size = new Size(23, 21);
			this.tsbDeleteShortcut.Text = "Delete Shortcut";
			this.tsbDeleteShortcut.ToolTipText = "Delete the selected shortcut";
			this.tsbDeleteShortcut.Click += this.tsbDeleteShortcut_Click;
			// 
			// tsbEditShortcut
			// 
			this.tsbEditShortcut.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbEditShortcut.Enabled = false;
			this.tsbEditShortcut.Image = (Image)resources.GetObject("tsbEditShortcut.Image");
			this.tsbEditShortcut.ImageTransparentColor = Color.Magenta;
			this.tsbEditShortcut.Name = "tsbEditShortcut";
			this.tsbEditShortcut.Size = new Size(23, 21);
			this.tsbEditShortcut.Text = "Edit Shortcut";
			this.tsbEditShortcut.ToolTipText = "Edit selected shortcut";
			this.tsbEditShortcut.Click += this.tsbEditShortcut_Click;
			// 
			// tsbRecallShortcut
			// 
			this.tsbRecallShortcut.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbRecallShortcut.Enabled = false;
			this.tsbRecallShortcut.Image = (Image)resources.GetObject("tsbRecallShortcut.Image");
			this.tsbRecallShortcut.ImageTransparentColor = Color.Magenta;
			this.tsbRecallShortcut.Name = "tsbRecallShortcut";
			this.tsbRecallShortcut.Size = new Size(23, 21);
			this.tsbRecallShortcut.Text = "Recall Shortcut";
			this.tsbRecallShortcut.ToolTipText = "Recalls the selected shortcut";
			this.tsbRecallShortcut.Click += this.tsbRecallShortcut_Click;
			// 
			// tsbMoveShortcutUp
			// 
			this.tsbMoveShortcutUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbMoveShortcutUp.Enabled = false;
			this.tsbMoveShortcutUp.Image = (Image)resources.GetObject("tsbMoveShortcutUp.Image");
			this.tsbMoveShortcutUp.ImageTransparentColor = Color.Magenta;
			this.tsbMoveShortcutUp.Name = "tsbMoveShortcutUp";
			this.tsbMoveShortcutUp.Size = new Size(23, 21);
			this.tsbMoveShortcutUp.Text = "toolStripButton1";
			this.tsbMoveShortcutUp.Click += this.tsbMoveShortcutUp_Click;
			// 
			// tsbMoveShortcutDown
			// 
			this.tsbMoveShortcutDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbMoveShortcutDown.Enabled = false;
			this.tsbMoveShortcutDown.Image = (Image)resources.GetObject("tsbMoveShortcutDown.Image");
			this.tsbMoveShortcutDown.ImageTransparentColor = Color.Magenta;
			this.tsbMoveShortcutDown.Name = "tsbMoveShortcutDown";
			this.tsbMoveShortcutDown.Size = new Size(23, 21);
			this.tsbMoveShortcutDown.Text = "toolStripButton2";
			this.tsbMoveShortcutDown.Click += this.tsbMoveShortcutDown_Click;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = DockStyle.Fill;
			this.splitContainer2.Location = new Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.lsvAddresses);
			this.splitContainer2.Size = new Size(596, 399);
			this.splitContainer2.SplitterDistance = 110;
			this.splitContainer2.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.05176F));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 74.94824F));
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.txtDeviceID, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.txtDriver, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtHardwareAddress, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtSpeed, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
			this.tableLayoutPanel1.Size = new Size(596, 110);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.Anchor = AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(3, 88);
			this.label4.Name = "label4";
			this.label4.Size = new Size(56, 15);
			this.label4.TabIndex = 8;
			this.label4.Text = "Device ID";
			// 
			// txtDeviceID
			// 
			this.txtDeviceID.Anchor = AnchorStyles.Left;
			this.txtDeviceID.Font = new Font("Consolas", 9F);
			this.helpProvider1.SetHelpKeyword(this.txtDeviceID, "adapter-details");
			this.helpProvider1.SetHelpNavigator(this.txtDeviceID, HelpNavigator.Topic);
			this.txtDeviceID.Location = new Point(152, 84);
			this.txtDeviceID.Name = "txtDeviceID";
			this.txtDeviceID.PlaceholderText = "Select an Adapter";
			this.txtDeviceID.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.txtDeviceID, true);
			this.txtDeviceID.Size = new Size(327, 22);
			this.txtDeviceID.TabIndex = 7;
			// 
			// txtDriver
			// 
			this.txtDriver.Anchor = AnchorStyles.Left;
			this.txtDriver.Font = new Font("Consolas", 9F);
			this.helpProvider1.SetHelpKeyword(this.txtDriver, "adapter-details");
			this.helpProvider1.SetHelpNavigator(this.txtDriver, HelpNavigator.Topic);
			this.txtDriver.Location = new Point(152, 57);
			this.txtDriver.Name = "txtDriver";
			this.txtDriver.PlaceholderText = "Select an Adapter";
			this.txtDriver.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.txtDriver, true);
			this.txtDriver.Size = new Size(327, 22);
			this.txtDriver.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Anchor = AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new Size(103, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Hardware Address";
			// 
			// label2
			// 
			this.label2.Anchor = AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(3, 33);
			this.label2.Name = "label2";
			this.label2.Size = new Size(39, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Speed";
			// 
			// txtHardwareAddress
			// 
			this.txtHardwareAddress.Anchor = AnchorStyles.Left;
			this.txtHardwareAddress.Font = new Font("Consolas", 9F);
			this.helpProvider1.SetHelpKeyword(this.txtHardwareAddress, "adapter-details");
			this.helpProvider1.SetHelpNavigator(this.txtHardwareAddress, HelpNavigator.Topic);
			this.txtHardwareAddress.Location = new Point(152, 3);
			this.txtHardwareAddress.Name = "txtHardwareAddress";
			this.txtHardwareAddress.PlaceholderText = "Select an Adapter";
			this.txtHardwareAddress.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.txtHardwareAddress, true);
			this.txtHardwareAddress.Size = new Size(327, 22);
			this.txtHardwareAddress.TabIndex = 2;
			// 
			// txtSpeed
			// 
			this.txtSpeed.Anchor = AnchorStyles.Left;
			this.txtSpeed.Font = new Font("Consolas", 9F);
			this.helpProvider1.SetHelpKeyword(this.txtSpeed, "adapter-details");
			this.helpProvider1.SetHelpNavigator(this.txtSpeed, HelpNavigator.Topic);
			this.txtSpeed.Location = new Point(152, 30);
			this.txtSpeed.Name = "txtSpeed";
			this.txtSpeed.PlaceholderText = "Select an Adapter";
			this.txtSpeed.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.txtSpeed, true);
			this.txtSpeed.Size = new Size(327, 22);
			this.txtSpeed.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Anchor = AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(3, 60);
			this.label3.Name = "label3";
			this.label3.Size = new Size(101, 15);
			this.label3.TabIndex = 4;
			this.label3.Text = "Driver Description";
			// 
			// lsvAddresses
			// 
			this.lsvAddresses.AllowColumnReorder = true;
			this.lsvAddresses.Columns.AddRange(new ColumnHeader[] { this.chAddress, this.chPrefixLength, this.chAddressFamily, this.chPrefixOrigin, this.chSuffixOrigin });
			this.lsvAddresses.Dock = DockStyle.Fill;
			this.lsvAddresses.FullRowSelect = true;
			this.lsvAddresses.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.helpProvider1.SetHelpKeyword(this.lsvAddresses, "adapter-addresses-list");
			this.helpProvider1.SetHelpNavigator(this.lsvAddresses, HelpNavigator.Topic);
			this.lsvAddresses.Location = new Point(0, 0);
			this.lsvAddresses.Name = "lsvAddresses";
			this.helpProvider1.SetShowHelp(this.lsvAddresses, true);
			this.lsvAddresses.Size = new Size(596, 285);
			this.lsvAddresses.TabIndex = 0;
			this.lsvAddresses.UseCompatibleStateImageBehavior = false;
			this.lsvAddresses.View = View.Details;
			this.lsvAddresses.DoubleClick += this.lsvAddresses_DoubleClick;
			// 
			// chAddress
			// 
			this.chAddress.Text = "Address";
			this.chAddress.Width = 120;
			// 
			// chPrefixLength
			// 
			this.chPrefixLength.Text = "Prefix Length";
			// 
			// chAddressFamily
			// 
			this.chAddressFamily.Text = "Family";
			// 
			// chPrefixOrigin
			// 
			this.chPrefixOrigin.Text = "Prefix Origin";
			this.chPrefixOrigin.Width = 100;
			// 
			// chSuffixOrigin
			// 
			this.chSuffixOrigin.Text = "Suffix Origin";
			this.chSuffixOrigin.Width = 100;
			// 
			// tsMain
			// 
			this.tsMain.GripStyle = ToolStripGripStyle.Hidden;
			this.helpProvider1.SetHelpKeyword(this.tsMain, "tool-bar");
			this.helpProvider1.SetHelpNavigator(this.tsMain, HelpNavigator.Topic);
			this.tsMain.ImageScalingSize = new Size(24, 24);
			this.tsMain.Items.AddRange(new ToolStripItem[] { this.tsbSettings, this.tsbDebug, this.tsbControlPanel, this.tsbHelp, this.tsbBugReport });
			this.tsMain.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.tsMain.Location = new Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Padding = new Padding(0, 0, 2, 0);
			this.tsMain.RenderMode = ToolStripRenderMode.System;
			this.helpProvider1.SetShowHelp(this.tsMain, true);
			this.tsMain.Size = new Size(1067, 31);
			this.tsMain.TabIndex = 0;
			this.tsMain.Text = "toolStrip1";
			// 
			// tsbSettings
			// 
			this.tsbSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSettings.Image = (Image)resources.GetObject("tsbSettings.Image");
			this.tsbSettings.ImageTransparentColor = Color.Magenta;
			this.tsbSettings.Name = "tsbSettings";
			this.tsbSettings.Size = new Size(28, 28);
			this.tsbSettings.Text = "Settings";
			this.tsbSettings.ToolTipText = "Open the application settings dialog";
			this.tsbSettings.Click += this.tsbSettings_Click;
			// 
			// tsbDebug
			// 
			this.tsbDebug.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbDebug.Image = (Image)resources.GetObject("tsbDebug.Image");
			this.tsbDebug.ImageTransparentColor = Color.Magenta;
			this.tsbDebug.Name = "tsbDebug";
			this.tsbDebug.Size = new Size(28, 28);
			this.tsbDebug.Text = "Debug";
			this.tsbDebug.ToolTipText = "Show the debug window";
			this.tsbDebug.Click += this.tsbDebug_Click;
			// 
			// tsbControlPanel
			// 
			this.tsbControlPanel.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbControlPanel.Image = (Image)resources.GetObject("tsbControlPanel.Image");
			this.tsbControlPanel.ImageTransparentColor = Color.Magenta;
			this.tsbControlPanel.Name = "tsbControlPanel";
			this.tsbControlPanel.Size = new Size(28, 28);
			this.tsbControlPanel.Text = "Control Panel";
			this.tsbControlPanel.ToolTipText = "Launch network adapters control panel";
			this.tsbControlPanel.Click += this.tsbControlPanel_Click;
			// 
			// tsbHelp
			// 
			this.tsbHelp.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbHelp.Image = (Image)resources.GetObject("tsbHelp.Image");
			this.tsbHelp.ImageTransparentColor = Color.Magenta;
			this.tsbHelp.Name = "tsbHelp";
			this.tsbHelp.Size = new Size(28, 28);
			this.tsbHelp.Text = "Help";
			this.tsbHelp.ToolTipText = "Show the help document";
			this.tsbHelp.Click += this.tsbHelp_Click;
			// 
			// tsbBugReport
			// 
			this.tsbBugReport.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbBugReport.Image = (Image)resources.GetObject("tsbBugReport.Image");
			this.tsbBugReport.ImageTransparentColor = Color.Magenta;
			this.tsbBugReport.Name = "tsbBugReport";
			this.tsbBugReport.Size = new Size(28, 28);
			this.tsbBugReport.Text = "Bug Report";
			this.tsbBugReport.ToolTipText = "Submit bug reports or other feedback";
			this.tsbBugReport.Click += this.tsbBugReport_Click;
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
			this.notifyIcon1.ContextMenuStrip = this.cmsNotifyIconMenu;
			this.notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
			this.notifyIcon1.Text = "IP Address Changer";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDoubleClick += this.notifyIcon1_MouseDoubleClick;
			// 
			// cmsNotifyIconMenu
			// 
			this.cmsNotifyIconMenu.ImageScalingSize = new Size(24, 24);
			this.cmsNotifyIconMenu.Items.AddRange(new ToolStripItem[] { this.tsmiShow, this.tsmiHide, this.tsmiExit, this.toolStripSeparator1, this.tsmiControlPanel, this.toolStripSeparator2, this.tsmiShortcuts });
			this.cmsNotifyIconMenu.Name = "contextMenuStrip1";
			this.cmsNotifyIconMenu.Size = new Size(147, 126);
			// 
			// tsmiShow
			// 
			this.tsmiShow.Name = "tsmiShow";
			this.tsmiShow.Size = new Size(146, 22);
			this.tsmiShow.Text = "&Show";
			this.tsmiShow.ToolTipText = "Show the application";
			this.tsmiShow.Click += this.tsmiShow_Click;
			// 
			// tsmiHide
			// 
			this.tsmiHide.Name = "tsmiHide";
			this.tsmiHide.Size = new Size(146, 22);
			this.tsmiHide.Text = "&Hide";
			this.tsmiHide.ToolTipText = "Hide the application";
			this.tsmiHide.Click += this.tsmiHide_Click;
			// 
			// tsmiExit
			// 
			this.tsmiExit.Name = "tsmiExit";
			this.tsmiExit.Size = new Size(146, 22);
			this.tsmiExit.Text = "E&xit";
			this.tsmiExit.ToolTipText = "Exit the application";
			this.tsmiExit.Click += this.tsmiExit_Click;
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(143, 6);
			// 
			// tsmiControlPanel
			// 
			this.tsmiControlPanel.Name = "tsmiControlPanel";
			this.tsmiControlPanel.Size = new Size(146, 22);
			this.tsmiControlPanel.Text = "&Control Panel";
			this.tsmiControlPanel.ToolTipText = "Launch the network adapters control panel";
			this.tsmiControlPanel.Click += this.tsmiControlPanel_Click;
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(143, 6);
			// 
			// tsmiShortcuts
			// 
			this.tsmiShortcuts.DoubleClickEnabled = true;
			this.tsmiShortcuts.Name = "tsmiShortcuts";
			this.tsmiShortcuts.Size = new Size(146, 22);
			this.tsmiShortcuts.Text = "Shortcuts";
			this.tsmiShortcuts.ToolTipText = "IP Address Shortcuts List";
			// 
			// helpProvider1
			// 
			this.helpProvider1.HelpNamespace = "https://spectrumintegrators.github.io/IPAddressChanger/";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(1067, 452);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.tsMain);
			this.Controls.Add(this.ssStatus);
			this.helpProvider1.SetHelpKeyword(this, "main-window");
			this.helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
			this.Icon = (Icon)resources.GetObject("$this.Icon");
			this.Name = "frmMain";
			this.helpProvider1.SetShowHelp(this, true);
			this.Text = "IP Address Changer";
			this.FormClosing += this.frmMain_FormClosing;
			this.Load += this.frmMain_Load;
			this.SizeChanged += this.frmMain_SizeChanged;
			this.ssStatus.ResumeLayout(false);
			this.ssStatus.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.splitContainer3).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tsAdapters.ResumeLayout(false);
			this.tsAdapters.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tsShortcuts.ResumeLayout(false);
			this.tsShortcuts.PerformLayout();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.cmsNotifyIconMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion
		private StatusStrip ssStatus;
		private ToolStripStatusLabel tsslStatus;
		private SplitContainer splitContainer1;
		private ToolStrip tsMain;
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
		private SplitContainer splitContainer3;
		private ListBox lsbShortcuts;
		private TableLayoutPanel tableLayoutPanel3;
		private TableLayoutPanel tableLayoutPanel2;
		private ToolStripButton tsbSettings;
		private ToolStripButton tsbDebug;
		private ToolStripButton tsbControlPanel;
		private ToolStripMenuItem tsmiControlPanel;
		private ToolStripStatusLabel tsslVersion;
		private HelpProvider helpProvider1;
		private ToolStripButton tsbHelp;
		private ToolStripButton tsbBugReport;
		private ListView lsvAdapters;
		private ColumnHeader chAdapterStatus;
		private ColumnHeader chAdapterName;
		private ImageList netAdapterIcons;
		private ToolStrip tsAdapters;
		private ToolStripButton tsbRefresh;
		private ToolStripButton tsbOnlineOnly;
		private ToolStrip tsShortcuts;
		private ToolStripButton tsbNewShortcut;
		private ToolStripButton tsbDeleteShortcut;
		private ToolStripButton tsbEditShortcut;
		private ToolStripButton tsbRecallShortcut;
		private ToolStripButton tsbMoveShortcutUp;
		private ToolStripButton tsbMoveShortcutDown;
		private ToolStripSpringLabel tslShortcuts;
		private ToolStripSpringLabel tslAdapters;
	}
}
