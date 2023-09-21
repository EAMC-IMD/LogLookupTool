using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Log_Lookup_Tool
{
    public partial class frmMain : Form
    {
        private int flashCount = 0;
        readonly System.Media.SoundPlayer sound = new System.Media.SoundPlayer(Properties.Resources.Jeopardy_Music);
        public frmMain()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        const uint WM_QUERYENDSESSION = 0x0011;
        const uint WM_ENDSESSION = 0x0016;

        protected override void WndProc(ref Message m) {
            if (m.Msg == WM_QUERYENDSESSION) {
                Program.RemoveLockFile(Program.LockFile);
            } else if (m.Msg == WM_ENDSESSION) {
                Program.RemoveLockFile(Program.LockFile);
                Program.eventLog.WriteEntry("Log Lookup Tool exit.", System.Diagnostics.EventLogEntryType.Information, 5005);
                Application.Exit();
            }
            base.WndProc(ref m);
        }

        private void btnUser_Click(object sender, EventArgs e) {
            frmUserSearch frmUserSearch = new frmUserSearch();
            frmUserSearch.Show();
        }

        private void btnEUD_Click(object sender, EventArgs e) {
            frmComputerSearch frmComputerSearch = new frmComputerSearch();
            frmComputerSearch.Show();
        }

        private void frmMain_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'eUDLoggingDataSet.vwHardwareList' table. You can move, or remove it, as needed.
            this.vwHardwareListTableAdapter.Fill(this.eUDLoggingDataSet.vwHardwareList);
            Text = $"Log Lookup Tool - {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version} - {Program.UserName}";
            if (Program.IsAdmin)
                btnElevate.Enabled = false;
        }

        private void btnExportHardwareList_Click(object sender, EventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"Full Hardware List.xlsx"
            };
            bool fileError = false;
            if (sfd.ShowDialog() == DialogResult.OK) {
                if (File.Exists(sfd.FileName)) {
                    try {
                        File.Delete(sfd.FileName);
                    } catch (IOException ex) {
                        fileError = true;
                        MessageBox.Show($"Error writing file. {ex.Message}");
                    }
                }
                if (!fileError) {
                    sound.Play();
                    MessageBox.Show("One moment while the file is saved");
                    Program.eventLog.WriteEntry("Full hardware list export", System.Diagnostics.EventLogEntryType.Information, 4101);
                    this.vwHardwareListTableAdapter.GetData().ExportToExcel(sfd.FileName);
                    sound.Stop();
                    MessageBox.Show("Export complete!");
                } else {
                    MessageBox.Show("One moment while the sheet is generated.");
                    this.vwHardwareListTableAdapter.GetData().ExportToExcel(null);
                }
            } else {
                MessageBox.Show("One moment while the sheet is generated.");
                this.vwHardwareListTableAdapter.GetData().ExportToExcel(null);
            }
            
        }

        private void btnExportInventoryData_Click(object sender, EventArgs e) {
            frmSNInventorySearch frmSNInventorySearch = new frmSNInventorySearch();
            frmSNInventorySearch.Show();
        }

        private void IPLookup(string ip) {
            string query = @"SELECT (
                            SELECT VLAN FROM VLANs WHERE dbo.IPAddressIsInRange(@ip, [VLAN_CIDR])=1
                        ) VLAN, (
                            SELECT VLAN_Desc FROM VLANs WHERE dbo.IPAddressIsInRange(@ip, [VLAN_CIDR]) = 1
                        ) VLAN_Desc
                        ";
            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder {
                ["Server"] = Properties.Resources.SQLServer,
                ["Initial Catalog"] = Properties.Resources.Database,
                ["Integrated Security"] = true
            };
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString());
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand {
                Connection = sqlConnection,
                CommandText = query
            };
            sqlCommand.Parameters.Add("@ip", SqlDbType.VarChar);
            sqlCommand.Parameters["@ip"].Value = ip;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Program.eventLog.WriteEntry("VLAN lookup", System.Diagnostics.EventLogEntryType.Information, 4101);
            StringBuilder result = new StringBuilder();
            if (reader.HasRows) {
                reader.Read();
                result.AppendLine($"IP Address: {ip}");
                result.AppendLine($"VLAN: {reader["VLAN"]}");
                result.AppendLine($"VLAN Desc: {reader["VLAN_Desc"]}");
            } else {
                result.AppendLine("No VLAN entry with a matching CIDR exists in the database.");
			}
            txtIPResult.Text = result.ToString();
        }

		private void btnIPLookup_Click(object sender, EventArgs e) {
            if (!Regex.IsMatch(txtIP.Text, @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")) {
                timer1.Enabled = true;
                return;
			}
            IPLookup(txtIP.Text);
            return;
		}

		private void txtIP_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar != (char)13) {
                return;
			}
            if (!Regex.IsMatch(txtIP.Text, @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")) {
                timer1.Enabled = true;
                return;
            }
            e.Handled = true;
            IPLookup(txtIP.Text);
            return;
        }

		private void timer1_Tick(object sender, EventArgs e) {
            if (flashCount >= 20) {
                timer1.Enabled = false;
                flashCount = 0;
                txtIP.BackColor = SystemColors.Control;
                return;
			}
            if (txtIP.BackColor == SystemColors.Control) {
                txtIP.BackColor = Color.Red;
			} else {
                txtIP.BackColor = SystemColors.Control;
			}
            flashCount++;
		}

        private void btnAccept_Click(object sender, EventArgs e) {
            frmAcceptCustody frmAcceptCustody = new frmAcceptCustody();
            frmAcceptCustody.Show();
        }

        private void btnCustodyReport_Click(object sender, EventArgs e) {
            frmInCustody frmInCustody = new frmInCustody();
            frmInCustody.Show();
        }

        private void btnReturn_Click(object sender, EventArgs e) {
            frmReturnLaptop frmReturnLaptop = new frmReturnLaptop();
            frmReturnLaptop.Show();
        }

        private void btnElevate_Click(object sender, EventArgs e) {
            if (Program.IsAdmin)
                return;
            Program.RestartAsAdmin();
        }

        private void btnIIssue_Click(object sender, EventArgs e) {
            frmIAIssue frmIAIssue = new frmIAIssue();
            frmIAIssue.Show();
        }

        private void btnIReturn_Click(object sender, EventArgs e) {
            frmIAReturn frmIAReturn = new frmIAReturn();
            frmIAReturn.Show();
        }

        private void btnBatch_Click(object sender, EventArgs e) {
            frmIAccessBatch frmIAccessBatch = new frmIAccessBatch();
            frmIAccessBatch.Show();
        }
    }
}
