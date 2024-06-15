namespace IPAddressChanger {
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
			this.lsbAdapters = new ListBox();
			this.ssStatus = new StatusStrip();
			this.tsslStatus = new ToolStripStatusLabel();
			this.tsslVersion = new ToolStripStatusLabel();
			this.splitContainer1 = new SplitContainer();
			this.splitContainer3 = new SplitContainer();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.label6 = new Label();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.lsbShortcuts = new ListBox();
			this.label5 = new Label();
			this.tsMain = new ToolStrip();
			this.tsbRefresh = new ToolStripButton();
			this.tsbOnlineOnly = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.tsbNewShortcut = new ToolStripButton();
			this.tsbDeleteShortcut = new ToolStripButton();
			this.tsbEditShortcut = new ToolStripButton();
			this.tsbRecallShortcut = new ToolStripButton();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.tsbSettings = new ToolStripButton();
			this.tsbDebug = new ToolStripButton();
			this.tsbControlPanel = new ToolStripButton();
			this.tsbHelp = new ToolStripButton();
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
			this.tableLayoutPanel2.SuspendLayout();
			this.tsMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.cmsNotifyIconMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// lsbAdapters
			// 
			this.lsbAdapters.Dock = DockStyle.Fill;
			this.lsbAdapters.FormattingEnabled = true;
			this.helpProvider1.SetHelpKeyword(this.lsbAdapters, "adapters-list");
			this.helpProvider1.SetHelpNavigator(this.lsbAdapters, HelpNavigator.Topic);
			this.helpProvider1.SetHelpString(this.lsbAdapters, "");
			this.lsbAdapters.IntegralHeight = false;
			this.lsbAdapters.ItemHeight = 15;
			this.lsbAdapters.Location = new Point(3, 27);
			this.lsbAdapters.Name = "lsbAdapters";
			this.helpProvider1.SetShowHelp(this.lsbAdapters, true);
			this.lsbAdapters.Size = new Size(315, 168);
			this.lsbAdapters.TabIndex = 2;
			this.lsbAdapters.SelectedIndexChanged += this.lsbAdapters_SelectedIndexChanged;
			this.lsbAdapters.DoubleClick += this.lsbAdapters_DoubleClick;
			// 
			// ssStatus
			// 
			this.ssStatus.ImageScalingSize = new Size(24, 24);
			this.ssStatus.Items.AddRange(new ToolStripItem[] { this.tsslStatus, this.tsslVersion });
			this.ssStatus.Location = new Point(0, 428);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new Size(800, 22);
			this.ssStatus.TabIndex = 3;
			this.ssStatus.Text = "statusStrip1";
			// 
			// tsslStatus
			// 
			this.tsslStatus.Name = "tsslStatus";
			this.tsslStatus.Size = new Size(740, 17);
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
			this.splitContainer1.Location = new Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
			this.splitContainer1.Panel1.Controls.Add(this.tsMain);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new Size(800, 428);
			this.splitContainer1.SplitterDistance = 321;
			this.splitContainer1.TabIndex = 4;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = DockStyle.Fill;
			this.splitContainer3.Location = new Point(0, 31);
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
			this.splitContainer3.Size = new Size(321, 397);
			this.splitContainer3.SplitterDistance = 198;
			this.splitContainer3.SplitterWidth = 2;
			this.splitContainer3.TabIndex = 3;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.label6, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.lsbAdapters, 0, 1);
			this.tableLayoutPanel3.Dock = DockStyle.Fill;
			this.tableLayoutPanel3.Location = new Point(0, 0);
			this.tableLayoutPanel3.Margin = new Padding(2);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new Size(321, 198);
			this.tableLayoutPanel3.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = DockStyle.Bottom;
			this.label6.Location = new Point(2, 9);
			this.label6.Margin = new Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new Size(317, 15);
			this.label6.TabIndex = 3;
			this.label6.Text = "Adapters";
			this.label6.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.lsbShortcuts, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
			this.tableLayoutPanel2.Dock = DockStyle.Fill;
			this.tableLayoutPanel2.Location = new Point(0, 0);
			this.tableLayoutPanel2.Margin = new Padding(2);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new Size(321, 197);
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
			this.lsbShortcuts.Size = new Size(317, 169);
			this.lsbShortcuts.TabIndex = 0;
			this.lsbShortcuts.SelectedIndexChanged += this.lsbShortcuts_SelectedIndexChanged;
			this.lsbShortcuts.DoubleClick += this.lsbShortcuts_DoubleClick;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = DockStyle.Bottom;
			this.label5.Location = new Point(2, 9);
			this.label5.Margin = new Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new Size(317, 15);
			this.label5.TabIndex = 1;
			this.label5.Text = "Shortcuts";
			this.label5.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// tsMain
			// 
			this.helpProvider1.SetHelpKeyword(this.tsMain, "tool-bar");
			this.helpProvider1.SetHelpNavigator(this.tsMain, HelpNavigator.Topic);
			this.tsMain.ImageScalingSize = new Size(24, 24);
			this.tsMain.Items.AddRange(new ToolStripItem[] { this.tsbRefresh, this.tsbOnlineOnly, this.toolStripSeparator3, this.tsbNewShortcut, this.tsbDeleteShortcut, this.tsbEditShortcut, this.tsbRecallShortcut, this.toolStripSeparator4, this.tsbSettings, this.tsbDebug, this.tsbControlPanel, this.tsbHelp });
			this.tsMain.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.tsMain.Location = new Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Padding = new Padding(0, 0, 2, 0);
			this.helpProvider1.SetShowHelp(this.tsMain, true);
			this.tsMain.Size = new Size(321, 31);
			this.tsMain.TabIndex = 0;
			this.tsMain.Text = "toolStrip1";
			// 
			// tsbRefresh
			// 
			this.tsbRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbRefresh.Image = (Image)resources.GetObject("tsbRefresh.Image");
			this.tsbRefresh.ImageTransparentColor = Color.Magenta;
			this.tsbRefresh.Name = "tsbRefresh";
			this.tsbRefresh.Size = new Size(28, 28);
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
			this.tsbOnlineOnly.Size = new Size(28, 28);
			this.tsbOnlineOnly.Text = "Hide Offline";
			this.tsbOnlineOnly.ToolTipText = "Hide offline adapters";
			this.tsbOnlineOnly.Click += this.tsbOnlineOnly_Click;
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 31);
			// 
			// tsbNewShortcut
			// 
			this.tsbNewShortcut.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbNewShortcut.Enabled = false;
			this.tsbNewShortcut.Image = (Image)resources.GetObject("tsbNewShortcut.Image");
			this.tsbNewShortcut.ImageTransparentColor = Color.Magenta;
			this.tsbNewShortcut.Name = "tsbNewShortcut";
			this.tsbNewShortcut.Size = new Size(28, 28);
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
			this.tsbDeleteShortcut.Size = new Size(28, 28);
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
			this.tsbEditShortcut.Size = new Size(28, 28);
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
			this.tsbRecallShortcut.Size = new Size(28, 28);
			this.tsbRecallShortcut.Text = "Recall Shortcut";
			this.tsbRecallShortcut.ToolTipText = "Recalls the selected shortcut";
			this.tsbRecallShortcut.Click += this.tsbRecallShortcut_Click;
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new Size(6, 31);
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
			this.splitContainer2.Size = new Size(475, 428);
			this.splitContainer2.SplitterDistance = 147;
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
			this.tableLayoutPanel1.Size = new Size(475, 147);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.Anchor = AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(3, 120);
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
			this.txtDeviceID.Location = new Point(121, 116);
			this.txtDeviceID.Name = "txtDeviceID";
			this.txtDeviceID.PlaceholderText = "Select an Adapter";
			this.txtDeviceID.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.txtDeviceID, true);
			this.txtDeviceID.Size = new Size(351, 22);
			this.txtDeviceID.TabIndex = 7;
			// 
			// txtDriver
			// 
			this.txtDriver.Anchor = AnchorStyles.Left;
			this.txtDriver.Font = new Font("Consolas", 9F);
			this.helpProvider1.SetHelpKeyword(this.txtDriver, "adapter-details");
			this.helpProvider1.SetHelpNavigator(this.txtDriver, HelpNavigator.Topic);
			this.txtDriver.Location = new Point(121, 79);
			this.txtDriver.Name = "txtDriver";
			this.txtDriver.PlaceholderText = "Select an Adapter";
			this.txtDriver.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.txtDriver, true);
			this.txtDriver.Size = new Size(351, 22);
			this.txtDriver.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Anchor = AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 10);
			this.label1.Name = "label1";
			this.label1.Size = new Size(103, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Hardware Address";
			// 
			// label2
			// 
			this.label2.Anchor = AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(3, 46);
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
			this.txtHardwareAddress.Location = new Point(121, 7);
			this.txtHardwareAddress.Name = "txtHardwareAddress";
			this.txtHardwareAddress.PlaceholderText = "Select an Adapter";
			this.txtHardwareAddress.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.txtHardwareAddress, true);
			this.txtHardwareAddress.Size = new Size(351, 22);
			this.txtHardwareAddress.TabIndex = 2;
			// 
			// txtSpeed
			// 
			this.txtSpeed.Anchor = AnchorStyles.Left;
			this.txtSpeed.Font = new Font("Consolas", 9F);
			this.helpProvider1.SetHelpKeyword(this.txtSpeed, "adapter-details");
			this.helpProvider1.SetHelpNavigator(this.txtSpeed, HelpNavigator.Topic);
			this.txtSpeed.Location = new Point(121, 43);
			this.txtSpeed.Name = "txtSpeed";
			this.txtSpeed.PlaceholderText = "Select an Adapter";
			this.txtSpeed.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.txtSpeed, true);
			this.txtSpeed.Size = new Size(351, 22);
			this.txtSpeed.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Anchor = AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(3, 82);
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
			this.lsvAddresses.Size = new Size(475, 277);
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
			// notifyIcon1
			// 
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
			this.ClientSize = new Size(800, 450);
			this.Controls.Add(this.splitContainer1);
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
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.splitContainer3).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.cmsNotifyIconMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion
		private ListBox lsbAdapters;
		private StatusStrip ssStatus;
		private ToolStripStatusLabel tsslStatus;
		private SplitContainer splitContainer1;
		private ToolStrip tsMain;
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
		private ToolStripButton tsbControlPanel;
		private ToolStripMenuItem tsmiControlPanel;
		private ToolStripStatusLabel tsslVersion;
		private HelpProvider helpProvider1;
		private ToolStripButton tsbHelp;
	}
}
