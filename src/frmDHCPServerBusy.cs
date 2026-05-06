using IPAddressChanger.Properties;

namespace IPAddressChanger;

/// <summary>
/// Modeless busy dialog used by the DHCP server form during multi-step operations
/// (start sequence, future pre-flight server discovery probe). Owns its own
/// CancellationTokenSource and exposes the token to the caller. The Cancel button,
/// Escape key, and the window's [X] all request cancellation; the caller is
/// responsible for closing the dialog when its work is done.
/// </summary>
public partial class frmDHCPServerBusy : Form {
	private readonly CancellationTokenSource _cts = new();

	public frmDHCPServerBusy() {
		InitializeComponent();
		helpProvider1.HelpNamespace = Resources.ReadmeUrl;
	}

	public frmDHCPServerBusy(string initialStatus) : this() {
		lblStatus.Text = initialStatus;
	}

	public CancellationToken CancellationToken => _cts.Token;

	public bool IsCancellationRequested => _cts.IsCancellationRequested;

	/// <summary>
	/// Updates the status label. Safe to call from any thread.
	/// </summary>
	public void SetStatus(string status) {
		if (IsDisposed) return;
		if (InvokeRequired) {
			BeginInvoke(() => SetStatus(status));
			return;
		}
		lblStatus.Text = status;
	}

	private void cmdCancel_Click(object sender, EventArgs e) {
		RequestCancel();
	}

	private void frmDHCPServerBusy_FormClosing(object sender, FormClosingEventArgs e) {
		// If the user closes via [X] before the operation finishes, treat it as a cancel.
		// We don't block the close — the caller's awaited work will see the cancellation
		// token tripped and unwind on its next checkpoint.
		RequestCancel();
	}

	private void RequestCancel() {
		if (_cts.IsCancellationRequested) return;
		try { _cts.Cancel(); } catch (ObjectDisposedException) { /* already disposed */ }
		cmdCancel.Enabled = false;
		lblStatus.Text = "Cancelling...";
	}
}
