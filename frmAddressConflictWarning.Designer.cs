namespace IPAddressChanger;

partial class frmAddressConflictWarning {
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
		this.cmdClose = new Button();
		this.tableLayoutPanel2 = new TableLayoutPanel();
		this.txtConflictWarning = new TextBox();
		this.pictureBox1 = new PictureBox();
		this.chkSuppressMessages = new CheckBox();
		this.tableLayoutPanel1.SuspendLayout();
		this.tableLayoutPanel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.SuspendLayout();
		//
		// helpProvider1
		//
		this.helpProvider1.HelpNamespace = "https://spectrumintegrators.github.io/IPAddressChanger/";
		// 
		// tableLayoutPanel1
		// 
		this.tableLayoutPanel1.ColumnCount = 1;
		this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
		this.tableLayoutPanel1.Controls.Add(this.cmdClose, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.chkSuppressMessages, 0, 2);
		this.tableLayoutPanel1.Dock = DockStyle.Fill;
		this.tableLayoutPanel1.Location = new Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 3;
		this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 69.06475F));
		this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30.9352512F));
		this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
		this.tableLayoutPanel1.Size = new Size(444, 173);
		this.tableLayoutPanel1.TabIndex = 2;
		// 
		// cmdClose
		// 
		this.cmdClose.Anchor = AnchorStyles.Top;
		this.cmdClose.Location = new Point(174, 105);
		this.cmdClose.Name = "cmdClose";
		this.cmdClose.Size = new Size(95, 37);
		this.cmdClose.TabIndex = 2;
		this.cmdClose.Text = "Close";
		this.cmdClose.UseVisualStyleBackColor = true;
		this.cmdClose.Click += this.cmdClose_Click;
		// 
		// tableLayoutPanel2
		// 
		this.tableLayoutPanel2.ColumnCount = 2;
		this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.5251141F));
		this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 84.474884F));
		this.tableLayoutPanel2.Controls.Add(this.txtConflictWarning, 1, 0);
		this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 0, 0);
		this.tableLayoutPanel2.Dock = DockStyle.Fill;
		this.tableLayoutPanel2.Location = new Point(3, 3);
		this.tableLayoutPanel2.Name = "tableLayoutPanel2";
		this.tableLayoutPanel2.RowCount = 1;
		this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		this.tableLayoutPanel2.Size = new Size(438, 96);
		this.tableLayoutPanel2.TabIndex = 3;
		// 
		// txtConflictWarning
		// 
		this.txtConflictWarning.BorderStyle = BorderStyle.None;
		this.txtConflictWarning.Dock = DockStyle.Fill;
		this.txtConflictWarning.Location = new Point(71, 3);
		this.txtConflictWarning.Multiline = true;
		this.txtConflictWarning.Name = "txtConflictWarning";
		this.txtConflictWarning.ReadOnly = true;
		this.txtConflictWarning.Size = new Size(364, 90);
		this.txtConflictWarning.TabIndex = 0;
		// 
		// pictureBox1
		// 
		this.pictureBox1.Dock = DockStyle.Fill;
		this.pictureBox1.Image = Properties.Resources.warning_yellow_7231_32wx31h_exp;
		this.pictureBox1.Location = new Point(3, 3);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new Size(62, 90);
		this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
		this.pictureBox1.TabIndex = 1;
		this.pictureBox1.TabStop = false;
		// 
		// chkSuppressMessages
		// 
		this.chkSuppressMessages.AutoSize = true;
		this.chkSuppressMessages.Location = new Point(3, 151);
		this.chkSuppressMessages.Name = "chkSuppressMessages";
		this.chkSuppressMessages.Size = new Size(249, 19);
		this.chkSuppressMessages.TabIndex = 4;
		this.chkSuppressMessages.Text = "Don't show this warning again this session";
		this.chkSuppressMessages.UseVisualStyleBackColor = true;
		// 
		// frmAddressConflictWarning
		// 
		this.AcceptButton = this.cmdClose;
		this.AutoScaleDimensions = new SizeF(7F, 15F);
		this.AutoScaleMode = AutoScaleMode.Font;
		this.CancelButton = this.cmdClose;
		this.ClientSize = new Size(444, 173);
		this.Controls.Add(this.tableLayoutPanel1);
		this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
		this.helpProvider1.SetHelpKeyword(this, "address-conflict-warning");
		this.helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
		this.helpProvider1.SetShowHelp(this, true);
		this.Name = "frmAddressConflictWarning";
		this.ShowIcon = false;
		this.ShowInTaskbar = false;
		this.SizeGripStyle = SizeGripStyle.Hide;
		this.StartPosition = FormStartPosition.CenterParent;
		this.Text = "Address Conflict";
		this.FormClosing += this.frmAddressConflictWarning_FormClosing;
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.tableLayoutPanel2.ResumeLayout(false);
		this.tableLayoutPanel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.ResumeLayout(false);
	}

	#endregion

	private TableLayoutPanel tableLayoutPanel1;
	private Button cmdClose;
	private TableLayoutPanel tableLayoutPanel2;
	private TextBox txtConflictWarning;
	private PictureBox pictureBox1;
	private CheckBox chkSuppressMessages;
	private HelpProvider helpProvider1;
}