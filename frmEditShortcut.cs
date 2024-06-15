using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Numerics;
using System.Net;

namespace IPAddressChanger {
	public partial class frmEditShortcut : Form {
		private Regex ipv4Pattern = new Regex(@"^(?:\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})$");
		internal string DeviceID { get; set; } = "";
		internal int? ShortcutIndex { get; set; } = null;
		internal bool nameHasChanged = false;
		internal string adapterName = "";

		public frmEditShortcut() {
			InitializeComponent();
		}

		private void CalculateAndDisplaySubnetMask() {
			if (ipv4Pattern.Match(txtIPAddress.Text).Success && nudPrefixLength.Value <= 32) {
				lblIPv4SubnetMask.Visible = true;
				uint cidr = (uint)nudPrefixLength.Value;
				uint mask = (cidr == 0) ? 0 : uint.MaxValue << (int)(32 - cidr);
				byte[] bytes = BitConverter.GetBytes(mask).Reverse().ToArray();
				lblIPv4SubnetMask.Text = (new IPAddress(bytes)).ToString();
			} else {
				lblIPv4SubnetMask.Visible = false;
			}
		}
		private void ShowValidationError(string msg, Control ctl) {
			MessageBox.Show(msg, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			ctl.Select();
		}

		private bool ValidateFields() {
			if (txtName.Text.Trim().Length == 0) {
				ShowValidationError("Name is required", txtName);
				return false;
			}
			if (!chkUseDHCP.Checked) {
				if (txtIPAddress.Text.Trim().Length == 0) {
					ShowValidationError("IP address is required", txtIPAddress);
					return false;
				}
			}
			return true;
		}
		private void chkUseDHCP_CheckedChanged(object sender, EventArgs e) {
			txtIPAddress.Enabled = !chkUseDHCP.Checked;
			nudPrefixLength.Enabled = !chkUseDHCP.Checked;
		}

		private void frmEditShortcut_Load(object sender, EventArgs e) {
			txtName.Select();
		}

		private void cmdOK_Click(object sender, EventArgs e) {
			if (ValidateFields()) {
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void cmdDelete_Click(object sender, EventArgs e) {
			if (MessageBox.Show("Are you sure you want to delete this shortcut?", "Delete Shortcut?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
				this.DialogResult = DialogResult.Yes;
				this.Close();
			}
		}

		private void txtIPAddress_TextChanged(object sender, EventArgs e) {
			CalculateAndDisplaySubnetMask();
			if (!nameHasChanged) {
				txtName.Text = $"{adapterName} - {txtIPAddress.Text}/{nudPrefixLength.Value}";
				nameHasChanged = false;
			}
		}

		private void nudPrefixLength_ValueChanged(object sender, EventArgs e) {
			CalculateAndDisplaySubnetMask();
			if (!nameHasChanged) {
				txtName.Text = $"{adapterName} - {txtIPAddress.Text}/{nudPrefixLength.Value}";
				nameHasChanged = false;
			}
		}

		private void txtName_TextChanged(object sender, EventArgs e) {
			nameHasChanged = true;
		}
	}
}
