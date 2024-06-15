namespace IPAddressChanger {
	partial class frmEditShortcut {
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
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.label1 = new Label();
			this.txtName = new TextBox();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.cmdDelete = new Button();
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.label4 = new Label();
			this.txtIPAddress = new TextBox();
			this.label3 = new Label();
			this.label2 = new Label();
			this.chkUseDHCP = new CheckBox();
			this.label5 = new Label();
			this.txtAdapterName = new TextBox();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.nudPrefixLength = new NumericUpDown();
			this.lblIPv4SubnetMask = new Label();
			this.helpProvider1 = new HelpProvider();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.nudPrefixLength).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.375F));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.625F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.txtIPAddress, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.chkUseDHCP, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtAdapterName, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 4);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Margin = new Padding(2);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			this.tableLayoutPanel1.Size = new Size(560, 161);
			this.tableLayoutPanel1.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.Anchor = AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(2, 5);
			this.label1.Margin = new Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(87, 15);
			this.label1.TabIndex = 9;
			this.label1.Text = "Shortcut Name";
			// 
			// txtName
			// 
			this.txtName.Anchor = AnchorStyles.Left;
			this.helpProvider1.SetHelpKeyword(this.txtName, "shortcut-name");
			this.helpProvider1.SetHelpNavigator(this.txtName, HelpNavigator.Topic);
			this.txtName.Location = new Point(116, 2);
			this.txtName.Margin = new Padding(2);
			this.txtName.Name = "txtName";
			this.helpProvider1.SetShowHelp(this.txtName, true);
			this.txtName.Size = new Size(441, 23);
			this.txtName.TabIndex = 0;
			this.txtName.TextChanged += this.txtName_TextChanged;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			this.tableLayoutPanel2.Controls.Add(this.cmdDelete, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.cmdCancel, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.cmdOK, 1, 0);
			this.tableLayoutPanel2.Dock = DockStyle.Right;
			this.tableLayoutPanel2.Location = new Point(239, 130);
			this.tableLayoutPanel2.Margin = new Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new Size(321, 31);
			this.tableLayoutPanel2.TabIndex = 7;
			// 
			// cmdDelete
			// 
			this.cmdDelete.Anchor = AnchorStyles.Right;
			this.helpProvider1.SetHelpKeyword(this.cmdDelete, "delete-button");
			this.helpProvider1.SetHelpNavigator(this.cmdDelete, HelpNavigator.Topic);
			this.cmdDelete.Location = new Point(27, 2);
			this.cmdDelete.Margin = new Padding(2);
			this.cmdDelete.Name = "cmdDelete";
			this.helpProvider1.SetShowHelp(this.cmdDelete, true);
			this.cmdDelete.Size = new Size(78, 27);
			this.cmdDelete.TabIndex = 6;
			this.cmdDelete.Text = "&Delete";
			this.cmdDelete.UseVisualStyleBackColor = true;
			this.cmdDelete.Click += this.cmdDelete_Click;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = AnchorStyles.Right;
			this.cmdCancel.DialogResult = DialogResult.Cancel;
			this.helpProvider1.SetHelpKeyword(this.cmdCancel, "newedit-shortcut-window");
			this.helpProvider1.SetHelpNavigator(this.cmdCancel, HelpNavigator.Topic);
			this.cmdCancel.Location = new Point(241, 2);
			this.cmdCancel.Margin = new Padding(2);
			this.cmdCancel.Name = "cmdCancel";
			this.helpProvider1.SetShowHelp(this.cmdCancel, true);
			this.cmdCancel.Size = new Size(78, 27);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = AnchorStyles.Right;
			this.helpProvider1.SetHelpKeyword(this.cmdOK, "newedit-shortcut-window");
			this.helpProvider1.SetHelpNavigator(this.cmdOK, HelpNavigator.Topic);
			this.cmdOK.Location = new Point(134, 2);
			this.cmdOK.Margin = new Padding(2);
			this.cmdOK.Name = "cmdOK";
			this.helpProvider1.SetShowHelp(this.cmdOK, true);
			this.cmdOK.Size = new Size(78, 27);
			this.cmdOK.TabIndex = 5;
			this.cmdOK.Text = "&OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += this.cmdOK_Click;
			// 
			// label4
			// 
			this.label4.Anchor = AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(2, 109);
			this.label4.Margin = new Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new Size(77, 15);
			this.label4.TabIndex = 9;
			this.label4.Text = "Prefix Length";
			// 
			// txtIPAddress
			// 
			this.txtIPAddress.Anchor = AnchorStyles.Left;
			this.helpProvider1.SetHelpKeyword(this.txtIPAddress, "ip-address");
			this.helpProvider1.SetHelpNavigator(this.txtIPAddress, HelpNavigator.Topic);
			this.txtIPAddress.Location = new Point(116, 80);
			this.txtIPAddress.Margin = new Padding(2);
			this.txtIPAddress.Name = "txtIPAddress";
			this.helpProvider1.SetShowHelp(this.txtIPAddress, true);
			this.txtIPAddress.Size = new Size(441, 23);
			this.txtIPAddress.TabIndex = 2;
			this.txtIPAddress.TextChanged += this.txtIPAddress_TextChanged;
			// 
			// label3
			// 
			this.label3.Anchor = AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(2, 83);
			this.label3.Margin = new Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new Size(62, 15);
			this.label3.TabIndex = 9;
			this.label3.Text = "IP Address";
			// 
			// label2
			// 
			this.label2.Anchor = AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(2, 57);
			this.label2.Margin = new Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(84, 15);
			this.label2.TabIndex = 9;
			this.label2.Text = "DHCP Enabled";
			// 
			// chkUseDHCP
			// 
			this.chkUseDHCP.Anchor = AnchorStyles.Left;
			this.chkUseDHCP.AutoSize = true;
			this.helpProvider1.SetHelpKeyword(this.chkUseDHCP, "dhcp-enabled");
			this.helpProvider1.SetHelpNavigator(this.chkUseDHCP, HelpNavigator.Topic);
			this.chkUseDHCP.Location = new Point(116, 58);
			this.chkUseDHCP.Margin = new Padding(2);
			this.chkUseDHCP.Name = "chkUseDHCP";
			this.helpProvider1.SetShowHelp(this.chkUseDHCP, true);
			this.chkUseDHCP.Size = new Size(15, 14);
			this.chkUseDHCP.TabIndex = 1;
			this.chkUseDHCP.UseVisualStyleBackColor = true;
			this.chkUseDHCP.CheckedChanged += this.chkUseDHCP_CheckedChanged;
			// 
			// label5
			// 
			this.label5.Anchor = AnchorStyles.Left;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(2, 31);
			this.label5.Margin = new Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new Size(49, 15);
			this.label5.TabIndex = 9;
			this.label5.Text = "Adapter";
			// 
			// txtAdapterName
			// 
			this.txtAdapterName.Anchor = AnchorStyles.Left;
			this.helpProvider1.SetHelpKeyword(this.txtAdapterName, "adapter");
			this.helpProvider1.SetHelpNavigator(this.txtAdapterName, HelpNavigator.Topic);
			this.txtAdapterName.Location = new Point(116, 28);
			this.txtAdapterName.Margin = new Padding(2);
			this.txtAdapterName.Name = "txtAdapterName";
			this.txtAdapterName.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.txtAdapterName, true);
			this.txtAdapterName.Size = new Size(441, 23);
			this.txtAdapterName.TabIndex = 7;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.5974655F));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.4025345F));
			this.tableLayoutPanel3.Controls.Add(this.nudPrefixLength, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.lblIPv4SubnetMask, 1, 0);
			this.tableLayoutPanel3.Dock = DockStyle.Fill;
			this.tableLayoutPanel3.Location = new Point(114, 104);
			this.tableLayoutPanel3.Margin = new Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new Size(446, 26);
			this.tableLayoutPanel3.TabIndex = 10;
			// 
			// nudPrefixLength
			// 
			this.nudPrefixLength.Dock = DockStyle.Fill;
			this.helpProvider1.SetHelpKeyword(this.nudPrefixLength, "prefix-length");
			this.helpProvider1.SetHelpNavigator(this.nudPrefixLength, HelpNavigator.Topic);
			this.nudPrefixLength.Location = new Point(2, 2);
			this.nudPrefixLength.Margin = new Padding(2);
			this.nudPrefixLength.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
			this.nudPrefixLength.Name = "nudPrefixLength";
			this.helpProvider1.SetShowHelp(this.nudPrefixLength, true);
			this.nudPrefixLength.Size = new Size(145, 23);
			this.nudPrefixLength.TabIndex = 3;
			this.nudPrefixLength.ValueChanged += this.nudPrefixLength_ValueChanged;
			// 
			// lblIPv4SubnetMask
			// 
			this.lblIPv4SubnetMask.AutoSize = true;
			this.lblIPv4SubnetMask.Dock = DockStyle.Fill;
			this.lblIPv4SubnetMask.Location = new Point(151, 0);
			this.lblIPv4SubnetMask.Margin = new Padding(2, 0, 2, 0);
			this.lblIPv4SubnetMask.Name = "lblIPv4SubnetMask";
			this.lblIPv4SubnetMask.Size = new Size(293, 26);
			this.lblIPv4SubnetMask.TabIndex = 8;
			this.lblIPv4SubnetMask.Text = "255.255.255.255";
			this.lblIPv4SubnetMask.TextAlign = ContentAlignment.MiddleLeft;
			this.lblIPv4SubnetMask.Visible = false;
			// 
			// helpProvider1
			// 
			this.helpProvider1.HelpNamespace = "https://spectrumintegrators.github.io/IPAddressChanger/";
			// 
			// frmEditShortcut
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new Size(560, 161);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.helpProvider1.SetHelpKeyword(this, "newedit-shortcut-window");
			this.helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
			this.Margin = new Padding(2);
			this.Name = "frmEditShortcut";
			this.helpProvider1.SetShowHelp(this, true);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Edit Shortcut";
			this.Load += this.frmEditShortcut_Load;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.nudPrefixLength).EndInit();
			this.ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private TableLayoutPanel tableLayoutPanel2;
		private Button cmdCancel;
		private Button cmdOK;
		internal TextBox txtName;
		internal CheckBox chkUseDHCP;
		internal TextBox txtIPAddress;
		internal NumericUpDown nudPrefixLength;
		internal Button cmdDelete;
		private Label label5;
		internal TextBox txtAdapterName;
		private TableLayoutPanel tableLayoutPanel3;
		private Label lblIPv4SubnetMask;
		private HelpProvider helpProvider1;
	}
}