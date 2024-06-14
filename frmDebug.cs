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
			txtDebugLog.Text += $"{DateTime.Now.ToString("%s.%fff")}: {message}\r\n";
		}

		internal void AddMessage(string format, params object[] args) {
			AddMessage(string.Format(format, args));
		}
	}
}
