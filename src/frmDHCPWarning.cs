using IPAddressChanger.Properties;

namespace IPAddressChanger;
public partial class frmDHCPWarning : Form {
	public frmDHCPWarning() {
		InitializeComponent();
		helpProvider1.HelpNamespace = Resources.ReadmeUrl;
	}

	private void chkSuppressMessages_CheckedChanged(object sender, EventArgs e) {
		Settings.Default.SuppressDHCPWarning = chkSuppressMessages.Checked;
		Settings.Default.Save();
	}

	private void cmdClose_Click(object sender, EventArgs e) {
		Close();
	}
}
