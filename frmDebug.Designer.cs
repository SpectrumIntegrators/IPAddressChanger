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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDebug));
			this.lsbDebug = new ListBox();
			this.toolStrip1 = new ToolStrip();
			this.tsbClear = new ToolStripButton();
			this.tsbCopy = new ToolStripButton();
			this.tsbSave = new ToolStripButton();
			this.helpProvider1 = new HelpProvider();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lsbDebug
			// 
			this.lsbDebug.Dock = DockStyle.Fill;
			this.lsbDebug.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lsbDebug.FormattingEnabled = true;
			this.helpProvider1.SetHelpKeyword(this.lsbDebug, "debug-messages-window");
			this.helpProvider1.SetHelpNavigator(this.lsbDebug, HelpNavigator.Topic);
			this.lsbDebug.HorizontalScrollbar = true;
			this.lsbDebug.IntegralHeight = false;
			this.lsbDebug.ItemHeight = 14;
			this.lsbDebug.Location = new Point(0, 25);
			this.lsbDebug.Name = "lsbDebug";
			this.lsbDebug.SelectionMode = SelectionMode.MultiExtended;
			this.helpProvider1.SetShowHelp(this.lsbDebug, true);
			this.lsbDebug.Size = new Size(1010, 143);
			this.lsbDebug.TabIndex = 0;
			this.lsbDebug.SelectedIndexChanged += this.lsbDebug_SelectedIndexChanged;
			this.lsbDebug.DoubleClick += this.lsbDebug_DoubleClick;
			// 
			// toolStrip1
			// 
			this.helpProvider1.SetHelpKeyword(this.toolStrip1, "debug-messages-window");
			this.helpProvider1.SetHelpNavigator(this.toolStrip1, HelpNavigator.Topic);
			this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.tsbClear, this.tsbCopy, this.tsbSave });
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.helpProvider1.SetShowHelp(this.toolStrip1, true);
			this.toolStrip1.Size = new Size(1010, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbClear
			// 
			this.tsbClear.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbClear.Image = (Image)resources.GetObject("tsbClear.Image");
			this.tsbClear.ImageTransparentColor = Color.Magenta;
			this.tsbClear.Name = "tsbClear";
			this.tsbClear.Size = new Size(23, 22);
			this.tsbClear.Text = "Clear";
			this.tsbClear.ToolTipText = "Clear the debug log";
			this.tsbClear.Click += this.tsbClear_Click;
			// 
			// tsbCopy
			// 
			this.tsbCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbCopy.Image = (Image)resources.GetObject("tsbCopy.Image");
			this.tsbCopy.ImageTransparentColor = Color.Magenta;
			this.tsbCopy.Name = "tsbCopy";
			this.tsbCopy.Size = new Size(23, 22);
			this.tsbCopy.Text = "Copy Selected";
			this.tsbCopy.ToolTipText = "Copy seleted items to the clipboard";
			this.tsbCopy.Click += this.tsbCopy_Click;
			// 
			// tsbSave
			// 
			this.tsbSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSave.Image = (Image)resources.GetObject("tsbSave.Image");
			this.tsbSave.ImageTransparentColor = Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new Size(23, 22);
			this.tsbSave.Text = "Save";
			this.tsbSave.ToolTipText = "Save the debug log to a file";
			this.tsbSave.Click += this.tsbSave_Click;
			// 
			// helpProvider1
			// 
			this.helpProvider1.HelpNamespace = "http://127.0.0.1:8000/Help.html";
			// 
			// frmDebug
			// 
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(1010, 168);
			this.Controls.Add(this.lsbDebug);
			this.Controls.Add(this.toolStrip1);
			this.helpProvider1.SetHelpKeyword(this, "debug-messages-window");
			this.helpProvider1.SetHelpNavigator(this, HelpNavigator.Topic);
			this.KeyPreview = true;
			this.Margin = new Padding(2);
			this.Name = "frmDebug";
			this.helpProvider1.SetShowHelp(this, true);
			this.Text = "Debug Messages";
			this.FormClosing += this.frmDebug_FormClosing;
			this.Load += this.frmDebug_Load;
			this.KeyDown += this.frmDebug_KeyDown;
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private ListBox lsbDebug;
		private ToolStrip toolStrip1;
		private ToolStripButton tsbClear;
		private ToolStripButton tsbCopy;
		private ToolStripButton tsbSave;
		private HelpProvider helpProvider1;
	}
}