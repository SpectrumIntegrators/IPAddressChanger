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
			this.splitContainer1 = new SplitContainer();
			this.tsAdapters = new ToolStrip();
			this.tsbRefresh = new ToolStripButton();
			this.tsbOnlineOnly = new ToolStripButton();
			this.tsbRemoveAddress = new ToolStripButton();
			this.tsbSetAddress = new ToolStripButton();
			this.tsbEnableDHCP = new ToolStripButton();
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
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tsmiExit = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.tsmiNoShortcuts = new ToolStripMenuItem();
			this.ssStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tsAdapters.SuspendLayout();
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
			this.lsbAdapters.ItemHeight = 15;
			this.lsbAdapters.Location = new Point(0, 25);
			this.lsbAdapters.Name = "lsbAdapters";
			this.lsbAdapters.Size = new Size(313, 403);
			this.lsbAdapters.TabIndex = 2;
			this.lsbAdapters.SelectedIndexChanged += this.lsbAdapters_SelectedIndexChanged;
			// 
			// ssStatus
			// 
			this.ssStatus.Items.AddRange(new ToolStripItem[] { this.tsslStatus });
			this.ssStatus.Location = new Point(0, 428);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new Size(800, 22);
			this.ssStatus.TabIndex = 3;
			this.ssStatus.Text = "statusStrip1";
			// 
			// tsslStatus
			// 
			this.tsslStatus.Name = "tsslStatus";
			this.tsslStatus.Size = new Size(118, 17);
			this.tsslStatus.Text = "toolStripStatusLabel1";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lsbAdapters);
			this.splitContainer1.Panel1.Controls.Add(this.tsAdapters);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new Size(800, 428);
			this.splitContainer1.SplitterDistance = 313;
			this.splitContainer1.TabIndex = 4;
			// 
			// tsAdapters
			// 
			this.tsAdapters.Items.AddRange(new ToolStripItem[] { this.tsbRefresh, this.tsbOnlineOnly, this.tsbRemoveAddress, this.tsbSetAddress, this.tsbEnableDHCP });
			this.tsAdapters.Location = new Point(0, 0);
			this.tsAdapters.Name = "tsAdapters";
			this.tsAdapters.Size = new Size(313, 25);
			this.tsAdapters.TabIndex = 0;
			this.tsAdapters.Text = "toolStrip1";
			// 
			// tsbRefresh
			// 
			this.tsbRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbRefresh.Image = (Image)resources.GetObject("tsbRefresh.Image");
			this.tsbRefresh.ImageTransparentColor = Color.Magenta;
			this.tsbRefresh.Name = "tsbRefresh";
			this.tsbRefresh.Size = new Size(23, 22);
			this.tsbRefresh.Text = "Refresh adapters list";
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
			this.tsbOnlineOnly.Size = new Size(23, 22);
			this.tsbOnlineOnly.Text = "Hide offline adapters";
			this.tsbOnlineOnly.Click += this.tsbOnlineOnly_Click;
			// 
			// tsbRemoveAddress
			// 
			this.tsbRemoveAddress.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbRemoveAddress.Enabled = false;
			this.tsbRemoveAddress.Image = (Image)resources.GetObject("tsbRemoveAddress.Image");
			this.tsbRemoveAddress.ImageTransparentColor = Color.Magenta;
			this.tsbRemoveAddress.Name = "tsbRemoveAddress";
			this.tsbRemoveAddress.Size = new Size(23, 22);
			this.tsbRemoveAddress.ToolTipText = "Remove all addresses from the seleted adapter";
			this.tsbRemoveAddress.Click += this.tsbRemoveAddress_Click;
			// 
			// tsbSetAddress
			// 
			this.tsbSetAddress.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSetAddress.Enabled = false;
			this.tsbSetAddress.Image = (Image)resources.GetObject("tsbSetAddress.Image");
			this.tsbSetAddress.ImageTransparentColor = Color.Magenta;
			this.tsbSetAddress.Name = "tsbSetAddress";
			this.tsbSetAddress.Size = new Size(23, 22);
			this.tsbSetAddress.Text = "toolStripButton1";
			this.tsbSetAddress.ToolTipText = "Set the IP address for the selected adapter";
			this.tsbSetAddress.Click += this.tsbSetAddress_Click;
			// 
			// tsbEnableDHCP
			// 
			this.tsbEnableDHCP.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbEnableDHCP.Enabled = false;
			this.tsbEnableDHCP.Image = (Image)resources.GetObject("tsbEnableDHCP.Image");
			this.tsbEnableDHCP.ImageTransparentColor = Color.Magenta;
			this.tsbEnableDHCP.Name = "tsbEnableDHCP";
			this.tsbEnableDHCP.Size = new Size(23, 22);
			this.tsbEnableDHCP.Text = "toolStripButton1";
			this.tsbEnableDHCP.ToolTipText = "Enable DHCP on the selected adapter";
			this.tsbEnableDHCP.Click += this.tsbEnableDHCP_Click;
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
			this.splitContainer2.Size = new Size(483, 428);
			this.splitContainer2.SplitterDistance = 148;
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
			this.tableLayoutPanel1.Size = new Size(483, 148);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.Anchor = AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(3, 122);
			this.label4.Name = "label4";
			this.label4.Size = new Size(56, 15);
			this.label4.TabIndex = 8;
			this.label4.Text = "Device ID";
			// 
			// txtDeviceID
			// 
			this.txtDeviceID.Anchor = AnchorStyles.Left;
			this.txtDeviceID.Font = new Font("Consolas", 9F);
			this.txtDeviceID.Location = new Point(124, 118);
			this.txtDeviceID.Name = "txtDeviceID";
			this.txtDeviceID.PlaceholderText = "Select an Adapter";
			this.txtDeviceID.ReadOnly = true;
			this.txtDeviceID.Size = new Size(356, 22);
			this.txtDeviceID.TabIndex = 7;
			// 
			// txtDriver
			// 
			this.txtDriver.Anchor = AnchorStyles.Left;
			this.txtDriver.Font = new Font("Consolas", 9F);
			this.txtDriver.Location = new Point(124, 81);
			this.txtDriver.Name = "txtDriver";
			this.txtDriver.PlaceholderText = "Select an Adapter";
			this.txtDriver.ReadOnly = true;
			this.txtDriver.Size = new Size(356, 22);
			this.txtDriver.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Anchor = AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 11);
			this.label1.Name = "label1";
			this.label1.Size = new Size(103, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Hardware Address";
			// 
			// label2
			// 
			this.label2.Anchor = AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(3, 48);
			this.label2.Name = "label2";
			this.label2.Size = new Size(39, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Speed";
			// 
			// txtHardwareAddress
			// 
			this.txtHardwareAddress.Anchor = AnchorStyles.Left;
			this.txtHardwareAddress.Font = new Font("Consolas", 9F);
			this.txtHardwareAddress.Location = new Point(124, 7);
			this.txtHardwareAddress.Name = "txtHardwareAddress";
			this.txtHardwareAddress.PlaceholderText = "Select an Adapter";
			this.txtHardwareAddress.ReadOnly = true;
			this.txtHardwareAddress.Size = new Size(356, 22);
			this.txtHardwareAddress.TabIndex = 2;
			// 
			// txtSpeed
			// 
			this.txtSpeed.Anchor = AnchorStyles.Left;
			this.txtSpeed.Font = new Font("Consolas", 9F);
			this.txtSpeed.Location = new Point(124, 44);
			this.txtSpeed.Name = "txtSpeed";
			this.txtSpeed.PlaceholderText = "Select an Adapter";
			this.txtSpeed.ReadOnly = true;
			this.txtSpeed.Size = new Size(356, 22);
			this.txtSpeed.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Anchor = AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(3, 85);
			this.label3.Name = "label3";
			this.label3.Size = new Size(101, 15);
			this.label3.TabIndex = 4;
			this.label3.Text = "Driver Description";
			// 
			// lsvAddresses
			// 
			this.lsvAddresses.Columns.AddRange(new ColumnHeader[] { this.chAddress, this.chPrefixLength, this.chAddressFamily, this.chPrefixOrigin, this.chSuffixOrigin });
			this.lsvAddresses.Dock = DockStyle.Fill;
			this.lsvAddresses.Location = new Point(0, 0);
			this.lsvAddresses.Name = "lsvAddresses";
			this.lsvAddresses.Size = new Size(483, 276);
			this.lsvAddresses.TabIndex = 0;
			this.lsvAddresses.UseCompatibleStateImageBehavior = false;
			this.lsvAddresses.View = View.Details;
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
			this.cmsNotifyIconMenu.Items.AddRange(new ToolStripItem[] { this.tsmiShow, this.tsmiHide, this.toolStripSeparator1, this.tsmiExit, this.toolStripSeparator2, this.tsmiNoShortcuts });
			this.cmsNotifyIconMenu.Name = "contextMenuStrip1";
			this.cmsNotifyIconMenu.Size = new Size(144, 104);
			// 
			// tsmiShow
			// 
			this.tsmiShow.Name = "tsmiShow";
			this.tsmiShow.Size = new Size(143, 22);
			this.tsmiShow.Text = "&Show";
			this.tsmiShow.ToolTipText = "Show the application";
			this.tsmiShow.Click += this.tsmiShow_Click;
			// 
			// tsmiHide
			// 
			this.tsmiHide.Name = "tsmiHide";
			this.tsmiHide.Size = new Size(143, 22);
			this.tsmiHide.Text = "&Hide";
			this.tsmiHide.ToolTipText = "Hide the application";
			this.tsmiHide.Click += this.tsmiHide_Click;
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(140, 6);
			// 
			// tsmiExit
			// 
			this.tsmiExit.Name = "tsmiExit";
			this.tsmiExit.Size = new Size(143, 22);
			this.tsmiExit.Text = "E&xit";
			this.tsmiExit.ToolTipText = "Exit the application";
			this.tsmiExit.Click += this.tsmiExit_Click;
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(140, 6);
			// 
			// tsmiNoShortcuts
			// 
			this.tsmiNoShortcuts.DoubleClickEnabled = true;
			this.tsmiNoShortcuts.Enabled = false;
			this.tsmiNoShortcuts.Name = "tsmiNoShortcuts";
			this.tsmiNoShortcuts.Size = new Size(143, 22);
			this.tsmiNoShortcuts.Text = "No Shortcuts";
			this.tsmiNoShortcuts.ToolTipText = "There are no IP addess shortcuts saved";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(800, 450);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.ssStatus);
			this.Name = "frmMain";
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
			this.tsAdapters.ResumeLayout(false);
			this.tsAdapters.PerformLayout();
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
		private ToolStripMenuItem tsmiNoShortcuts;
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
		private ToolStripButton tsbRemoveAddress;
		private Label label4;
		private TextBox txtDeviceID;
		private ToolStripButton tsbSetAddress;
		private ToolStripButton tsbEnableDHCP;
	}
}
