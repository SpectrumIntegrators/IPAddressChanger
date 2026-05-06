namespace IPAddressChanger;

partial class frmAdapterBusy {
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
		this.lblBusyReason = new Label();
		this.progressBar1 = new ProgressBar();
		this.cmdClose = new Button();
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
		this.tableLayoutPanel1.Controls.Add(this.lblBusyReason, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.cmdClose, 0, 2);
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
		// lblBusyReason
		// 
		this.lblBusyReason.AutoSize = true;
		this.lblBusyReason.Dock = DockStyle.Fill;
		this.lblBusyReason.Location = new Point(3, 0);
		this.lblBusyReason.Name = "lblBusyReason";
		this.lblBusyReason.Size = new Size(438, 58);
		this.lblBusyReason.TabIndex = 0;
		this.lblBusyReason.Text = "Renewing DHCP lease for ADAPTER NAME";
		this.lblBusyReason.TextAlign = ContentAlignment.MiddleCenter;
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
		// cmdClose
		// 
		this.cmdClose.Anchor = AnchorStyles.Top;
		this.cmdClose.Location = new Point(174, 97);
		this.cmdClose.Name = "cmdClose";
		this.cmdClose.Size = new Size(95, 39);
		this.cmdClose.TabIndex = 2;
		this.cmdClose.Text = "Close";
		this.cmdClose.UseVisualStyleBackColor = true;
		this.cmdClose.Click += this.cmdClose_Click;
		// 
		// frmAdapterBusy
		// 
		this.AcceptButton = this.cmdClose;
		this.AutoScaleDimensions = new SizeF(7F, 15F);
		this.AutoScaleMode = AutoScaleMode.Font;
		this.CancelButton = this.cmdClose;
		this.ClientSize = new Size(444, 139);
		this.Controls.Add(this.tableLayoutPanel1);
		this.FormBorderStyle = FormBorderStyle.FixedDialog;
		this.helpProvider1.SetHelpKeyword(this, "adapter-busy-dialog");
		this.helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "frmAdapterBusy";
		this.helpProvider1.SetShowHelp(this, true);
		this.ShowIcon = false;
		this.ShowInTaskbar = false;
		this.SizeGripStyle = SizeGripStyle.Hide;
		this.StartPosition = FormStartPosition.CenterParent;
		this.Text = "frmDHCPRenew";
		this.FormClosing += this.frmAdapterBusy_FormClosing;
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.ResumeLayout(false);
	}

	#endregion

	private TableLayoutPanel tableLayoutPanel1;
	private ProgressBar progressBar1;
	private Button cmdClose;
	public Label lblBusyReason;
	private HelpProvider helpProvider1;
}