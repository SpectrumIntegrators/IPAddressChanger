namespace IPAddressChanger {
	partial class frmDebug {
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
			txtDebugLog = new TextBox();
			SuspendLayout();
			// 
			// txtDebugLog
			// 
			txtDebugLog.Dock = DockStyle.Fill;
			txtDebugLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
			txtDebugLog.Location = new Point(0, 0);
			txtDebugLog.Multiline = true;
			txtDebugLog.Name = "txtDebugLog";
			txtDebugLog.ReadOnly = true;
			txtDebugLog.ScrollBars = ScrollBars.Both;
			txtDebugLog.Size = new Size(1443, 280);
			txtDebugLog.TabIndex = 0;
			txtDebugLog.WordWrap = false;
			// 
			// frmDebug
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1443, 280);
			Controls.Add(txtDebugLog);
			Name = "frmDebug";
			Text = "Debug Messages";
			FormClosing += frmDebug_FormClosing;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox txtDebugLog;
	}
}