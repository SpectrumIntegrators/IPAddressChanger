namespace IPAddressChanger {
	partial class frmSettings {
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
			tableLayoutPanel2 = new TableLayoutPanel();
			cmdCancel = new Button();
			cmdOK = new Button();
			tableLayoutPanel3 = new TableLayoutPanel();
			lblHideWhenMinimized = new Label();
			chkHideWhenMinimized = new CheckBox();
			label2 = new Label();
			cboShortcutDoubleClick = new ComboBox();
			lblStartMinimized = new Label();
			chkStartMinimized = new CheckBox();
			tableLayoutPanel1.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 2;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
			tableLayoutPanel1.Size = new Size(800, 450);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 3;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
			tableLayoutPanel2.Controls.Add(cmdCancel, 2, 0);
			tableLayoutPanel2.Controls.Add(cmdOK, 1, 0);
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new Point(3, 393);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 1;
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.Size = new Size(794, 54);
			tableLayoutPanel2.TabIndex = 0;
			// 
			// cmdCancel
			// 
			cmdCancel.Anchor = AnchorStyles.Top;
			cmdCancel.Location = new Point(638, 3);
			cmdCancel.Name = "cmdCancel";
			cmdCancel.Size = new Size(112, 40);
			cmdCancel.TabIndex = 0;
			cmdCancel.Text = "&Cancel";
			cmdCancel.UseVisualStyleBackColor = true;
			cmdCancel.Click += cmdCancel_Click;
			// 
			// cmdOK
			// 
			cmdOK.Anchor = AnchorStyles.Top;
			cmdOK.Enabled = false;
			cmdOK.Location = new Point(438, 3);
			cmdOK.Name = "cmdOK";
			cmdOK.Size = new Size(112, 40);
			cmdOK.TabIndex = 3;
			cmdOK.Text = "&OK";
			cmdOK.UseVisualStyleBackColor = true;
			cmdOK.Click += cmdOK_Click;
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.ColumnCount = 2;
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.66499F));
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.3350143F));
			tableLayoutPanel3.Controls.Add(lblHideWhenMinimized, 0, 0);
			tableLayoutPanel3.Controls.Add(chkHideWhenMinimized, 1, 0);
			tableLayoutPanel3.Controls.Add(label2, 0, 2);
			tableLayoutPanel3.Controls.Add(cboShortcutDoubleClick, 1, 2);
			tableLayoutPanel3.Controls.Add(lblStartMinimized, 0, 1);
			tableLayoutPanel3.Controls.Add(chkStartMinimized, 1, 1);
			tableLayoutPanel3.Dock = DockStyle.Fill;
			tableLayoutPanel3.Location = new Point(3, 3);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 4;
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel3.Size = new Size(794, 384);
			tableLayoutPanel3.TabIndex = 1;
			// 
			// lblHideWhenMinimized
			// 
			lblHideWhenMinimized.AutoSize = true;
			lblHideWhenMinimized.Dock = DockStyle.Fill;
			lblHideWhenMinimized.Location = new Point(3, 0);
			lblHideWhenMinimized.Name = "lblHideWhenMinimized";
			lblHideWhenMinimized.Size = new Size(301, 40);
			lblHideWhenMinimized.TabIndex = 0;
			lblHideWhenMinimized.Text = "Hide when minimized";
			lblHideWhenMinimized.TextAlign = ContentAlignment.MiddleLeft;
			lblHideWhenMinimized.Click += lblHideWhenMinimized_Click;
			// 
			// chkHideWhenMinimized
			// 
			chkHideWhenMinimized.Anchor = AnchorStyles.Left;
			chkHideWhenMinimized.AutoSize = true;
			chkHideWhenMinimized.Location = new Point(310, 9);
			chkHideWhenMinimized.Name = "chkHideWhenMinimized";
			chkHideWhenMinimized.Size = new Size(22, 21);
			chkHideWhenMinimized.TabIndex = 1;
			chkHideWhenMinimized.UseVisualStyleBackColor = true;
			chkHideWhenMinimized.CheckedChanged += chkHideWhenMinimized_CheckedChanged;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Dock = DockStyle.Fill;
			label2.Location = new Point(3, 80);
			label2.Name = "label2";
			label2.Size = new Size(301, 40);
			label2.TabIndex = 2;
			label2.Text = "Double clicking a shortcut will";
			label2.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// cboShortcutDoubleClick
			// 
			cboShortcutDoubleClick.Anchor = AnchorStyles.Left;
			cboShortcutDoubleClick.DropDownStyle = ComboBoxStyle.DropDownList;
			cboShortcutDoubleClick.FormattingEnabled = true;
			cboShortcutDoubleClick.Items.AddRange(new object[] { "Edit the shortcut", "Recall the shortcut" });
			cboShortcutDoubleClick.Location = new Point(310, 83);
			cboShortcutDoubleClick.Name = "cboShortcutDoubleClick";
			cboShortcutDoubleClick.Size = new Size(441, 33);
			cboShortcutDoubleClick.TabIndex = 2;
			cboShortcutDoubleClick.SelectedIndexChanged += cboShortcutDoubleClick_SelectedIndexChanged;
			// 
			// lblStartMinimized
			// 
			lblStartMinimized.AutoSize = true;
			lblStartMinimized.Dock = DockStyle.Fill;
			lblStartMinimized.Location = new Point(3, 40);
			lblStartMinimized.Name = "lblStartMinimized";
			lblStartMinimized.Size = new Size(301, 40);
			lblStartMinimized.TabIndex = 3;
			lblStartMinimized.Text = "Start minimized";
			lblStartMinimized.TextAlign = ContentAlignment.MiddleLeft;
			lblStartMinimized.Click += lblStartMinimized_Click;
			// 
			// chkStartMinimized
			// 
			chkStartMinimized.Anchor = AnchorStyles.Left;
			chkStartMinimized.AutoSize = true;
			chkStartMinimized.Location = new Point(310, 49);
			chkStartMinimized.Name = "chkStartMinimized";
			chkStartMinimized.Size = new Size(22, 21);
			chkStartMinimized.TabIndex = 4;
			chkStartMinimized.UseVisualStyleBackColor = true;
			chkStartMinimized.CheckedChanged += chkStartMinimized_CheckedChanged;
			// 
			// frmSettings
			// 
			AcceptButton = cmdOK;
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = cmdCancel;
			ClientSize = new Size(800, 450);
			Controls.Add(tableLayoutPanel1);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "frmSettings";
			ShowIcon = false;
			ShowInTaskbar = false;
			Text = "Settings";
			FormClosing += frmSettings_FormClosing;
			FormClosed += frmSettings_FormClosed;
			Load += frmSettings_Load;
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private TableLayoutPanel tableLayoutPanel2;
		private Button cmdCancel;
		private Button cmdOK;
		private TableLayoutPanel tableLayoutPanel3;
		private Label lblHideWhenMinimized;
		private CheckBox chkHideWhenMinimized;
		private Label label2;
		private ComboBox cboShortcutDoubleClick;
		private Label lblStartMinimized;
		private CheckBox chkStartMinimized;
	}
}