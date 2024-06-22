﻿using IPAddressChanger.Properties;
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
	public partial class frmSettings : Form {
		internal frmMain mainForm { get; set; }

		private bool controlsDirty = false;
		private void MarkDirty() {
			controlsDirty = true;
			cmdOK.Enabled = true;
		}

		private void LoadSettings() {
			chkCtrl.Checked = (Settings.Default.HotkeyModifier & (uint)IPAddressChanger.ModifierKeys.Control) != 0;
			chkAlt.Checked = (Settings.Default.HotkeyModifier & (uint)IPAddressChanger.ModifierKeys.Alt) != 0;
			chkShift.Checked = (Settings.Default.HotkeyModifier & (uint)IPAddressChanger.ModifierKeys.Shift) != 0;
			cboHotkey.SelectedIndex = (int)(Settings.Default.Hotkey - 112);

			chkHideWhenMinimized.Checked = Settings.Default.HideWhenMinimized;
			cboShortcutDoubleClick.SelectedIndex = Settings.Default.ShortcutDoubleClick;
			chkStartMinimized.Checked = Settings.Default.StartMinimized;
			txtControlPanelFile.Text = Settings.Default.AdaptersControlPanelFile;
			cmdOK.Enabled = false;
			this.controlsDirty = false;
		}

		private void SaveSettings() {
			Settings.Default.HideWhenMinimized = chkHideWhenMinimized.Checked;
			Settings.Default.ShortcutDoubleClick = cboShortcutDoubleClick.SelectedIndex;
			Settings.Default.StartMinimized = chkStartMinimized.Checked;
			Settings.Default.AdaptersControlPanelFile = txtControlPanelFile.Text;
			Settings.Default.Hotkey = (uint)cboHotkey.SelectedIndex + 112;
			Settings.Default.HotkeyModifier = (
				(uint)(chkCtrl.Checked ? IPAddressChanger.ModifierKeys.Control : 0)
				| (uint)(chkAlt.Checked ? IPAddressChanger.ModifierKeys.Alt : 0)
				| (uint)(chkShift.Checked ? IPAddressChanger.ModifierKeys.Shift : 0)
			);
			this.controlsDirty = false;
			Settings.Default.Save();
			this.mainForm.LoadSettings();
		}

		public frmSettings(frmMain parent) {
			this.mainForm = parent;
			InitializeComponent();
		}

		private void chkHideWhenMinimized_CheckedChanged(object sender, EventArgs e) {
			MarkDirty();
		}

		private void frmSettings_FormClosing(object sender, FormClosingEventArgs e) {
			if (e.CloseReason == CloseReason.UserClosing && controlsDirty) {
				DialogResult userAnswer = MessageBox.Show("Do you want to save your changes?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				switch (userAnswer) {
					case DialogResult.Yes:
						// save and let the form close
						SaveSettings();
						break;
					case DialogResult.No:
						// don't save but let the form close (don't do anything)
						break;
					case DialogResult.Cancel:
						// don't close the form
						e.Cancel = true;
						break;
				}
			}
		}

		private void frmSettings_Load(object sender, EventArgs e) {
			LoadSettings();
		}

		private void cmdCancel_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void frmSettings_FormClosed(object sender, FormClosedEventArgs e) {
			// tell the main form that we're gone
			this.mainForm.settingsForm = null;
		}

		private void cmdOK_Click(object sender, EventArgs e) {
			SaveSettings();
			this.Close();
		}

		private void cboShortcutDoubleClick_SelectedIndexChanged(object sender, EventArgs e) {
			MarkDirty();
		}

		private void chkStartMinimized_CheckedChanged(object sender, EventArgs e) {
			MarkDirty();
		}

		private void lblHideWhenMinimized_Click(object sender, EventArgs e) {
			chkHideWhenMinimized.Checked = !chkHideWhenMinimized.Checked;
		}

		private void lblStartMinimized_Click(object sender, EventArgs e) {
			chkStartMinimized.Checked = !chkStartMinimized.Checked;
		}

		private void cmdControlPanelBrowse_Click(object sender, EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.FileName = txtControlPanelFile.Text;
			ofd.Filter = "Control Panel Files|*.cpl|Executables|*.exe|All Files|*.*";
			ofd.Multiselect = false;
			ofd.Title = "Selet Network Adapters Control Panel Applet";
			ofd.DefaultExt = ".cpl";
			ofd.ValidateNames = true;
			if (ofd.ShowDialog(this) == DialogResult.OK) {
				txtControlPanelFile.Text = ofd.FileName;
				MarkDirty();
			}
		}

		private void txtControlPanelFile_TextChanged(object sender, EventArgs e) {
			MarkDirty();
		}

		private void chkCtrl_CheckedChanged(object sender, EventArgs e) {
			MarkDirty();
		}

		private void chkAlt_CheckedChanged(object sender, EventArgs e) {
			MarkDirty();
		}

		private void chkShift_CheckedChanged(object sender, EventArgs e) {
			MarkDirty();
		}

		private void cboHotkey_SelectedIndexChanged(object sender, EventArgs e) {
			MarkDirty();
		}
	}
}
