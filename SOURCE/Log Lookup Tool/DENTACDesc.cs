using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Log_Lookup_Tool {
    public partial class DENTACDesc : Form {
        private string _bldg = "";
        private string _room = "";
        private string _bldgdesc = "";
        private string _roomdesc = "";
        private bool _clear = false;
        public string Description {
            get {
                if (_clear) {
                    return "";
                }
                StringBuilder sb = new StringBuilder();
                sb.Append($"Bldg {_bldg}");
                if (!String.IsNullOrEmpty(_room) && !String.IsNullOrEmpty(_roomdesc)) {
                    sb.Append($", Rm {_room} - {_roomdesc}");
                } else if (!String.IsNullOrEmpty(_room)) {
                    sb.Append($", Rm {_room}");
                } else if (!String.IsNullOrEmpty(_roomdesc)) {
                    sb.Append($", Rm: {_roomdesc}");
                }
                if (!String.IsNullOrEmpty(_bldgdesc)) {
                    sb.Append($", {_bldgdesc}");
                }
                return sb.ToString();
            } 
            set {
                if (string.IsNullOrEmpty(value)) {
                    _bldg = "";
                    _room = "";
                    _bldgdesc = "";
                    _roomdesc = "";
                } else {
                    Regex regex = new Regex(@"Bldg (?<bldg>\d*)(-?\w?), Rm(( (?<rm>[A-Za-z0-9-]*)( - )?)|(: ))(?<rmdesc>\w*)?(, (?<bldgdesc>.*))?");
                    Match match = regex.Match(value);
                    if (!match.Success)
                        return;
                    _bldg = match.Groups["bldg"].Value;
                    if (match.Groups["rm"].Success)
                        _room = match.Groups["rm"].Value;
                    if (match.Groups["rmdesc"].Success)
                        _roomdesc = match.Groups["rmdesc"].Value;
                    if (match.Groups["bldgdesc"].Success)
                        _bldgdesc = match.Groups["bldgdesc"].Value;
                }
            }
        }
        public DENTACDesc() {
            InitializeComponent();
            switch (_bldg) {
                case "25501":
                    rbSDC.Checked = true;
                    break;
                case "320":
                    rbTDC.Checked = true;
                    break;
                case "300":
                    rbHDC.Checked = true;
                    break;
                case "38801":
                    rbHQ.Checked = true;
                    break;
                default:
                    txtBldg.Text = _bldg;
                    txtBldg.Enabled = true;
                    txtBldgDesc.Text = _bldgdesc;
                    txtBldgDesc.Enabled = true;
                    rbOther.Checked = true;
                    break;
            }
            if (_room != "")
                txtRoom.Text = _room;
            if (_roomdesc != "")
                txtRoomDesc.Text = _roomdesc;
        }

        private void rbClinic_CheckedChanged(object sender, EventArgs e) {
            if (rbHQ.Checked) {
                _bldg = "38801";
                _bldgdesc = "HQs Dental Health Activity";
                txtBldg.Enabled = false;
                lblBldg.Enabled = false;
                txtBldgDesc.Enabled = false;
                lblBldgDesc.Enabled = false;
            } else if (rbHDC.Checked) {
                _bldg = "300";
                _bldgdesc = "Hospital Dental Clinic";
                txtBldg.Enabled = false;
                lblBldg.Enabled = false;
                txtBldgDesc.Enabled = false;
                lblBldgDesc.Enabled = false;
            } else if (rbSDC.Checked) {
                _bldg = "25501";
                _bldgdesc = "Snyder Dental Clinic";
                txtBldg.Enabled = false;
                lblBldg.Enabled = false;
                txtBldgDesc.Enabled = false;
                lblBldgDesc.Enabled = false;
            } else if (rbTDC.Checked) {
                _bldg = "320";
                _bldgdesc = "Tingay Dental Clinic";
                txtBldg.Enabled = false;
                lblBldg.Enabled = false;
                txtBldgDesc.Enabled = false;
                lblBldgDesc.Enabled = false;
            } else if (rbOther.Checked) {
                txtBldg.Enabled = true;
                lblBldg.Enabled = true;
                txtBldgDesc.Enabled = true;
                lblBldgDesc.Enabled = true;
                _bldg = txtBldg.Text;
                _bldgdesc = txtBldgDesc.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e) {
            _clear = true;
            Close();
        }

        private void btnSet_Click(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(_bldg)) {
                MessageBox.Show("A building must be selected to set description.");
            }
            if (rbOther.Checked) {
                _bldg = txtBldg.Text;
                _bldgdesc = txtBldgDesc.Text;
            }
            _room = txtRoom.Text;
            _roomdesc = txtRoomDesc.Text;
            Close();
        }

        private void DENTACDesc_Shown(object sender, EventArgs e) {
            switch (_bldg) {
                case "25501":
                    rbSDC.Checked = true;
                    txtBldg.Enabled = false;
                    lblBldg.Enabled = false;
                    txtBldgDesc.Enabled = false;
                    lblBldgDesc.Enabled = false;
                    break;
                case "320":
                    rbTDC.Checked = true;
                    txtBldg.Enabled = false;
                    lblBldg.Enabled = false;
                    txtBldgDesc.Enabled = false;
                    lblBldgDesc.Enabled = false;
                    break;
                case "300":
                    rbHDC.Checked = true;
                    txtBldg.Enabled = false;
                    lblBldg.Enabled = false;
                    txtBldgDesc.Enabled = false;
                    lblBldgDesc.Enabled = false;
                    break;
                case "38801":
                    rbHQ.Checked = true;
                    txtBldg.Enabled = false;
                    lblBldg.Enabled = false;
                    txtBldgDesc.Enabled = false;
                    lblBldgDesc.Enabled = false;
                    break;
                default:
                    txtBldg.Text = _bldg;
                    txtBldgDesc.Text = _bldgdesc;
                    txtBldg.Enabled = true;
                    lblBldg.Enabled = true;
                    txtBldgDesc.Enabled = true;
                    lblBldgDesc.Enabled = true;
                    rbOther.Checked = true;
                    break;
            }
            if (_room != "")
                txtRoom.Text = _room;
            if (_roomdesc != "")
                txtRoomDesc.Text = _roomdesc;
        }
    }
}
