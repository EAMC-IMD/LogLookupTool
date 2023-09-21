using CAC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log_Lookup_Tool {
    public partial class frmIAIssue : Form {
        private string _DoDID;
        private string _Barcode;
        private string _Name;
        private string _SerialNumber;
        private Scan cacdata;
        public frmIAIssue() {
            InitializeComponent();
        }
        private void txtCACScan_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (String.IsNullOrEmpty(txtCACScan.Text))
                    return;
                if (txtCACScan.TextLength == 10) {
                    _DoDID = txtCACScan.Text;
                } else {
                    cacdata = new Scan(txtCACScan.Text);
                    switch (cacdata.ScanResult) {
                        case ScanStatus.UnsupportedCard:
                            MessageBox.Show("This type of ID does not encode a DoDID. Please use manual entry.");
                            txtCACScan.Clear();
                            return;
                        case ScanStatus.UnknownDataFormat:
                            MessageBox.Show("Input does not match expected length for EDIPI manual entry, 1D CAC barcode or 2D CAC barcode.  Please re-enter data.");
                            txtCACScan.Clear();
                            return;
                        case ScanStatus.InvalidScanData:
                            MessageBox.Show($"Validation testing for this barcode failed. If this recurrs, provide CAC scan to developer.\r\n{txtCACScan.Text}");
                            return;
                        case ScanStatus.NullInput:
                            return;
                        case ScanStatus.Success:
                            _DoDID = cacdata.Edipi;
                            if (cacdata.Barcode == BarcodeType.PDF417N || cacdata.Barcode == BarcodeType.PDF417M) {
                                _Name = $"{cacdata.Surname}, {cacdata.FirstName}";
                                _Barcode = txtCACScan.Text;
                                lblUserName.Text = _Name;
                            }
                            txtBadgeSerial.Focus();
                            break;
                    }
                }
                if (String.IsNullOrEmpty(_DoDID)) {
                    MessageBox.Show("Unable to parse input to obtain DoDID.");
                    return;
                }
                txtCACScan.Text = _DoDID;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(_DoDID)) {
                if (txtCACScan.TextLength == 10) {
                    _DoDID = txtCACScan.Text;
                } else {
                    MessageBox.Show("Acceptence not possible without DoDID or CAC scan.");
                    return;
                }
            }
            if (String.IsNullOrEmpty(_SerialNumber)) {
                if (!String.IsNullOrEmpty(txtBadgeSerial.Text)) {
                    _SerialNumber = txtBadgeSerial.Text;
                } else {
                    MessageBox.Show("Acceptence not possible without badge number");
                    return;
                }
            }
            string query = @"SELECT COUNT(badge_id) FROM iaccess_badges WHERE badge_serial=@checkval";
            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder {
                ["Server"] = Properties.Resources.SQLServer,
                ["Initial Catalog"] = Properties.Resources.Database,
                ["Integrated Security"] = true
            };
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString())) {
                sqlConnection.Open();
                Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                using (SqlCommand sqlCommand = new SqlCommand {
                    Connection = sqlConnection,
                    CommandText = query
                }) {
                    sqlCommand.Parameters.Add("@checkval", SqlDbType.Int);
                    sqlCommand.Parameters["@checkval"].Value = _SerialNumber;
                    if ((int)sqlCommand.ExecuteScalar() == 0) {
                        MessageBox.Show("Badge serial does not exist in logs.  Issuance cannot proceed.");
                        return;
                    }
                }
            }
            string samaccountname = Environment.UserName;
            DateTime acceptence = DateTime.Now;
            string message = $"Badge {_SerialNumber} issued to {_DoDID} and by {samaccountname} on {acceptence:g}";
            if (MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                query = @"dbo.iAccessIssuance";
                sqlConnectionString = new SqlConnectionStringBuilder {
                    ["Server"] = Properties.Resources.SQLServer,
                    ["Initial Catalog"] = Properties.Resources.Database,
                    ["Integrated Security"] = true
                };
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString())) {
                    sqlConnection.Open();
                    Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                    using (SqlCommand sqlCommand = new SqlCommand {
                        Connection = sqlConnection,
                        CommandText = query,
                        CommandType = CommandType.StoredProcedure
                    }) {
                        sqlCommand.Parameters.Add("@serial", SqlDbType.VarChar);
                        sqlCommand.Parameters.Add("@issued_to", SqlDbType.Char);

                        sqlCommand.Parameters["@serial"].Value = _SerialNumber.ToUpperInvariant();
                        sqlCommand.Parameters["@issued_to"].Value = _DoDID;

                        try {
                            if (sqlCommand.ExecuteNonQuery() == 1) {
                                Program.eventLog.WriteEntry("Issuance data written", System.Diagnostics.EventLogEntryType.Information, 4201);
                                MessageBox.Show("Issuance processed!");
                                frmIAIssue frmIAIssue = new frmIAIssue();
                                frmIAIssue.Show();
                                this.Close();
                            } else {
                                MessageBox.Show($"Issuance failed! Record issuance manually and contact developer. \r\n {_SerialNumber} \r\n {_DoDID} \r\n {acceptence:d} \r\n {samaccountname}");
                            }
                        } catch (SqlException ex) {
                            MessageBox.Show($"Issuance failed! Record acceptance manually and contact developer. \r\n {_SerialNumber} \r\n {_DoDID} \r\n {acceptence:d} \r\n {samaccountname} \r\n {ex.Message} \r\n {ex.Number}");
                        }
                    }
                }
            } else {
                return;
            }
        }
    }
}
