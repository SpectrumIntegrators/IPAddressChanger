namespace IPAddressChanger;

partial class frmDHCPServerBusy {
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
		this.helpProvider1 = new HelpProvider();
		this.tableLayoutPanel1 = new TableLayoutPanel();
		this.lblStatus = new Label();
		this.progressBar1 = new ProgressBar();
		this.cmdCancel = new Button();
		this.tableLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		//
		// helpProvider1
		//
		this.helpProvider1.HelpNamespace = "(set at runtime from Resources.ReadmeUrl)";
		//
		// tableLayoutPanel1
		//
		this.tableLayoutPanel1.ColumnCount = 1;
		this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		this.tableLayoutPanel1.Controls.Add(this.lblStatus, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.cmdCancel, 0, 2);
		this.tableLayoutPanel1.Dock = DockStyle.Fill;
		this.tableLayoutPanel1.Location = new Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 3;
		this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 41.72662F));
		this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25.89928F));
		this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 31.6546764F));
		this.tableLayoutPanel1.Size = new Size(444, 139);
		this.tableLayoutPanel1.TabIndex = 2;
		//
		// lblStatus
		//
		this.lblStatus.AutoSize = true;
		this.lblStatus.Dock = DockStyle.Fill;
		this.lblStatus.Location = new Point(3, 0);
		this.lblStatus.Name = "lblStatus";
		this.lblStatus.Size = new Size(438, 58);
		this.lblStatus.TabIndex = 0;
		this.lblStatus.Text = "Working...";
		this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;
		//
		// progressBar1
		//
		this.progressBar1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		this.progressBar1.Location = new Point(3, 64);
		this.progressBar1.Name = "progressBar1";
		this.progressBar1.Size = new Size(438, 23);
		this.progressBar1.Style = ProgressBarStyle.Marquee;
		this.progressBar1.TabIndex = 1;
		//
		// cmdCancel
		//
		this.cmdCancel.Anchor = AnchorStyles.Top;
		this.cmdCancel.Location = new Point(174, 97);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new Size(95, 39);
		this.cmdCancel.TabIndex = 2;
		this.cmdCancel.Text = "Cancel";
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.cmdCancel.Click += this.cmdCancel_Click;
		//
		// frmDHCPServerBusy
		//
		this.AcceptButton = this.cmdCancel;
		this.AutoScaleDimensions = new SizeF(7F, 15F);
		this.AutoScaleMode = AutoScaleMode.Font;
		this.CancelButton = this.cmdCancel;
		this.ClientSize = new Size(444, 139);
		this.Controls.Add(this.tableLayoutPanel1);
		this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
		this.helpProvider1.SetHelpKeyword(this, "dhcp-server-busy-dialog");
		this.helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
		this.helpProvider1.SetShowHelp(this, true);
		this.Name = "frmDHCPServerBusy";
		this.ShowIcon = false;
		this.ShowInTaskbar = false;
		this.SizeGripStyle = SizeGripStyle.Hide;
		this.StartPosition = FormStartPosition.CenterParent;
		this.Text = "DHCP Server";
		this.FormClosing += this.frmDHCPServerBusy_FormClosing;
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.ResumeLayout(false);
	}

	#endregion

	private TableLayoutPanel tableLayoutPanel1;
	private ProgressBar progressBar1;
	private Button cmdCancel;
	public Label lblStatus;
	private HelpProvider helpProvider1;
}
