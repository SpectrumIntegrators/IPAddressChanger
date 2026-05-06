using System.Text;
using IPAddressChanger.Properties;

namespace IPAddressChanger {
	public partial class frmDebug : Form {
		private delegate void AddMessageCallback(string message);
		// Watches the listbox's native window for vertical scroll messages so we can disable
		// auto-scroll the moment the user grabs the scrollbar (the managed ListBox doesn't
		// expose a scroll event for that).
		private readonly ListBoxScrollWatcher _scrollWatcher = new();
		public frmDebug() {
			InitializeComponent();
		}

		private void frmDebug_FormClosing(object sender, FormClosingEventArgs e) {
			if (e.CloseReason == CloseReason.UserClosing) {
				// if the user clicks the close button, just hide the form
				this.Hide();
				e.Cancel = true;
			}
		}

		internal void AddMessage(string message) {
			if (this.InvokeRequired) {
				AddMessageCallback amc = new(this.AddMessage);
				this.Invoke(amc, new object[] { message });

			} else {
				lsbDebug.Items.Add($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}: {message.Replace("\r\n", " ")}");
				if (Settings.Default.DebugAutoScroll && lsbDebug.Items.Count > 0) {
					ScrollToBottom();
				}
			}
		}

		private void ScrollToBottom() {
			// TopIndex changes generate WM_VSCROLL, which would otherwise trip the user-scroll
			// watcher and immediately disable auto-scroll. Suppress the watcher across the set.
			_scrollWatcher.Suppress = true;
			try {
				lsbDebug.TopIndex = lsbDebug.Items.Count - 1;
			} finally {
				_scrollWatcher.Suppress = false;
			}
		}

		internal void AddMessage(string format, params object[] args) {
			AddMessage(string.Format(format, args));
		}

		private void frmDebug_Load(object sender, EventArgs e) {
			helpProvider1.HelpNamespace = Resources.ReadmeUrl;
			tsbAutoScroll.Checked = Settings.Default.DebugAutoScroll;
			_scrollWatcher.UserScrolled += OnUserScrolledListBox;
			_scrollWatcher.AssignHandle(lsbDebug.Handle);
		}

		protected override void OnHandleDestroyed(EventArgs e) {
			_scrollWatcher.ReleaseHandle();
			base.OnHandleDestroyed(e);
		}

		private void OnUserScrolledListBox(object? sender, EventArgs e) {
			if (!Settings.Default.DebugAutoScroll) return;
			// Marshal back to the UI thread — WM_VSCROLL/WM_MOUSEWHEEL come in synchronously
			// on the UI thread already, but BeginInvoke keeps us safe if that ever changes.
			BeginInvoke(() => tsbAutoScroll.Checked = false);
		}

		private void lsbDebug_SelectedIndexChanged(object sender, EventArgs e) {

		}

		private void tsbClear_Click(object sender, EventArgs e) {
			lsbDebug.Items.Clear();
		}

		private void tsbCopy_Click(object sender, EventArgs e) {
			StringBuilder toClipboard = new();
			foreach (var item in lsbDebug.SelectedItems) {
				toClipboard.AppendLine(item.ToString());
			}
			Clipboard.SetText(toClipboard.ToString());
		}

		private void lsbDebug_DoubleClick(object sender, EventArgs e) {
			Clipboard.SetText(lsbDebug.Text + "\r\n");
		}

		private void frmDebug_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control) {
				tsbCopy.PerformClick();
			} else if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control) {
				for (int i = 0; i < lsbDebug.Items.Count; i++) {
					lsbDebug.SetSelected(i, true);
				}
			}
		}

		private void tsbSave_Click(object sender, EventArgs e) {
			SaveFileDialog sfd = new();
			sfd.Filter = "Log Fils|*.log|All Files|*.*";
			sfd.AddExtension = true;
			sfd.CheckWriteAccess = true;
			sfd.CheckPathExists = true;
			sfd.DefaultExt = ".log";
			sfd.OverwritePrompt = true;
			sfd.FileName = $"IPAddressChanger_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.log";
			sfd.Title = "Save Debug Log";
			sfd.ValidateNames = true;
			if (sfd.ShowDialog() == DialogResult.OK) {
				try {
					StreamWriter sw = new(sfd.FileName, false);
					for (int i = 0; i < lsbDebug.Items.Count; i++) {
						sw.WriteLine(lsbDebug.Items[i].ToString());
					}
					sw.Close();
					sw.Dispose();
				} catch (Exception ex) {
					string errMsg = $"Error saving debug log: {ex.Message}";
					MessageBox.Show(errMsg, "Error Saving Log", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					AddMessage(errMsg);
				}
			}
		}

		// Native-window subclass that taps into the listbox's WndProc to detect vertical scrolls
		// from any source (mouse wheel, scrollbar drag/click, keyboard navigation), all of which
		// produce WM_VSCROLL or WM_MOUSEWHEEL.
		private class ListBoxScrollWatcher : NativeWindow {
			private const int WM_VSCROLL = 0x0115;
			private const int WM_MOUSEWHEEL = 0x020A;
			public event EventHandler? UserScrolled;
			public bool Suppress { get; set; }

			protected override void WndProc(ref Message m) {
				if (!Suppress && (m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL)) {
					UserScrolled?.Invoke(this, EventArgs.Empty);
				}
				base.WndProc(ref m);
			}
		}

		private void tsbAutoScroll_CheckedChanged(object sender, EventArgs e) {
			Settings.Default.DebugAutoScroll = tsbAutoScroll.Checked;
			// In-memory only; persistence happens at app exit (frmMain.SaveShortcuts calls Save).
			if (tsbAutoScroll.Checked) {
				ScrollToBottom();
			}
		}
	}
}
