using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPAddressChanger;

public partial class frmAdapterBusy : Form {
	private AdapterInfo _adapterInfo;
	private frmMain _frmMain;
	//public frmAdapterBusy() {
	//	InitializeComponent();
	//}

	public frmAdapterBusy(string reason, AdapterInfo adapterInfo, frmMain frmMain) {
		InitializeComponent();
		_adapterInfo = adapterInfo;
		lblBusyReason.Text = reason;
		this.Text = $"{adapterInfo.Name} Busy...";
		this._frmMain = frmMain;
	}

	private void frmAdapterBusy_FormClosing(object sender, FormClosingEventArgs e) {
		_frmMain.AdapterDialogClosing(_adapterInfo);
	}

	private void cmdClose_Click(object sender, EventArgs e) {
		Close();
	}
}
