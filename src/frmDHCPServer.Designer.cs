using System.ComponentModel;

namespace IPAddressChanger;

partial class frmDHCPServer
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
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
		this.components = new Container();
		TableLayoutPanel tableLayoutPanel1;
		ColumnHeader columnHeader1;
		ColumnHeader columnHeader2;
		ColumnHeader columnHeader3;
		ColumnHeader columnHeader4;
		ColumnHeader columnHeader5;
		ColumnHeader columnHeader6;
		TableLayoutPanel tableLayoutPanel2;
		Label label1;
		Label label7;
		Label label8;
		Label label9;
		this.tsDHCPLeases = new ToolStrip();
		this.tsbAddCustomReservation = new ToolStripButton();
		this.tsbDeleteLease = new ToolStripButton();
		this.tsbEditLease = new ToolStripButton();
		this.lsvDHCPLeases = new ListView();
		this.cmsLeases = new ContextMenuStrip(this.components);
		this.tsmiCopyLease = new ToolStripMenuItem();
		this.tsmiEditLease = new ToolStripMenuItem();
		this.toolStripMenuItem1 = new ToolStripSeparator();
		this.tsmiDeleteLease = new ToolStripMenuItem();
		this.cboAdapters = new ComboBox();
		this.tableLayoutPanel3 = new TableLayoutPanel();
		this.txtAddressOctet4 = new TextBox();
		this.txtAddressOctet3 = new TextBox();
		this.txtAddressOctet2 = new TextBox();
		this.txtAddressOctet1 = new TextBox();
		this.txtPrefixLength = new TextBox();
		this.label3 = new Label();
		this.label4 = new Label();
		this.label5 = new Label();
		this.label6 = new Label();
		this.label2 = new Label();
		this.cmdDHCPProbe = new Button();
		this.cmdRefreshAdapters = new Button();
		this.lblEnableDHCPServer = new Label();
		this.chkEnableDHCPServer = new CheckBox();
		this.tableLayoutPanel4 = new TableLayoutPanel();
		this.txtPoolSize = new TextBox();
		this.txtPoolStartAddress = new TextBox();
		this.optAutoPool = new RadioButton();
		this.optCustomPool = new RadioButton();
		this.helpProvider1 = new HelpProvider();
		tableLayoutPanel1 = new TableLayoutPanel();
		columnHeader1 = new ColumnHeader();
		columnHeader2 = new ColumnHeader();
		columnHeader3 = new ColumnHeader();
		columnHeader4 = new ColumnHeader();
		columnHeader5 = new ColumnHeader();
		columnHeader6 = new ColumnHeader();
		tableLayoutPanel2 = new TableLayoutPanel();
		label1 = new Label();
		label7 = new Label();
		label8 = new Label();
		label9 = new Label();
		tableLayoutPanel1.SuspendLayout();
		this.tsDHCPLeases.SuspendLayout();
		this.cmsLeases.SuspendLayout();
		tableLayoutPanel2.SuspendLayout();
		this.tableLayoutPanel3.SuspendLayout();
		this.tableLayoutPanel4.SuspendLayout();
		this.SuspendLayout();
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 1;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Controls.Add(this.tsDHCPLeases, 0, 1);
		tableLayoutPanel1.Controls.Add(this.lsvDHCPLeases, 0, 2);
		tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
		tableLayoutPanel1.Dock = DockStyle.Fill;
		tableLayoutPanel1.Location = new Point(0, 0);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 3;
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Size = new Size(800, 450);
		tableLayoutPanel1.TabIndex = 0;
		// 
		// tsDHCPLeases
		// 
		this.helpProvider1.SetHelpKeyword(this.tsDHCPLeases, "tool-bar");
		this.helpProvider1.SetHelpNavigator(this.tsDHCPLeases, HelpNavigator.Topic);
		this.tsDHCPLeases.Items.AddRange(new ToolStripItem[] { this.tsbAddCustomReservation, this.tsbDeleteLease, this.tsbEditLease });
		this.tsDHCPLeases.Location = new Point(0, 140);
		this.tsDHCPLeases.Name = "tsDHCPLeases";
		this.helpProvider1.SetShowHelp(this.tsDHCPLeases, true);
		this.tsDHCPLeases.Size = new Size(800, 25);
		this.tsDHCPLeases.TabIndex = 7;
		this.tsDHCPLeases.TabStop = true;
		this.tsDHCPLeases.Text = "toolStrip1";
		// 
		// tsbAddCustomReservation
		// 
		this.tsbAddCustomReservation.DisplayStyle = ToolStripItemDisplayStyle.Image;
		this.tsbAddCustomReservation.Image = Properties.Resources.AddTable_5632;
		this.tsbAddCustomReservation.ImageTransparentColor = Color.Magenta;
		this.tsbAddCustomReservation.Name = "tsbAddCustomReservation";
		this.tsbAddCustomReservation.Size = new Size(23, 22);
		this.tsbAddCustomReservation.Text = "Add Custom Reservation";
		this.tsbAddCustomReservation.Click += this.tsbAddCustomReservation_Click;
		// 
		// tsbDeleteLease
		// 
		this.tsbDeleteLease.DisplayStyle = ToolStripItemDisplayStyle.Image;
		this.tsbDeleteLease.Enabled = false;
		this.tsbDeleteLease.Image = Properties.Resources.DeleteTablefromDatabase_270;
		this.tsbDeleteLease.ImageTransparentColor = Color.Magenta;
		this.tsbDeleteLease.Name = "tsbDeleteLease";
		this.tsbDeleteLease.Size = new Size(23, 22);
		this.tsbDeleteLease.Text = "Delete Lease";
		this.tsbDeleteLease.Click += this.tsbDeleteLease_Click;
		// 
		// tsbEditLease
		// 
		this.tsbEditLease.DisplayStyle = ToolStripItemDisplayStyle.Image;
		this.tsbEditLease.Image = Properties.Resources.EditTableRow_16x;
		this.tsbEditLease.ImageTransparentColor = Color.Magenta;
		this.tsbEditLease.Name = "tsbEditLease";
		this.tsbEditLease.Size = new Size(23, 22);
		this.tsbEditLease.Text = "Edit Lease";
		this.tsbEditLease.ToolTipText = "Edit Lease";
		this.tsbEditLease.Click += this.tsbEditLease_Click;
		// 
		// lsvDHCPLeases
		// 
		this.lsvDHCPLeases.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6 });
		this.lsvDHCPLeases.ContextMenuStrip = this.cmsLeases;
		this.lsvDHCPLeases.Dock = DockStyle.Fill;
		this.lsvDHCPLeases.FullRowSelect = true;
		this.helpProvider1.SetHelpKeyword(this.lsvDHCPLeases, "dhcp-lease-list");
		this.helpProvider1.SetHelpNavigator(this.lsvDHCPLeases, HelpNavigator.Topic);
		this.lsvDHCPLeases.Location = new Point(3, 168);
		this.lsvDHCPLeases.Name = "lsvDHCPLeases";
		this.helpProvider1.SetShowHelp(this.lsvDHCPLeases, true);
		this.lsvDHCPLeases.Size = new Size(794, 279);
		this.lsvDHCPLeases.TabIndex = 8;
		this.lsvDHCPLeases.UseCompatibleStateImageBehavior = false;
		this.lsvDHCPLeases.View = View.Details;
		this.lsvDHCPLeases.SelectedIndexChanged += this.lsvDHCPLeases_SelectedIndexChanged;
		this.lsvDHCPLeases.DoubleClick += this.lsvDHCPLeases_DoubleClick;
		this.lsvDHCPLeases.KeyDown += this.lsvDHCPLeases_KeyDown;
		// 
		// columnHeader1
		// 
		columnHeader1.Text = "MAC Address";
		columnHeader1.Width = 160;
		// 
		// columnHeader2
		// 
		columnHeader2.Text = "IP Address";
		columnHeader2.Width = 120;
		// 
		// columnHeader3
		// 
		columnHeader3.Text = "Hostname";
		columnHeader3.Width = 160;
		// 
		// columnHeader4
		// 
		columnHeader4.Text = "Address Assigned";
		columnHeader4.Width = 120;
		// 
		// columnHeader5
		// 
		columnHeader5.Text = "Lease Expiration";
		columnHeader5.Width = 120;
		// 
		// columnHeader6
		// 
		columnHeader6.Text = "Last Commuication";
		columnHeader6.Width = 110;
		// 
		// cmsLeases
		// 
		this.helpProvider1.SetHelpKeyword(this.cmsLeases, "lease-context-menu");
		this.helpProvider1.SetHelpNavigator(this.cmsLeases, HelpNavigator.Topic);
		this.cmsLeases.Items.AddRange(new ToolStripItem[] { this.tsmiCopyLease, this.tsmiEditLease, this.toolStripMenuItem1, this.tsmiDeleteLease });
		this.cmsLeases.Name = "cmsLeases";
		this.helpProvider1.SetShowHelp(this.cmsLeases, true);
		this.cmsLeases.Size = new Size(108, 76);
		this.cmsLeases.Opening += this.cmsLeases_Opening;
		// 
		// tsmiCopyLease
		// 
		this.tsmiCopyLease.Name = "tsmiCopyLease";
		this.tsmiCopyLease.Size = new Size(107, 22);
		this.tsmiCopyLease.Text = "&Copy";
		this.tsmiCopyLease.Click += this.tsmiCopyLease_Click;
		// 
		// tsmiEditLease
		// 
		this.tsmiEditLease.Name = "tsmiEditLease";
		this.tsmiEditLease.Size = new Size(107, 22);
		this.tsmiEditLease.Text = "&Edit";
		this.tsmiEditLease.Click += this.tsmiEditLease_Click;
		// 
		// toolStripMenuItem1
		// 
		this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		this.toolStripMenuItem1.Size = new Size(104, 6);
		// 
		// tsmiDeleteLease
		// 
		this.tsmiDeleteLease.Name = "tsmiDeleteLease";
		this.tsmiDeleteLease.Size = new Size(107, 22);
		this.tsmiDeleteLease.Text = "&Delete";
		this.tsmiDeleteLease.Click += this.tsmiDeleteLease_Click;
		// 
		// tableLayoutPanel2
		// 
		tableLayoutPanel2.ColumnCount = 3;
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 109F));
		tableLayoutPanel2.Controls.Add(label1, 0, 0);
		tableLayoutPanel2.Controls.Add(this.cboAdapters, 1, 0);
		tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 1);
		tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
		tableLayoutPanel2.Controls.Add(this.cmdDHCPProbe, 2, 1);
		tableLayoutPanel2.Controls.Add(this.cmdRefreshAdapters, 2, 0);
		tableLayoutPanel2.Controls.Add(this.lblEnableDHCPServer, 0, 3);
		tableLayoutPanel2.Controls.Add(this.chkEnableDHCPServer, 1, 3);
		tableLayoutPanel2.Controls.Add(label7, 0, 2);
		tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 2);
		tableLayoutPanel2.Dock = DockStyle.Fill;
		tableLayoutPanel2.Location = new Point(3, 3);
		tableLayoutPanel2.Name = "tableLayoutPanel2";
		tableLayoutPanel2.RowCount = 4;
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
		tableLayoutPanel2.Size = new Size(794, 134);
		tableLayoutPanel2.TabIndex = 2;
		// 
		// label1
		// 
		label1.Anchor = AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new Point(28, 9);
		label1.Name = "label1";
		label1.Size = new Size(49, 15);
		label1.TabIndex = 0;
		label1.Text = "Adapter";
		// 
		// cboAdapters
		// 
		this.cboAdapters.Dock = DockStyle.Fill;
		this.cboAdapters.DropDownStyle = ComboBoxStyle.DropDownList;
		this.cboAdapters.FormattingEnabled = true;
		this.helpProvider1.SetHelpKeyword(this.cboAdapters, "adapter-selection");
		this.helpProvider1.SetHelpNavigator(this.cboAdapters, HelpNavigator.Topic);
		this.cboAdapters.Location = new Point(83, 3);
		this.cboAdapters.Name = "cboAdapters";
		this.helpProvider1.SetShowHelp(this.cboAdapters, true);
		this.cboAdapters.Size = new Size(599, 23);
		this.cboAdapters.TabIndex = 0;
		this.cboAdapters.SelectedIndexChanged += this.cboAdapters_SelectedIndexChanged;
		// 
		// tableLayoutPanel3
		// 
		this.tableLayoutPanel3.ColumnCount = 9;
		this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
		this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
		this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
		this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
		this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
		this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
		this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
		this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
		this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
		this.tableLayoutPanel3.Controls.Add(this.txtAddressOctet4, 6, 0);
		this.tableLayoutPanel3.Controls.Add(this.txtAddressOctet3, 4, 0);
		this.tableLayoutPanel3.Controls.Add(this.txtAddressOctet2, 2, 0);
		this.tableLayoutPanel3.Controls.Add(this.txtAddressOctet1, 0, 0);
		this.tableLayoutPanel3.Controls.Add(this.txtPrefixLength, 8, 0);
		this.tableLayoutPanel3.Controls.Add(this.label3, 1, 0);
		this.tableLayoutPanel3.Controls.Add(this.label4, 3, 0);
		this.tableLayoutPanel3.Controls.Add(this.label5, 5, 0);
		this.tableLayoutPanel3.Controls.Add(this.label6, 7, 0);
		this.tableLayoutPanel3.Location = new Point(83, 36);
		this.tableLayoutPanel3.Name = "tableLayoutPanel3";
		this.tableLayoutPanel3.RowCount = 1;
		this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		this.tableLayoutPanel3.Size = new Size(386, 26);
		this.tableLayoutPanel3.TabIndex = 2;
		// 
		// txtAddressOctet4
		// 
		this.txtAddressOctet4.Font = new Font("Consolas", 9F);
		this.helpProvider1.SetHelpKeyword(this.txtAddressOctet4, "address-and-prefix-entry");
		this.helpProvider1.SetHelpNavigator(this.txtAddressOctet4, HelpNavigator.Topic);
		this.txtAddressOctet4.Location = new Point(246, 3);
		this.txtAddressOctet4.MaxLength = 3;
		this.txtAddressOctet4.Name = "txtAddressOctet4";
		this.txtAddressOctet4.PlaceholderText = "255";
		this.helpProvider1.SetShowHelp(this.txtAddressOctet4, true);
		this.txtAddressOctet4.Size = new Size(55, 22);
		this.txtAddressOctet4.TabIndex = 4;
		// 
		// txtAddressOctet3
		// 
		this.txtAddressOctet3.Font = new Font("Consolas", 9F);
		this.helpProvider1.SetHelpKeyword(this.txtAddressOctet3, "address-and-prefix-entry");
		this.helpProvider1.SetHelpNavigator(this.txtAddressOctet3, HelpNavigator.Topic);
		this.txtAddressOctet3.Location = new Point(165, 3);
		this.txtAddressOctet3.MaxLength = 3;
		this.txtAddressOctet3.Name = "txtAddressOctet3";
		this.txtAddressOctet3.PlaceholderText = "255";
		this.helpProvider1.SetShowHelp(this.txtAddressOctet3, true);
		this.txtAddressOctet3.Size = new Size(55, 22);
		this.txtAddressOctet3.TabIndex = 3;
		// 
		// txtAddressOctet2
		// 
		this.txtAddressOctet2.Font = new Font("Consolas", 9F);
		this.helpProvider1.SetHelpKeyword(this.txtAddressOctet2, "address-and-prefix-entry");
		this.helpProvider1.SetHelpNavigator(this.txtAddressOctet2, HelpNavigator.Topic);
		this.txtAddressOctet2.Location = new Point(84, 3);
		this.txtAddressOctet2.MaxLength = 3;
		this.txtAddressOctet2.Name = "txtAddressOctet2";
		this.txtAddressOctet2.PlaceholderText = "255";
		this.helpProvider1.SetShowHelp(this.txtAddressOctet2, true);
		this.txtAddressOctet2.Size = new Size(55, 22);
		this.txtAddressOctet2.TabIndex = 2;
		// 
		// txtAddressOctet1
		// 
		this.txtAddressOctet1.Font = new Font("Consolas", 9F);
		this.helpProvider1.SetHelpKeyword(this.txtAddressOctet1, "address-and-prefix-entry");
		this.helpProvider1.SetHelpNavigator(this.txtAddressOctet1, HelpNavigator.Topic);
		this.txtAddressOctet1.Location = new Point(3, 3);
		this.txtAddressOctet1.MaxLength = 3;
		this.txtAddressOctet1.Name = "txtAddressOctet1";
		this.txtAddressOctet1.PlaceholderText = "255";
		this.helpProvider1.SetShowHelp(this.txtAddressOctet1, true);
		this.txtAddressOctet1.Size = new Size(55, 22);
		this.txtAddressOctet1.TabIndex = 1;
		// 
		// txtPrefixLength
		// 
		this.txtPrefixLength.Font = new Font("Consolas", 9F);
		this.helpProvider1.SetHelpKeyword(this.txtPrefixLength, "address-and-prefix-entry");
		this.helpProvider1.SetHelpNavigator(this.txtPrefixLength, HelpNavigator.Topic);
		this.txtPrefixLength.Location = new Point(327, 3);
		this.txtPrefixLength.MaxLength = 2;
		this.txtPrefixLength.Name = "txtPrefixLength";
		this.txtPrefixLength.PlaceholderText = "32";
		this.helpProvider1.SetShowHelp(this.txtPrefixLength, true);
		this.txtPrefixLength.Size = new Size(56, 22);
		this.txtPrefixLength.TabIndex = 5;
		// 
		// label3
		// 
		this.label3.Anchor = AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Font = new Font("Consolas", 12F, FontStyle.Bold);
		this.label3.Location = new Point(64, 3);
		this.label3.Name = "label3";
		this.label3.Size = new Size(14, 19);
		this.label3.TabIndex = 5;
		this.label3.Text = ".";
		// 
		// label4
		// 
		this.label4.Anchor = AnchorStyles.Left;
		this.label4.AutoSize = true;
		this.label4.Font = new Font("Consolas", 12F, FontStyle.Bold);
		this.label4.Location = new Point(145, 3);
		this.label4.Name = "label4";
		this.label4.Size = new Size(14, 19);
		this.label4.TabIndex = 6;
		this.label4.Text = ".";
		// 
		// label5
		// 
		this.label5.Anchor = AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Font = new Font("Consolas", 12F, FontStyle.Bold);
		this.label5.Location = new Point(226, 3);
		this.label5.Name = "label5";
		this.label5.Size = new Size(14, 19);
		this.label5.TabIndex = 7;
		this.label5.Text = ".";
		// 
		// label6
		// 
		this.label6.Anchor = AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Font = new Font("Consolas", 12F, FontStyle.Bold);
		this.label6.Location = new Point(307, 3);
		this.label6.Name = "label6";
		this.label6.Size = new Size(14, 19);
		this.label6.TabIndex = 8;
		this.label6.Text = "/";
		// 
		// label2
		// 
		this.label2.Anchor = AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Location = new Point(28, 42);
		this.label2.Name = "label2";
		this.label2.Size = new Size(49, 15);
		this.label2.TabIndex = 3;
		this.label2.Text = "Address";
		// 
		// cmdDHCPProbe
		// 
		this.cmdDHCPProbe.Enabled = false;
		this.helpProvider1.SetHelpKeyword(this.cmdDHCPProbe, "dhcp-discover-preflight-check");
		this.helpProvider1.SetHelpNavigator(this.cmdDHCPProbe, HelpNavigator.Topic);
		this.cmdDHCPProbe.Location = new Point(688, 36);
		this.cmdDHCPProbe.Name = "cmdDHCPProbe";
		this.helpProvider1.SetShowHelp(this.cmdDHCPProbe, true);
		this.cmdDHCPProbe.Size = new Size(103, 26);
		this.cmdDHCPProbe.TabIndex = 7;
		this.cmdDHCPProbe.Text = "Probe";
		this.cmdDHCPProbe.UseVisualStyleBackColor = true;
		this.cmdDHCPProbe.Click += this.cmdDHCPProbe_Click;
		// 
		// cmdRefreshAdapters
		// 
		this.cmdRefreshAdapters.Dock = DockStyle.Fill;
		this.helpProvider1.SetHelpKeyword(this.cmdRefreshAdapters, "adapter-selection");
		this.helpProvider1.SetHelpNavigator(this.cmdRefreshAdapters, HelpNavigator.Topic);
		this.cmdRefreshAdapters.Location = new Point(688, 3);
		this.cmdRefreshAdapters.Name = "cmdRefreshAdapters";
		this.helpProvider1.SetShowHelp(this.cmdRefreshAdapters, true);
		this.cmdRefreshAdapters.Size = new Size(103, 27);
		this.cmdRefreshAdapters.TabIndex = 8;
		this.cmdRefreshAdapters.Text = "Refresh";
		this.cmdRefreshAdapters.UseVisualStyleBackColor = true;
		this.cmdRefreshAdapters.Click += this.cmdRefreshAdapters_Click;
		// 
		// lblEnableDHCPServer
		// 
		this.lblEnableDHCPServer.Anchor = AnchorStyles.Right;
		this.lblEnableDHCPServer.AutoSize = true;
		this.lblEnableDHCPServer.Location = new Point(35, 109);
		this.lblEnableDHCPServer.Name = "lblEnableDHCPServer";
		this.lblEnableDHCPServer.Size = new Size(42, 15);
		this.lblEnableDHCPServer.TabIndex = 4;
		this.lblEnableDHCPServer.Text = "Enable";
		// 
		// chkEnableDHCPServer
		// 
		this.chkEnableDHCPServer.Anchor = AnchorStyles.Left;
		this.chkEnableDHCPServer.AutoSize = true;
		this.helpProvider1.SetHelpKeyword(this.chkEnableDHCPServer, "enable-dhcp-server");
		this.helpProvider1.SetHelpNavigator(this.chkEnableDHCPServer, HelpNavigator.Topic);
		this.chkEnableDHCPServer.Location = new Point(83, 109);
		this.chkEnableDHCPServer.Name = "chkEnableDHCPServer";
		this.helpProvider1.SetShowHelp(this.chkEnableDHCPServer, true);
		this.chkEnableDHCPServer.Size = new Size(15, 14);
		this.chkEnableDHCPServer.TabIndex = 6;
		this.chkEnableDHCPServer.UseVisualStyleBackColor = true;
		this.chkEnableDHCPServer.CheckedChanged += this.chkEnableDHCPServer_CheckedChanged;
		this.chkEnableDHCPServer.Click += this.chkEnableDHCPServer_Click;
		// 
		// label7
		// 
		label7.Anchor = AnchorStyles.Right;
		label7.AutoSize = true;
		label7.Location = new Point(46, 75);
		label7.Name = "label7";
		label7.Size = new Size(31, 15);
		label7.TabIndex = 9;
		label7.Text = "Pool";
		// 
		// tableLayoutPanel4
		// 
		this.tableLayoutPanel4.ColumnCount = 6;
		this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
		this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
		this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
		this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
		this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
		this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
		this.tableLayoutPanel4.Controls.Add(this.txtPoolSize, 5, 0);
		this.tableLayoutPanel4.Controls.Add(label8, 4, 0);
		this.tableLayoutPanel4.Controls.Add(label9, 2, 0);
		this.tableLayoutPanel4.Controls.Add(this.txtPoolStartAddress, 3, 0);
		this.tableLayoutPanel4.Controls.Add(this.optAutoPool, 0, 0);
		this.tableLayoutPanel4.Controls.Add(this.optCustomPool, 1, 0);
		this.tableLayoutPanel4.Dock = DockStyle.Fill;
		this.tableLayoutPanel4.Location = new Point(80, 66);
		this.tableLayoutPanel4.Margin = new Padding(0);
		this.tableLayoutPanel4.Name = "tableLayoutPanel4";
		this.tableLayoutPanel4.RowCount = 1;
		this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		this.tableLayoutPanel4.Size = new Size(605, 33);
		this.tableLayoutPanel4.TabIndex = 10;
		// 
		// txtPoolSize
		//
		this.txtPoolSize.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		this.txtPoolSize.Enabled = false;
		this.helpProvider1.SetHelpKeyword(this.txtPoolSize, "dhcp-pool");
		this.helpProvider1.SetHelpNavigator(this.txtPoolSize, HelpNavigator.Topic);
		this.txtPoolSize.Location = new Point(524, 5);
		this.txtPoolSize.Name = "txtPoolSize";
		this.txtPoolSize.PlaceholderText = "50";
		this.helpProvider1.SetShowHelp(this.txtPoolSize, true);
		this.txtPoolSize.Size = new Size(78, 23);
		this.txtPoolSize.TabIndex = 0;
		// 
		// label8
		// 
		label8.Anchor = AnchorStyles.Right;
		label8.AutoSize = true;
		label8.Location = new Point(491, 9);
		label8.Name = "label8";
		label8.Size = new Size(27, 15);
		label8.TabIndex = 1;
		label8.Text = "Size";
		// 
		// label9
		// 
		label9.Anchor = AnchorStyles.Right;
		label9.AutoSize = true;
		label9.Location = new Point(176, 9);
		label9.Name = "label9";
		label9.Size = new Size(31, 15);
		label9.TabIndex = 2;
		label9.Text = "Start";
		// 
		// txtPoolStartAddress
		//
		this.txtPoolStartAddress.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		this.txtPoolStartAddress.Enabled = false;
		this.helpProvider1.SetHelpKeyword(this.txtPoolStartAddress, "dhcp-pool");
		this.helpProvider1.SetHelpNavigator(this.txtPoolStartAddress, HelpNavigator.Topic);
		this.txtPoolStartAddress.Location = new Point(213, 5);
		this.txtPoolStartAddress.Name = "txtPoolStartAddress";
		this.txtPoolStartAddress.PlaceholderText = "0.0.0.0";
		this.helpProvider1.SetShowHelp(this.txtPoolStartAddress, true);
		this.txtPoolStartAddress.Size = new Size(245, 23);
		this.txtPoolStartAddress.TabIndex = 3;
		// 
		// optAutoPool
		//
		this.optAutoPool.Anchor = AnchorStyles.Left;
		this.optAutoPool.AutoSize = true;
		this.optAutoPool.Checked = true;
		this.helpProvider1.SetHelpKeyword(this.optAutoPool, "dhcp-pool");
		this.helpProvider1.SetHelpNavigator(this.optAutoPool, HelpNavigator.Topic);
		this.optAutoPool.Location = new Point(3, 7);
		this.optAutoPool.Name = "optAutoPool";
		this.helpProvider1.SetShowHelp(this.optAutoPool, true);
		this.optAutoPool.Size = new Size(51, 19);
		this.optAutoPool.TabIndex = 4;
		this.optAutoPool.TabStop = true;
		this.optAutoPool.Text = "Auto";
		this.optAutoPool.UseVisualStyleBackColor = true;
		this.optAutoPool.CheckedChanged += this.optAutoPool_CheckedChanged;
		//
		// optCustomPool
		//
		this.optCustomPool.Anchor = AnchorStyles.Left;
		this.optCustomPool.AutoSize = true;
		this.helpProvider1.SetHelpKeyword(this.optCustomPool, "dhcp-pool");
		this.helpProvider1.SetHelpNavigator(this.optCustomPool, HelpNavigator.Topic);
		this.optCustomPool.Location = new Point(73, 7);
		this.optCustomPool.Name = "optCustomPool";
		this.helpProvider1.SetShowHelp(this.optCustomPool, true);
		this.optCustomPool.Size = new Size(67, 19);
		this.optCustomPool.TabIndex = 5;
		this.optCustomPool.Text = "Custom";
		this.optCustomPool.UseVisualStyleBackColor = true;
		this.optCustomPool.CheckedChanged += this.optCustomPool_CheckedChanged;
		// 
		// helpProvider1
		// 
		this.helpProvider1.HelpNamespace = "(set at runtime from Resources.ReadmeUrl)";
		// 
		// frmDHCPServer
		// 
		this.AutoScaleDimensions = new SizeF(7F, 15F);
		this.AutoScaleMode = AutoScaleMode.Font;
		this.ClientSize = new Size(800, 450);
		this.Controls.Add(tableLayoutPanel1);
		this.helpProvider1.SetHelpKeyword(this, "dhcp-server-1");
		this.helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
		this.Name = "frmDHCPServer";
		this.helpProvider1.SetShowHelp(this, true);
		this.Text = "DHCP Server";
		this.FormClosing += this.frmDHCPServer_FormClosing;
		this.Load += this.frmDHCPServer_Load;
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		this.tsDHCPLeases.ResumeLayout(false);
		this.tsDHCPLeases.PerformLayout();
		this.cmsLeases.ResumeLayout(false);
		tableLayoutPanel2.ResumeLayout(false);
		tableLayoutPanel2.PerformLayout();
		this.tableLayoutPanel3.ResumeLayout(false);
		this.tableLayoutPanel3.PerformLayout();
		this.tableLayoutPanel4.ResumeLayout(false);
		this.tableLayoutPanel4.PerformLayout();
		this.ResumeLayout(false);
	}

	#endregion

	private ToolStrip tsDHCPLeases;
	private ListView lsvDHCPLeases;
	private ComboBox cboAdapters;
	private TableLayoutPanel tableLayoutPanel3;
	private TextBox txtPrefixLength;
	private Label label2;
	private Label label3;
	private Label label4;
	private Label label5;
	private Label label6;
	private ToolStripButton tsbAddCustomReservation;
	private ToolStripButton tsbDeleteLease;
	private Label lblEnableDHCPServer;
	private CheckBox chkEnableDHCPServer;
	private TextBox txtAddressOctet4;
	private TextBox txtAddressOctet3;
	private TextBox txtAddressOctet2;
	private TextBox txtAddressOctet1;
	private ContextMenuStrip cmsLeases;
	private ToolStripMenuItem tsmiCopyLease;
	private ToolStripMenuItem tsmiEditLease;
	private ToolStripSeparator toolStripMenuItem1;
	private ToolStripMenuItem tsmiDeleteLease;
	private ToolStripButton tsbEditLease;
	private Button cmdDHCPProbe;
	private HelpProvider helpProvider1;
	private Button cmdRefreshAdapters;
	private TableLayoutPanel tableLayoutPanel4;
	private TextBox txtPoolSize;
	private TextBox txtPoolStartAddress;
	private RadioButton optAutoPool;
	private RadioButton optCustomPool;
}