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
			tableLayoutPanel1 = new TableLayoutPanel();
			label1 = new Label();
			txtName = new TextBox();
			tableLayoutPanel2 = new TableLayoutPanel();
			cmdDelete = new Button();
			cmdCancel = new Button();
			cmdOK = new Button();
			label4 = new Label();
			txtIPAddress = new TextBox();
			label3 = new Label();
			label2 = new Label();
			chkUseDHCP = new CheckBox();
			label5 = new Label();
			txtAdapterName = new TextBox();
			tableLayoutPanel3 = new TableLayoutPanel();
			nudPrefixLength = new NumericUpDown();
			lblIPv4SubnetMask = new Label();
			helpProvider1 = new HelpProvider();
			tableLayoutPanel1.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nudPrefixLength).BeginInit();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.375F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 79.625F));
			tableLayoutPanel1.Controls.Add(label1, 0, 0);
			tableLayoutPanel1.Controls.Add(txtName, 1, 0);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 5);
			tableLayoutPanel1.Controls.Add(label4, 0, 4);
			tableLayoutPanel1.Controls.Add(txtIPAddress, 1, 3);
			tableLayoutPanel1.Controls.Add(label3, 0, 3);
			tableLayoutPanel1.Controls.Add(label2, 0, 2);
			tableLayoutPanel1.Controls.Add(chkUseDHCP, 1, 2);
			tableLayoutPanel1.Controls.Add(label5, 0, 1);
			tableLayoutPanel1.Controls.Add(txtAdapterName, 1, 1);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 4);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Margin = new Padding(12);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 6;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
			tableLayoutPanel1.Size = new Size(800, 353);
			tableLayoutPanel1.TabIndex = 9;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Left;
			label1.AutoSize = true;
			label1.Location = new Point(3, 16);
			label1.Name = "label1";
			label1.Size = new Size(131, 25);
			label1.TabIndex = 9;
			label1.Text = "Shortcut Name";
			// 
			// txtName
			// 
			txtName.Anchor = AnchorStyles.Left;
			helpProvider1.SetHelpKeyword(txtName, "shortcut-name");
			helpProvider1.SetHelpNavigator(txtName, HelpNavigator.Topic);
			txtName.Location = new Point(166, 13);
			txtName.Name = "txtName";
			helpProvider1.SetShowHelp(txtName, true);
			txtName.Size = new Size(628, 31);
			txtName.TabIndex = 0;
			txtName.TextChanged += txtName_TextChanged;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 3;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
			tableLayoutPanel2.Controls.Add(cmdDelete, 0, 0);
			tableLayoutPanel2.Controls.Add(cmdCancel, 2, 0);
			tableLayoutPanel2.Controls.Add(cmdOK, 1, 0);
			tableLayoutPanel2.Dock = DockStyle.Right;
			tableLayoutPanel2.Location = new Point(341, 290);
			tableLayoutPanel2.Margin = new Padding(0);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 1;
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.Size = new Size(459, 63);
			tableLayoutPanel2.TabIndex = 7;
			// 
			// cmdDelete
			// 
			cmdDelete.Anchor = AnchorStyles.Right;
			helpProvider1.SetHelpKeyword(cmdDelete, "delete-button");
			helpProvider1.SetHelpNavigator(cmdDelete, HelpNavigator.Topic);
			cmdDelete.Location = new Point(38, 9);
			cmdDelete.Name = "cmdDelete";
			helpProvider1.SetShowHelp(cmdDelete, true);
			cmdDelete.Size = new Size(111, 45);
			cmdDelete.TabIndex = 6;
			cmdDelete.Text = "&Delete";
			cmdDelete.UseVisualStyleBackColor = true;
			cmdDelete.Click += cmdDelete_Click;
			// 
			// cmdCancel
			// 
			cmdCancel.Anchor = AnchorStyles.Right;
			cmdCancel.DialogResult = DialogResult.Cancel;
			helpProvider1.SetHelpKeyword(cmdCancel, "newedit-shortcut-window");
			helpProvider1.SetHelpNavigator(cmdCancel, HelpNavigator.Topic);
			cmdCancel.Location = new Point(345, 9);
			cmdCancel.Name = "cmdCancel";
			helpProvider1.SetShowHelp(cmdCancel, true);
			cmdCancel.Size = new Size(111, 45);
			cmdCancel.TabIndex = 4;
			cmdCancel.Text = "&Cancel";
			cmdCancel.UseVisualStyleBackColor = true;
			// 
			// cmdOK
			// 
			cmdOK.Anchor = AnchorStyles.Right;
			helpProvider1.SetHelpKeyword(cmdOK, "newedit-shortcut-window");
			helpProvider1.SetHelpNavigator(cmdOK, HelpNavigator.Topic);
			cmdOK.Location = new Point(190, 9);
			cmdOK.Name = "cmdOK";
			helpProvider1.SetShowHelp(cmdOK, true);
			cmdOK.Size = new Size(111, 45);
			cmdOK.TabIndex = 5;
			cmdOK.Text = "&OK";
			cmdOK.UseVisualStyleBackColor = true;
			cmdOK.Click += cmdOK_Click;
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Left;
			label4.AutoSize = true;
			label4.Location = new Point(3, 248);
			label4.Name = "label4";
			label4.Size = new Size(114, 25);
			label4.TabIndex = 9;
			label4.Text = "Prefix Length";
			// 
			// txtIPAddress
			// 
			txtIPAddress.Anchor = AnchorStyles.Left;
			helpProvider1.SetHelpKeyword(txtIPAddress, "ip-address");
			helpProvider1.SetHelpNavigator(txtIPAddress, HelpNavigator.Topic);
			txtIPAddress.Location = new Point(166, 187);
			txtIPAddress.Name = "txtIPAddress";
			helpProvider1.SetShowHelp(txtIPAddress, true);
			txtIPAddress.Size = new Size(628, 31);
			txtIPAddress.TabIndex = 2;
			txtIPAddress.TextChanged += txtIPAddress_TextChanged;
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Left;
			label3.AutoSize = true;
			label3.Location = new Point(3, 190);
			label3.Name = "label3";
			label3.Size = new Size(97, 25);
			label3.TabIndex = 9;
			label3.Text = "IP Address";
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Left;
			label2.AutoSize = true;
			label2.Location = new Point(3, 132);
			label2.Name = "label2";
			label2.Size = new Size(127, 25);
			label2.TabIndex = 9;
			label2.Text = "DHCP Enabled";
			// 
			// chkUseDHCP
			// 
			chkUseDHCP.Anchor = AnchorStyles.Left;
			chkUseDHCP.AutoSize = true;
			helpProvider1.SetHelpKeyword(chkUseDHCP, "dhcp-enabled");
			helpProvider1.SetHelpNavigator(chkUseDHCP, HelpNavigator.Topic);
			chkUseDHCP.Location = new Point(166, 134);
			chkUseDHCP.Name = "chkUseDHCP";
			helpProvider1.SetShowHelp(chkUseDHCP, true);
			chkUseDHCP.Size = new Size(22, 21);
			chkUseDHCP.TabIndex = 1;
			chkUseDHCP.UseVisualStyleBackColor = true;
			chkUseDHCP.CheckedChanged += chkUseDHCP_CheckedChanged;
			// 
			// label5
			// 
			label5.Anchor = AnchorStyles.Left;
			label5.AutoSize = true;
			label5.Location = new Point(3, 74);
			label5.Name = "label5";
			label5.Size = new Size(76, 25);
			label5.TabIndex = 9;
			label5.Text = "Adapter";
			// 
			// txtAdapterName
			// 
			txtAdapterName.Anchor = AnchorStyles.Left;
			helpProvider1.SetHelpKeyword(txtAdapterName, "adapter");
			helpProvider1.SetHelpNavigator(txtAdapterName, HelpNavigator.Topic);
			txtAdapterName.Location = new Point(166, 71);
			txtAdapterName.Name = "txtAdapterName";
			txtAdapterName.ReadOnly = true;
			helpProvider1.SetShowHelp(txtAdapterName, true);
			txtAdapterName.Size = new Size(628, 31);
			txtAdapterName.TabIndex = 7;
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.ColumnCount = 2;
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.5974655F));
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.4025345F));
			tableLayoutPanel3.Controls.Add(nudPrefixLength, 0, 0);
			tableLayoutPanel3.Controls.Add(lblIPv4SubnetMask, 1, 0);
			tableLayoutPanel3.Dock = DockStyle.Fill;
			tableLayoutPanel3.Location = new Point(163, 232);
			tableLayoutPanel3.Margin = new Padding(0);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 1;
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel3.Size = new Size(637, 58);
			tableLayoutPanel3.TabIndex = 10;
			// 
			// nudPrefixLength
			// 
			nudPrefixLength.Dock = DockStyle.Fill;
			helpProvider1.SetHelpKeyword(nudPrefixLength, "prefix-length");
			helpProvider1.SetHelpNavigator(nudPrefixLength, HelpNavigator.Topic);
			nudPrefixLength.Location = new Point(3, 3);
			nudPrefixLength.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
			nudPrefixLength.Name = "nudPrefixLength";
			helpProvider1.SetShowHelp(nudPrefixLength, true);
			nudPrefixLength.Size = new Size(208, 31);
			nudPrefixLength.TabIndex = 3;
			nudPrefixLength.ValueChanged += nudPrefixLength_ValueChanged;
			// 
			// lblIPv4SubnetMask
			// 
			lblIPv4SubnetMask.AutoSize = true;
			lblIPv4SubnetMask.Dock = DockStyle.Fill;
			lblIPv4SubnetMask.Location = new Point(217, 0);
			lblIPv4SubnetMask.Name = "lblIPv4SubnetMask";
			lblIPv4SubnetMask.Size = new Size(417, 58);
			lblIPv4SubnetMask.TabIndex = 8;
			lblIPv4SubnetMask.Text = "255.255.255.255";
			lblIPv4SubnetMask.TextAlign = ContentAlignment.MiddleLeft;
			lblIPv4SubnetMask.Visible = false;
			// 
			// helpProvider1
			// 
			helpProvider1.HelpNamespace = "https://spectrumintegrators.github.io/IPAddressChanger/";
			// 
			// frmEditShortcut
			// 
			AcceptButton = cmdOK;
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = cmdCancel;
			ClientSize = new Size(800, 353);
			ControlBox = false;
			Controls.Add(tableLayoutPanel1);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			helpProvider1.SetHelpKeyword(this, "newedit-shortcut-window");
			helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
			Name = "frmEditShortcut";
			helpProvider1.SetShowHelp(this, true);
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Edit Shortcut";
			Load += frmEditShortcut_Load;
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)nudPrefixLength).EndInit();
			ResumeLayout(false);
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