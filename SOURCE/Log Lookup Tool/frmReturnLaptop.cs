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
    public partial class frmReturnLaptop : Form {
        public string EDIPI;
        public string SN;
        private string _PickupEDIPI;
        private string _PickupFullScan;
        public frmReturnLaptop() {
            InitializeComponent();
        }

        private void frmReturnLaptop_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'eUDCustody.Custody' table. You can move, or remove it, as needed.
            //this.custodyTableAdapter.Fill(this.eUDCustody.Custody);
            dgvResults.DataSource = null;
            dgvResults.Refresh();
            if (!String.IsNullOrEmpty(EDIPI)) {
                custodyTableAdapter.FillByDoDID(eUDCustody.Custody, EDIPI);
                Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                Program.eventLog.WriteEntry($"Custody data retreived by DoDID", System.Diagnostics.EventLogEntryType.Information, 4101);
                dgvResults.DataSource = eUDCustody.Custody;
                dgvResults.Refresh();
                return;
            }
            if (!String.IsNullOrEmpty(SN)) {
                custodyTableAdapter.FillBySN(eUDCustody.Custody, SN);
                Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                Program.eventLog.WriteEntry($"Custody data retreived by SN for {SN}", System.Diagnostics.EventLogEntryType.Information, 4101); 
                dgvResults.DataSource = eUDCustody.Custody;
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
            if (e.ColumnIndex == dgvResults.Columns["CustomerName"].Index) {
                if (dgvResults.Rows[e.RowIndex].Cells["FullScan"].Value != DBNull.Value && dgvResults.Rows[e.RowIndex].Cells["FullScan"].Value != null) {
                    Scan scan = new Scan(dgvResults.Rows[e.RowIndex].Cells["FullScan"].Value.ToString(), true);
                    dgvResults.Rows[e.RowIndex].Cells["CustomerName"].Value = $"{scan.Surname}, {scan.FirstName}";
                } else if (dgvResults.Rows[e.RowIndex].Cells["DoDID"].Value != DBNull.Value && dgvResults.Rows[e.RowIndex].Cells["DoDID"].Value != null) {
                    string dodid = dgvResults.Rows[e.RowIndex].Cells["DoDID"].Value.ToString();
                    DirectoryEntry rootDSE = new DirectoryEntry(Properties.Resources.LDAPRoot);
                    var defaultNamingContext = rootDSE.Properties["defaultNamingContext"].Value;
                    using (DirectorySearcher directorySearcher = new DirectorySearcher(@"LDAP://" + defaultNamingContext)) {
                        directorySearcher.Filter = $"(userprincipalname={dodid}*)";
                        SearchResult result = directorySearcher.FindOne();
                        if (result is null) {
                            return;
                        }
                        DirectoryEntry ADUser = result.GetDirectoryEntry();
                        string name = $"{ADUser.Properties["sn"][0].ToString().ToNameCase()}, {ADUser.Properties["givenname"][0].ToString().ToNameCase()}";
                        dgvResults.Rows[e.RowIndex].Cells["CustomerName"].Value = name;
                    }
                }
            }
            if (e.ColumnIndex == dgvResults.Columns["PickupDoDID"].Index) {
                if (dgvResults.Rows[e.RowIndex].Cells["Returned"].Value != DBNull.Value && dgvResults.Rows[e.RowIndex].Cells["PickupDoDID"].Value == DBNull.Value) { 
                    dgvResults.Rows[e.RowIndex].Cells["PickupDoDID"].Value = dgvResults.Rows[e.RowIndex].Cells["DoDID"].Value;
                    dgvResults.Rows[e.RowIndex].Cells["PickupName"].Value = dgvResults.Rows[e.RowIndex].Cells["CustomerName"].Value;
                }
            }
            if (e.ColumnIndex == dgvResults.Columns["PickupName"].Index) {
                if (dgvResults.Rows[e.RowIndex].Cells["PickupFullScan"].Value != DBNull.Value && dgvResults.Rows[e.RowIndex].Cells["PickupFullScan"].Value != null) {
                    Scan scan = new Scan(dgvResults.Rows[e.RowIndex].Cells["PickupFullScan"].Value.ToString(), true);
                    dgvResults.Rows[e.RowIndex].Cells["PickupName"].Value = $"{scan.Surname}, {scan.FirstName}";
                } else if (dgvResults.Rows[e.RowIndex].Cells["PickupDoDID"].Value != DBNull.Value && dgvResults.Rows[e.RowIndex].Cells["PickupDoDID"].Value != null) {
                    string dodid = dgvResults.Rows[e.RowIndex].Cells["PickupDoDID"].Value.ToString();
                    DirectoryEntry rootDSE = new DirectoryEntry(Properties.Resources.LDAPRoot);
                    var defaultNamingContext = rootDSE.Properties["defaultNamingContext"].Value;
                    using (DirectorySearcher directorySearcher = new DirectorySearcher(@"LDAP://" + defaultNamingContext)) {
                        directorySearcher.Filter = $"(userprincipalname={dodid}*)";
                        SearchResult result = directorySearcher.FindOne();
                        if (result is null) {
                            return;
                        }
                        DirectoryEntry ADUser = result.GetDirectoryEntry();
                        string name = $"{ADUser.Properties["sn"][0].ToString().ToNameCase()}, {ADUser.Properties["givenname"][0].ToString().ToNameCase()}";
                        dgvResults.Rows[e.RowIndex].Cells["PickupName"].Value = name;
                    }
                }
            }
        }

        private void btnSNSearch_Click(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(txtSNSearch.Text))
                return;
            SN = txtSNSearch.Text;
            txtSNSearch.Clear();
            custodyTableAdapter.FillBySN(eUDCustody.Custody, SN);
            Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
            Program.eventLog.WriteEntry($"Custody data retreived by SN for {SN}", System.Diagnostics.EventLogEntryType.Information, 4101);
            dgvResults.DataSource = eUDCustody.Custody;
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
            custodyTableAdapter.FillByDoDID(eUDCustody.Custody, EDIPI);
            Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
            Program.eventLog.WriteEntry($"Custody data retreived by DoDID", System.Diagnostics.EventLogEntryType.Information, 4101);
            dgvResults.DataSource = eUDCustody.Custody;
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
            if (dgvResults.Rows[currentRow].Cells["Returned"].Value != null && !String.IsNullOrEmpty(dgvResults.Rows[currentRow].Cells["Returned"].Value.ToString()))
                return;
            string query = @"UPDATE Custody SET Returned=@returned, ReturnedBy=@tech, PickupDoDID=@pickupedipi, PickupFullScan=@pickupfullscan WHERE CustodyID=@id";
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
                    sqlCommand.Parameters.Add("@returned", SqlDbType.DateTime2);
                    sqlCommand.Parameters.Add("@tech", SqlDbType.VarChar);
                    sqlCommand.Parameters.Add("@pickupedipi", SqlDbType.VarChar);
                    sqlCommand.Parameters.Add("@pickupfullscan", SqlDbType.VarChar);
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                    sqlCommand.Parameters["@returned"].Value = DateTime.Now;
                    sqlCommand.Parameters["@tech"].Value = Environment.UserName;
                    sqlCommand.Parameters["@pickupedipi"].Value = dgvResults.Rows[currentRow].Cells["DoDID"].Value;
                    sqlCommand.Parameters["@pickupfullscan"].Value = dgvResults.Rows[currentRow].Cells["FullScan"].Value;
                    sqlCommand.Parameters["@id"].Value = (int)dgvResults.Rows[currentRow].Cells["CustodyId"].Value;
                    try {
                        if (sqlCommand.ExecuteNonQuery() == 1) {
                            Program.eventLog.WriteEntry($"Custody data written", System.Diagnostics.EventLogEntryType.Information, 4201);
                            MessageBox.Show("Return processed!");
                            frmReturnLaptop frmReturnLaptop = new frmReturnLaptop {
                                EDIPI = EDIPI,
                                SN = SN
                            };
                            frmReturnLaptop.Show();
                            this.Close();
                        } else {
                            MessageBox.Show($"Return failed! Record return manually and contact developer. \r\n  {DateTime.Now:d} \r\n {Environment.UserName} \r\n {dgvResults.SelectedRows[0].Cells["CustodyId"].Value}");
                        }
                    } catch (SqlException ex) {
                        MessageBox.Show($"Return failed! Record acceptance manually and contact developer. \r\n {DateTime.Now:d} \r\n {Environment.UserName} \r\n {dgvResults.SelectedRows[0].Cells["CustodyId"].Value} \r\n {ex.Message} \r\n {ex.Number}");
                    }
                }
            }
            dgvResults.Refresh();
        }

        private void txtReturnAlt_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                btnReturnAlt_Click(btnReturnAlt, new EventArgs());
            }
        }

        private void btnReturnAlt_Click(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(txtReturnAlt.Text))
                return;
            if (txtReturnAlt.TextLength == 10) {
                _PickupEDIPI = txtReturnAlt.Text;
            } else {
                Scan cacdata = new Scan(txtReturnAlt.Text);
                switch (cacdata.ScanResult) {
                    case ScanStatus.UnsupportedCard:
                        MessageBox.Show("This type of ID does not encode a DoDID. Please use manual entry.");
                        txtReturnAlt.Clear();
                        return;
                    case ScanStatus.UnknownDataFormat:
                        MessageBox.Show("Input does not match expected length for EDIPI manual entry, 1D CAC barcode or 2D CAC barcode.  Please re-enter data.");
                        txtReturnAlt.Clear();
                        return;
                    case ScanStatus.InvalidScanData:
                        MessageBox.Show($"Validation testing for this barcode failed. If this recurrs, provide CAC scan to developer.\r\n{txtUserSearch.Text}");
                        return;
                    case ScanStatus.NullInput:
                        return;
                    case ScanStatus.Success:
                        _PickupEDIPI = cacdata.Edipi;
                        _PickupFullScan = txtReturnAlt.Text;
                        break;
                }
            }
            if (String.IsNullOrEmpty(_PickupEDIPI)) {
                MessageBox.Show("Unable to parse input to obtain DoDID.");
                return;
            }
            if (_PickupEDIPI is null)
                return;
            int currentRow;
            if (dgvResults.SelectedRows.Count == 0) {
                currentRow = dgvResults.SelectedCells[0].RowIndex;
            } else {
                currentRow = dgvResults.SelectedRows[0].Index;
            }
            if (dgvResults.Rows[currentRow].Cells["Returned"].Value != null && !String.IsNullOrEmpty(dgvResults.Rows[currentRow].Cells["Returned"].Value.ToString()))
                return;
            string query = @"UPDATE Custody SET Returned=@returned, ReturnedBy=@tech, PickupDoDID=@pickupedipi, PickupFullScan=@pickupfullscan WHERE CustodyID=@id";
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
                    sqlCommand.Parameters.Add("@returned", SqlDbType.DateTime2);
                    sqlCommand.Parameters.Add("@tech", SqlDbType.VarChar);
                    sqlCommand.Parameters.Add("@pickupedipi", SqlDbType.VarChar);
                    sqlCommand.Parameters.Add("@pickupfullscan", SqlDbType.VarChar);
                    sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                    sqlCommand.Parameters["@returned"].Value = DateTime.Now;
                    sqlCommand.Parameters["@tech"].Value = Environment.UserName;
                    sqlCommand.Parameters["@pickupedipi"].Value = _PickupEDIPI;
                    if (_PickupFullScan is null) {
                        DirectoryEntry rootDSE = new DirectoryEntry(Properties.Resources.LDAPRoot);
                        var defaultNamingContext = rootDSE.Properties["defaultNamingContext"].Value;
                        using (DirectorySearcher directorySearcher = new DirectorySearcher(@"LDAP://" + defaultNamingContext)) {
                            directorySearcher.Filter = $"(userprincipalname={_PickupEDIPI}*)";
                            SearchResult result = directorySearcher.FindOne();
                            if (result is null) {
                                sqlCommand.Parameters["@pickupfullscan"].Value = DBNull.Value;
                            } else {
                                DirectoryEntry ADUser = result.GetDirectoryEntry();
                                sqlCommand.Parameters["@pickupfullscan"].Value = $"M{ConvertEDIPIToBase32(_PickupEDIPI)}xxxxxxxx{ADUser.Properties["givenname"][0].ToString().ToNameCase(),-20}x{ADUser.Properties["sn"][0].ToString().ToNameCase(),-26}xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
                            }
                        }
                    } else {
                        sqlCommand.Parameters["@pickupfullscan"].Value = _PickupFullScan;
                    }
                    sqlCommand.Parameters["@id"].Value = (int)dgvResults.Rows[currentRow].Cells["CustodyId"].Value;
                    try {
                        if (sqlCommand.ExecuteNonQuery() == 1) {
                            Program.eventLog.WriteEntry($"Custody data written", System.Diagnostics.EventLogEntryType.Information, 4201);
                            MessageBox.Show("Return processed!");
                            frmReturnLaptop frmReturnLaptop = new frmReturnLaptop {
                                EDIPI = EDIPI,
                                SN = SN
                            };
                            frmReturnLaptop.Show();
                            this.Close();
                        } else {
                            MessageBox.Show($"Return failed! Record return manually and contact developer. \r\n  {DateTime.Now:d} \r\n {Environment.UserName} \r\n {dgvResults.SelectedRows[0].Cells["CustodyId"].Value}");
                        }
                    } catch (SqlException ex) {
                        MessageBox.Show($"Return failed! Record acceptance manually and contact developer. \r\n {DateTime.Now:d} \r\n {Environment.UserName} \r\n {dgvResults.SelectedRows[0].Cells["CustodyId"].Value} \r\n {ex.Message} \r\n {ex.Number}");
                    }
                }
            }
            dgvResults.Refresh();
        }
    }
}
