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
using CAC;

namespace Log_Lookup_Tool {
    public partial class frmIAReturn : Form {
        public string EDIPI;
        public string SN;
        public frmIAReturn() {
            InitializeComponent();
        }

        private void frmIAReturn_Load(object sender, EventArgs e) {
            this.iaccess_logTableAdapter.FillByIssued(this.eUDCustody.iaccess_log);
            dgvResults.DataSource = eUDCustody.iaccess_log;
            dgvResults.Refresh();
            if (!String.IsNullOrEmpty(EDIPI)) {
                txtUserSearch.Text = EDIPI;
                iaccess_logTableAdapter.FillByDoDID(eUDCustody.iaccess_log, EDIPI);
                Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                Program.eventLog.WriteEntry($"Custody data retreived by DoDID", System.Diagnostics.EventLogEntryType.Information, 4101);
                dgvResults.DataSource = eUDCustody.iaccess_log;
                dgvResults.Refresh();
                return;
            }
            if (!String.IsNullOrEmpty(SN)) {
                txtSNSearch.Text = SN;
                iaccess_logTableAdapter.FillBySerial(eUDCustody.iaccess_log, Int32.Parse(SN));
                Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                Program.eventLog.WriteEntry($"Custody data retreived by SN for {SN}", System.Diagnostics.EventLogEntryType.Information, 4101); 
                dgvResults.DataSource = eUDCustody.iaccess_log;
                dgvResults.Refresh();
                return;
            }
        }
        private string ConvertToBase32(ulong value) {
            if (value <= 9)
                return value.ToString();
            return ((char)(value + 55)).ToString();
        }

        private string ConvertEDIPIToBase32(string EDIPI) {
            StringBuilder result = new StringBuilder();
            ulong numerator = ulong.Parse(EDIPI);
            ulong quotient = numerator / 32;
            ulong remainder = numerator % 32;
            result.Append(ConvertToBase32(remainder));
            while (quotient != 0) {
                numerator = quotient;
                quotient = numerator / 32;
                remainder = numerator % 32;
                result.Append(ConvertToBase32(remainder));
            }
            string workingString = result.ToString();
            char[] workingArray = workingString.ToCharArray();
            Array.Reverse(workingArray);
            workingString = new string(workingArray);
            return workingString;
        }
        private void dgvResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {

        }

        private void btnSNSearch_Click(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(txtSNSearch.Text))
                return;
            SN = txtSNSearch.Text;
            txtSNSearch.Clear();
            iaccess_logTableAdapter.FillBySerial(eUDCustody.iaccess_log, Int32.Parse(SN));
            Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
            Program.eventLog.WriteEntry($"Custody data retreived by SN for {SN}", System.Diagnostics.EventLogEntryType.Information, 4101);
            dgvResults.DataSource = eUDCustody.iaccess_log;
            dgvResults.Refresh();
        }

        private void btnUserSearch_Click(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(txtUserSearch.Text))
                return;
            if (txtUserSearch.TextLength == 10) {
                EDIPI = txtUserSearch.Text;
            } else {
                Scan cacdata = new Scan(txtUserSearch.Text);
                switch (cacdata.ScanResult) {
                    case ScanStatus.UnsupportedCard:
                        MessageBox.Show("This type of ID does not encode a DoDID. Please use manual entry.");
                        txtUserSearch.Clear();
                        return;
                    case ScanStatus.UnknownDataFormat:
                        MessageBox.Show("Input does not match expected length for EDIPI manual entry, 1D CAC barcode or 2D CAC barcode.  Please re-enter data.");
                        txtUserSearch.Clear();
                        return;
                    case ScanStatus.InvalidScanData:
                        MessageBox.Show($"Validation testing for this barcode failed. If this recurrs, provide CAC scan to developer.\r\n{txtUserSearch.Text}");
                        return;
                    case ScanStatus.NullInput:
                        return;
                    case ScanStatus.Success:
                        EDIPI = cacdata.Edipi;
                        break;
                }
            }
            if (String.IsNullOrEmpty(EDIPI)) {
                MessageBox.Show("Unable to parse input to obtain DoDID.");
                return;
            }
            txtUserSearch.Clear();
            iaccess_logTableAdapter.FillByDoDID(eUDCustody.iaccess_log, EDIPI);
            Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
            Program.eventLog.WriteEntry($"Custody data retreived by DoDID", System.Diagnostics.EventLogEntryType.Information, 4101);
            dgvResults.DataSource = eUDCustody.iaccess_log;
            dgvResults.Refresh();
        }

        private void txtSNSearch_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                btnSNSearch_Click(btnSNSearch, new EventArgs());
            }
        }

        private void txtUserSearch_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                btnUserSearch_Click(btnUserSearch, new EventArgs());
            }
        }

        private void btnReturn_Click(object sender, EventArgs e) {
            int currentRow;
            if (dgvResults.SelectedRows.Count == 0) {
                currentRow = dgvResults.SelectedCells[0].RowIndex;
            } else {
                currentRow = dgvResults.SelectedRows[0].Index;
            }
            if (dgvResults.Rows[currentRow].Cells["return_date"].Value != null && !String.IsNullOrEmpty(dgvResults.Rows[currentRow].Cells["return_date"].Value.ToString()))
                return;
            string query = @"dbo.iAccessReturn";
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
                    CommandType = CommandType.StoredProcedure,
                    CommandText = query
                }) {
                    sqlCommand.Parameters.Add("@issuance_id", SqlDbType.Int);
                    sqlCommand.Parameters["@issuance_id"].Value = (int)dgvResults.Rows[currentRow].Cells["issuance_id"].Value;
                    try {
                        if (sqlCommand.ExecuteNonQuery() == 1) {
                            Program.eventLog.WriteEntry($"Badge data written", System.Diagnostics.EventLogEntryType.Information, 4201);
                            MessageBox.Show("Return processed!");
                            frmIAReturn frmIAReturn = new frmIAReturn {
                                EDIPI = EDIPI,
                                SN = SN
                            };
                            frmIAReturn.Show();
                            Close();
                        } else {
                            MessageBox.Show($"Return failed! Record return manually and contact developer. \r\n  {DateTime.Now:d} \r\n {Environment.UserName} \r\n {dgvResults.SelectedRows[0].Cells["issuance_id"].Value}");
                        }
                    } catch (SqlException ex) {
                        MessageBox.Show($"Return failed! Record acceptance manually and contact developer. \r\n {DateTime.Now:d} \r\n {Environment.UserName} \r\n {dgvResults.SelectedRows[0].Cells["badge_id"].Value} \r\n {ex.Message} \r\n {ex.Number}");
                    }
                }
            }
            dgvResults.Refresh();
        }

        private void SetLost(int currentRow, int lostVal) {
            string query = @"UPDATE iaccess_badges SET badge_status=@lossval WHERE badge_id=@id";
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
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                    sqlCommand.Parameters.Add("@lossval", SqlDbType.Int);
                    sqlCommand.Parameters["@id"].Value = (int)dgvResults.Rows[currentRow].Cells["badge_id"].Value;
                    sqlCommand.Parameters["@lossval"].Value = lostVal;
                    try {
                        if (sqlCommand.ExecuteNonQuery() == 1) {
                            Program.eventLog.WriteEntry($"Badge data written", System.Diagnostics.EventLogEntryType.Information, 4201);
                            MessageBox.Show("Badge status updated!");
                            frmIAReturn frmIAReturn = new frmIAReturn {
                                EDIPI = EDIPI,
                                SN = SN
                            };
                            frmIAReturn.Show();
                            this.Close();
                        } else {
                            MessageBox.Show($"Badge status not updated. Record status change manually and contact developer. \r\n  {DateTime.Now:d} \r\n {Environment.UserName} \r\n {dgvResults.SelectedRows[0].Cells["CustodyId"].Value}");
                        }
                    } catch (SqlException ex) {
                        MessageBox.Show($"Badge status not updated. Record status change manually and contact developer. \r\n {DateTime.Now:d} \r\n {Environment.UserName} \r\n {dgvResults.SelectedRows[0].Cells["CustodyId"].Value} \r\n {ex.Message} \r\n {ex.Number}");
                    }
                }
            }
        }

        private void btnLost_Click(object sender, EventArgs e) {
            int currentRow;
            if (dgvResults.SelectedRows.Count == 0) {
                currentRow = dgvResults.SelectedCells[0].RowIndex;
            } else {
                currentRow = dgvResults.SelectedRows[0].Index;
            }
            SetLost(currentRow, 3);
            dgvResults.Refresh();
        }

        private void btnFound_Click(object sender, EventArgs e) {
            int currentRow;
            if (dgvResults.SelectedRows.Count == 0) {
                currentRow = dgvResults.SelectedCells[0].RowIndex;
            } else {
                currentRow = dgvResults.SelectedRows[0].Index;
            }
            SetLost(currentRow, 1);
            dgvResults.Refresh();
        }

        private void btnReturnedToDHA_Click(object sender, EventArgs e) {
            int currentRow;
            if (dgvResults.SelectedRows.Count == 0) {
                currentRow = dgvResults.SelectedCells[0].RowIndex;
            } else {
                currentRow = dgvResults.SelectedRows[0].Index;
            }
            SetLost(currentRow, 4);
            dgvResults.Refresh();
        }
    }
}
