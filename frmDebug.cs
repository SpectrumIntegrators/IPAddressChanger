using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPAddressChanger {
	public partial class frmDebug : Form {
		private delegate void AddMessageCallback(string message);
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
			}
		}

		internal void AddMessage(string format, params object[] args) {
			AddMessage(string.Format(format, args));
		}

		private void frmDebug_Load(object sender, EventArgs e) {

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
			sfd.Title = "Save Debug Log";
			sfd.ValidateNames = true;
			if (sfd.ShowDialog() == DialogResult.OK) {
				try {
					StreamWriter sw = new(sfd.FileName, false);
					for (int i = 0; i<lsbDebug.Items.Count; i++) {
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
	}
}
