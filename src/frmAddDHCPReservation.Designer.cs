namespace IPAddressChanger;

partial class frmAddDHCPReservation {
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
		Label label1;
		Label label2;
		this.tableLayoutPanel1 = new TableLayoutPanel();
		this.tableLayoutPanel2 = new TableLayoutPanel();
		this.cmdOK = new Button();
		this.cmdCancel = new Button();
		this.txtMACAddress = new TextBox();
		this.txtIPAddress = new TextBox();
		label1 = new Label();
		label2 = new Label();
		this.tableLayoutPanel1.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		this.SuspendLayout();
		// 
		// label1
		// 
		label1.Anchor = AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new Point(12, 8);
		label1.Name = "label1";
		label1.Size = new Size(79, 15);
		label1.TabIndex = 3;
		label1.Text = "MAC Address";
		// 
		// label2
		// 
		label2.Anchor = AnchorStyles.Right;
		label2.AutoSize = true;
		label2.Location = new Point(29, 39);
		label2.Name = "label2";
		label2.Size = new Size(62, 15);
		label2.TabIndex = 4;
		label2.Text = "IP Address";
		// 
		// tableLayoutPanel1
		// 
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
		this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
		this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
		this.tableLayoutPanel1.Controls.Add(this.txtMACAddress, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.txtIPAddress, 1, 1);
		this.tableLayoutPanel1.Controls.Add(label1, 0, 0);
		this.tableLayoutPanel1.Controls.Add(label2, 0, 1);
		this.tableLayoutPanel1.Dock = DockStyle.Fill;
		this.tableLayoutPanel1.Location = new Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 3;
		this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
		this.tableLayoutPanel1.Size = new Size(376, 102);
		this.tableLayoutPanel1.TabIndex = 0;
		// 
		// tableLayoutPanel2
		// 
		this.tableLayoutPanel2.Anchor = AnchorStyles.Right;
		this.tableLayoutPanel2.ColumnCount = 2;
		this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		this.tableLayoutPanel2.Controls.Add(this.cmdOK, 0, 0);
		this.tableLayoutPanel2.Controls.Add(this.cmdCancel, 1, 0);
		this.tableLayoutPanel2.Location = new Point(173, 65);
		this.tableLayoutPanel2.Name = "tableLayoutPanel2";
		this.tableLayoutPanel2.RowCount = 1;
		this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		this.tableLayoutPanel2.Size = new Size(200, 34);
		this.tableLayoutPanel2.TabIndex = 2;
		// 
		// cmdOK
		// 
		this.cmdOK.Dock = DockStyle.Fill;
		this.cmdOK.Location = new Point(3, 3);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new Size(94, 28);
		this.cmdOK.TabIndex = 3;
		this.cmdOK.Text = "&OK";
		this.cmdOK.UseVisualStyleBackColor = true;
		this.cmdOK.Click += this.cmdOK_Click;
		// 
		// cmdCancel
		// 
		this.cmdCancel.DialogResult = DialogResult.Cancel;
		this.cmdCancel.Dock = DockStyle.Fill;
		this.cmdCancel.Location = new Point(103, 3);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new Size(94, 28);
		this.cmdCancel.TabIndex = 2;
		this.cmdCancel.Text = "&Cancel";
		this.cmdCancel.UseVisualStyleBackColor = true;
		// 
		// txtMACAddress
		// 
		this.txtMACAddress.Dock = DockStyle.Fill;
		this.txtMACAddress.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
		this.txtMACAddress.Location = new Point(97, 3);
		this.txtMACAddress.MaxLength = 17;
		this.txtMACAddress.Name = "txtMACAddress";
		this.txtMACAddress.PlaceholderText = "11:22:33:44:55:66";
		this.txtMACAddress.Size = new Size(276, 22);
		this.txtMACAddress.TabIndex = 0;
		this.txtMACAddress.WordWrap = false;
		// 
		// txtIPAddress
		// 
		this.txtIPAddress.Dock = DockStyle.Top;
		this.txtIPAddress.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
		this.txtIPAddress.Location = new Point(97, 34);
		this.txtIPAddress.MaxLength = 15;
		this.txtIPAddress.Name = "txtIPAddress";
		this.txtIPAddress.PlaceholderText = "10.10.10.10";
		this.txtIPAddress.Size = new Size(276, 22);
		this.txtIPAddress.TabIndex = 1;
		this.txtIPAddress.WordWrap = false;
		// 
		// frmAddDHCPReservation
		// 
		this.AcceptButton = this.cmdOK;
		this.AutoScaleDimensions = new SizeF(7F, 15F);
		this.AutoScaleMode = AutoScaleMode.Font;
		this.CancelButton = this.cmdCancel;
		this.ClientSize = new Size(376, 102);
		this.ControlBox = false;
		this.Controls.Add(this.tableLayoutPanel1);
		this.FormBorderStyle = FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "frmAddDHCPReservation";
		this.ShowIcon = false;
		this.ShowInTaskbar = false;
		this.StartPosition = FormStartPosition.CenterParent;
		this.Text = "Add DHCP Reservation";
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.tableLayoutPanel2.ResumeLayout(false);
		this.ResumeLayout(false);
	}

	#endregion

	private TableLayoutPanel tableLayoutPanel1;
	private TableLayoutPanel tableLayoutPanel2;
	private Button cmdOK;
	private Button cmdCancel;
	public TextBox txtMACAddress;
	public TextBox txtIPAddress;
}