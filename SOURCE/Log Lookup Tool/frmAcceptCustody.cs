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
    public partial class frmAcceptCustody : Form {
        private string _DoDID;
        private string _Barcode;
        private string _Name;
        private string _SerialNumber;
        private Scan cacdata;
        public frmAcceptCustody() {
            InitializeComponent();
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
            return workingString.PadLeft(7, '0');
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
                            txtLaptopScan.Focus();
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

        private void txtLaptopScan_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (txtLaptopScan.TextLength == 32) {
                    lblLaptopInfo.Text = "ECN barcode detected.  Barcode will be stored, but manual retrieval not possible.";
                } else {
                    string query = @"SELECT ECN, MfrSerialNo FROM pcInventory.dbo.DMLSS WHERE Ecn=@query OR MfrSerialNo=@query OR Ecn5=@query";
                    SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder {
                        ["Server"] = Program.settings.SQLServer,
                        ["Initial Catalog"] = "pcInventory",
                        ["Integrated Security"] = true
                    };
                    using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString())) {
                        sqlConnection.Open();
                        Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                        using (SqlCommand sqlCommand = new SqlCommand {
                            Connection = sqlConnection,
                            CommandText = query
                        }) {
                            sqlCommand.Parameters.Add("@query", SqlDbType.VarChar);
                            sqlCommand.Parameters["@query"].Value = txtLaptopScan.Text;
                            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                            Program.eventLog.WriteEntry("ECN lookup", System.Diagnostics.EventLogEntryType.Information, 4101);
                            if (sqlDataReader.HasRows && sqlDataReader.Read()) {
                                _SerialNumber = sqlDataReader["MfrSerialNo"].ToString();
                                string ECN = sqlDataReader["ECN"].ToString();
                                lblLaptopInfo.Text = $"ECN: {ECN}, SN: {_SerialNumber}";
                            }
                        }
                    }
                }
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
                if (!String.IsNullOrEmpty(txtLaptopScan.Text)) {
                    _SerialNumber = txtLaptopScan.Text;
                } else {
                    MessageBox.Show("Acceptence not possible without SN");
                    return;
                }
            }
            string samaccountname = Environment.UserName;
            DateTime acceptence = DateTime.Now;
            string message = $"SN {_SerialNumber} submitted by {_DoDID} and received by {samaccountname} on {acceptence:g}";
            if (MessageBox.Show(message,"Confirmation",MessageBoxButtons.YesNo) == DialogResult.Yes) {
                string query = @"INSERT INTO Custody (SerialNumber, DoDID, FullScan, Received, ReceivedBy) VALUES (@sn, @dodid, @cac, @received, @tech)";
                SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder {
                    ["Server"] = Program.settings.SQLServer,
                    ["Initial Catalog"] = Program.settings.Database,
                    ["Integrated Security"] = true
                };
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString())) {
                    sqlConnection.Open();
                    Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                    using (SqlCommand sqlCommand = new SqlCommand {
                        Connection = sqlConnection,
                        CommandText = query
                    }) {
                        sqlCommand.Parameters.Add("@sn", SqlDbType.VarChar);
                        sqlCommand.Parameters.Add("@dodid", SqlDbType.VarChar);
                        sqlCommand.Parameters.Add("@cac", SqlDbType.VarChar);
                        sqlCommand.Parameters.Add("@received", SqlDbType.DateTime2);
                        sqlCommand.Parameters.Add("@tech", SqlDbType.VarChar);
                        sqlCommand.Parameters["@sn"].Value = _SerialNumber.ToUpperInvariant();
                        sqlCommand.Parameters["@dodid"].Value = _DoDID;
                        if (cacdata != null && cacdata.ScanResult == ScanStatus.Success) {
                            sqlCommand.Parameters["@cac"].Value = _Barcode;
                        } else {
                            DirectoryEntry rootDSE = new DirectoryEntry(Program.settings.LDAPRoot);
                            var defaultNamingContext = rootDSE.Properties["defaultNamingContext"].Value;
                            using (DirectorySearcher directorySearcher = new DirectorySearcher(@"LDAP://" + defaultNamingContext)) {
                                directorySearcher.Filter = $"(userprincipalname={_DoDID}*)";
                                SearchResult result = directorySearcher.FindOne();
                                if (result is null) {
                                    sqlCommand.Parameters["@cac"].Value = DBNull.Value;
                                } else {
                                    DirectoryEntry ADUser = result.GetDirectoryEntry();
                                    sqlCommand.Parameters["@cac"].Value = $"M{ConvertEDIPIToBase32(_DoDID)}xxxxxxxx{ADUser.Properties["givenname"][0].ToString().ToNameCase(),-20}x{ADUser.Properties["sn"][0].ToString().ToNameCase(),-26}xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
                                }
                            }
                        }

                        sqlCommand.Parameters["@received"].Value = acceptence;
                        sqlCommand.Parameters["@tech"].Value = samaccountname;
                        try {
                            if (sqlCommand.ExecuteNonQuery() == 1) {
                                Program.eventLog.WriteEntry("Custody data written", System.Diagnostics.EventLogEntryType.Information, 4201);
                                MessageBox.Show("Acceptance processed!");
                                frmAcceptCustody frmAcceptCustody = new frmAcceptCustody();
                                frmAcceptCustody.Show();
                                this.Close();
                            } else {
                                MessageBox.Show($"Acceptance failed! Record acceptance manually and contact developer. \r\n {_SerialNumber} \r\n {_DoDID} \r\n {acceptence:d} \r\n {samaccountname}");
                            }
                        } catch (SqlException ex) {
                            if (ex.Number == 2601) {
                                MessageBox.Show("This EUD is already marked as in CSC Custody.");
                                return;
                            }
                            MessageBox.Show($"Acceptance failed! Record acceptance manually and contact developer. \r\n {_SerialNumber} \r\n {_DoDID} \r\n {acceptence:d} \r\n {samaccountname} \r\n {ex.Message} \r\n {ex.Number}");
                        }
                    }
                }
            } else {
                return;
            }
        }
    }
}
