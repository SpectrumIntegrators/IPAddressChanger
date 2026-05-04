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
		TableLayoutPanel tableLayoutPanel1;
		ColumnHeader columnHeader1;
		ColumnHeader columnHeader2;
		ColumnHeader columnHeader3;
		ColumnHeader columnHeader4;
		ColumnHeader columnHeader5;
		ColumnHeader columnHeader6;
		TableLayoutPanel tableLayoutPanel2;
		Label label1;
		this.tsDHCPLeases = new ToolStrip();
		this.tsbAddCustomReservation = new ToolStripButton();
		this.tsbDeleteLease = new ToolStripButton();
		this.lsvDHCPLeases = new ListView();
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
		this.lblEnableDHCPServer = new Label();
		this.chkEnableDHCPServer = new CheckBox();
		tableLayoutPanel1 = new TableLayoutPanel();
		columnHeader1 = new ColumnHeader();
		columnHeader2 = new ColumnHeader();
		columnHeader3 = new ColumnHeader();
		columnHeader4 = new ColumnHeader();
		columnHeader5 = new ColumnHeader();
		columnHeader6 = new ColumnHeader();
		tableLayoutPanel2 = new TableLayoutPanel();
		label1 = new Label();
		tableLayoutPanel1.SuspendLayout();
		this.tsDHCPLeases.SuspendLayout();
		tableLayoutPanel2.SuspendLayout();
		this.tableLayoutPanel3.SuspendLayout();
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
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Size = new Size(800, 450);
		tableLayoutPanel1.TabIndex = 0;
		// 
		// tsDHCPLeases
		// 
		this.tsDHCPLeases.Items.AddRange(new ToolStripItem[] { this.tsbAddCustomReservation, this.tsbDeleteLease });
		this.tsDHCPLeases.Location = new Point(0, 105);
		this.tsDHCPLeases.Name = "tsDHCPLeases";
		this.tsDHCPLeases.Size = new Size(800, 25);
		this.tsDHCPLeases.TabIndex = 0;
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
		// lsvDHCPLeases
		// 
		this.lsvDHCPLeases.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6 });
		this.lsvDHCPLeases.Dock = DockStyle.Fill;
		this.lsvDHCPLeases.FullRowSelect = true;
		this.lsvDHCPLeases.Location = new Point(3, 133);
		this.lsvDHCPLeases.Name = "lsvDHCPLeases";
		this.lsvDHCPLeases.Size = new Size(794, 314);
		this.lsvDHCPLeases.TabIndex = 1;
		this.lsvDHCPLeases.UseCompatibleStateImageBehavior = false;
		this.lsvDHCPLeases.View = View.Details;
		this.lsvDHCPLeases.SelectedIndexChanged += this.lsvDHCPLeases_SelectedIndexChanged;
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
		// tableLayoutPanel2
		// 
		tableLayoutPanel2.ColumnCount = 2;
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.Controls.Add(label1, 0, 0);
		tableLayoutPanel2.Controls.Add(this.cboAdapters, 1, 0);
		tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 1);
		tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
		tableLayoutPanel2.Controls.Add(this.lblEnableDHCPServer, 0, 2);
		tableLayoutPanel2.Controls.Add(this.chkEnableDHCPServer, 1, 2);
		tableLayoutPanel2.Dock = DockStyle.Fill;
		tableLayoutPanel2.Location = new Point(3, 3);
		tableLayoutPanel2.Name = "tableLayoutPanel2";
		tableLayoutPanel2.RowCount = 3;
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
		tableLayoutPanel2.Size = new Size(794, 99);
		tableLayoutPanel2.TabIndex = 2;
		// 
		// label1
		// 
		label1.Anchor = AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new Point(28, 8);
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
		this.cboAdapters.Location = new Point(83, 3);
		this.cboAdapters.Name = "cboAdapters";
		this.cboAdapters.Size = new Size(708, 23);
		this.cboAdapters.TabIndex = 1;
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
		this.tableLayoutPanel3.Location = new Point(83, 35);
		this.tableLayoutPanel3.Name = "tableLayoutPanel3";
		this.tableLayoutPanel3.RowCount = 1;
		this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		this.tableLayoutPanel3.Size = new Size(386, 26);
		this.tableLayoutPanel3.TabIndex = 2;
		// 
		// txtAddressOctet4
		// 
		this.txtAddressOctet4.Font = new Font("Consolas", 9F);
		this.txtAddressOctet4.Location = new Point(246, 3);
		this.txtAddressOctet4.MaxLength = 3;
		this.txtAddressOctet4.Name = "txtAddressOctet4";
		this.txtAddressOctet4.PlaceholderText = "255";
		this.txtAddressOctet4.Size = new Size(55, 22);
		this.txtAddressOctet4.TabIndex = 12;
		// 
		// txtAddressOctet3
		// 
		this.txtAddressOctet3.Font = new Font("Consolas", 9F);
		this.txtAddressOctet3.Location = new Point(165, 3);
		this.txtAddressOctet3.MaxLength = 3;
		this.txtAddressOctet3.Name = "txtAddressOctet3";
		this.txtAddressOctet3.PlaceholderText = "255";
		this.txtAddressOctet3.Size = new Size(55, 22);
		this.txtAddressOctet3.TabIndex = 11;
		// 
		// txtAddressOctet2
		// 
		this.txtAddressOctet2.Font = new Font("Consolas", 9F);
		this.txtAddressOctet2.Location = new Point(84, 3);
		this.txtAddressOctet2.MaxLength = 3;
		this.txtAddressOctet2.Name = "txtAddressOctet2";
		this.txtAddressOctet2.PlaceholderText = "255";
		this.txtAddressOctet2.Size = new Size(55, 22);
		this.txtAddressOctet2.TabIndex = 10;
		// 
		// txtAddressOctet1
		// 
		this.txtAddressOctet1.Font = new Font("Consolas", 9F);
		this.txtAddressOctet1.Location = new Point(3, 3);
		this.txtAddressOctet1.MaxLength = 3;
		this.txtAddressOctet1.Name = "txtAddressOctet1";
		this.txtAddressOctet1.PlaceholderText = "255";
		this.txtAddressOctet1.Size = new Size(55, 22);
		this.txtAddressOctet1.TabIndex = 9;
		// 
		// txtPrefixLength
		// 
		this.txtPrefixLength.Font = new Font("Consolas", 9F);
		this.txtPrefixLength.Location = new Point(327, 3);
		this.txtPrefixLength.MaxLength = 2;
		this.txtPrefixLength.Name = "txtPrefixLength";
		this.txtPrefixLength.PlaceholderText = "32";
		this.txtPrefixLength.Size = new Size(56, 22);
		this.txtPrefixLength.TabIndex = 0;
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
		this.label2.Location = new Point(28, 40);
		this.label2.Name = "label2";
		this.label2.Size = new Size(49, 15);
		this.label2.TabIndex = 3;
		this.label2.Text = "Address";
		// 
		// lblEnableDHCPServer
		// 
		this.lblEnableDHCPServer.Anchor = AnchorStyles.Right;
		this.lblEnableDHCPServer.AutoSize = true;
		this.lblEnableDHCPServer.Location = new Point(35, 74);
		this.lblEnableDHCPServer.Name = "lblEnableDHCPServer";
		this.lblEnableDHCPServer.Size = new Size(42, 15);
		this.lblEnableDHCPServer.TabIndex = 4;
		this.lblEnableDHCPServer.Text = "Enable";
		// 
		// chkEnableDHCPServer
		// 
		this.chkEnableDHCPServer.Anchor = AnchorStyles.Left;
		this.chkEnableDHCPServer.AutoSize = true;
		this.chkEnableDHCPServer.Location = new Point(83, 74);
		this.chkEnableDHCPServer.Name = "chkEnableDHCPServer";
		this.chkEnableDHCPServer.Size = new Size(15, 14);
		this.chkEnableDHCPServer.TabIndex = 5;
		this.chkEnableDHCPServer.UseVisualStyleBackColor = true;
		this.chkEnableDHCPServer.CheckedChanged += this.chkEnableDHCPServer_CheckedChanged;
		this.chkEnableDHCPServer.Click += this.chkEnableDHCPServer_Click;
		// 
		// frmDHCPServer
		// 
		this.AutoScaleDimensions = new SizeF(7F, 15F);
		this.AutoScaleMode = AutoScaleMode.Font;
		this.ClientSize = new Size(800, 450);
		this.Controls.Add(tableLayoutPanel1);
		this.Name = "frmDHCPServer";
		this.Text = "DHCP Server";
		this.FormClosing += this.frmDHCPServer_FormClosing;
		this.Load += this.frmDHCPServer_Load;
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		this.tsDHCPLeases.ResumeLayout(false);
		this.tsDHCPLeases.PerformLayout();
		tableLayoutPanel2.ResumeLayout(false);
		tableLayoutPanel2.PerformLayout();
		this.tableLayoutPanel3.ResumeLayout(false);
		this.tableLayoutPanel3.PerformLayout();
		this.ResumeLayout(false);
	}

	#endregion

	private TableLayoutPanel tableLayoutPanel1;
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
	private ColumnHeader columnHeader1;
	private ColumnHeader columnHeader2;
	private ColumnHeader columnHeader3;
	private ColumnHeader columnHeader4;
	private ColumnHeader columnHeader5;
	private Label lblEnableDHCPServer;
	private CheckBox chkEnableDHCPServer;
	private TextBox txtAddressOctet4;
	private TextBox txtAddressOctet3;
	private TextBox txtAddressOctet2;
	private TextBox txtAddressOctet1;
}