using System;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace Log_Lookup_Tool {
    public partial class frmComputerSearch : Form {

        string preLoad = null;
        private string selectedHostname = null;
        private string selectedECN = null;
        public frmComputerSearch() {
            InitializeComponent();
        }

        public void PreLoadSearchValue (string Hostname) {
            this.preLoad = Hostname;
        }

        private bool IsEnabled() {
            PrincipalContext context = new PrincipalContext(ContextType.Domain, "MHS");
            ComputerPrincipalEx computer = ComputerPrincipalEx.FindByIdentity(context, selectedHostname);
            return computer.IsEnabled();
        }

        private string GetHostnameFromDB(string Query) {
            string sql;
            string result = null;
            if (Regex.IsMatch(Query, @"\d?\d{5}$")) {
                sql = @"SELECT [dbo].SNfromECN(@ecn) AS hostname";
                SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder {
                    ["Server"] = Properties.Resources.SQLServer,
                    ["Initial Catalog"] = Properties.Resources.Database,
                    ["Integrated Security"] = true
                };
                SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString());
                sqlConnection.Open();
                Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                SqlCommand sqlCommand = new SqlCommand {
                    Connection = sqlConnection,
                    CommandText = sql
                };
                sqlCommand.Parameters.Add("@ecn", SqlDbType.VarChar);
                sqlCommand.Parameters["@ecn"].Value = Query;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                Program.eventLog.WriteEntry("SN lookup", System.Diagnostics.EventLogEntryType.Information, 4101);
                if (reader.HasRows) {
                    while (reader.Read()) {
                        if (reader.GetValue(0).ToString() == "log error") {
                            MessageBox.Show("A SN was retrieved from the DMLSS snapshot, but no log entry was found associated with it.");
                            return null;
                        } else if (reader.GetValue(0).ToString() == "DMLSS error") {
                            MessageBox.Show("DMLSS snapshot does not contain that ECN. Please try the search again with the serial number.");
                            return null;
                        } else {
                            result = (string)reader.GetValue(0);
                        }
                    }
                    reader.Close();
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    return result;
                } else {
                    MessageBox.Show("An uncaught exception occured attempting to resolve this ECN to a SN.");
                    return null;
                }
            } else {
                sql = @"SELECT computername FROM EUDStatData WHERE SerialNumber LIKE '%'+@SN";
                SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder {
                    ["Server"] = Properties.Resources.SQLServer,
                    ["Initial Catalog"] = Properties.Resources.Database,
                    ["Integrated Security"] = true
                };
                SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString());
                sqlConnection.Open();
                Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                SqlCommand sqlCommand = new SqlCommand {
                    Connection = sqlConnection,
                    CommandText = sql
                };
                sqlCommand.Parameters.Add("@SN", SqlDbType.VarChar);
                sqlCommand.Parameters["@SN"].Value = Query.Length <= 7 ? Query : Query.Substring(Query.Length - 7);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                Program.eventLog.WriteEntry("SN lookup", System.Diagnostics.EventLogEntryType.Information, 4101);
                if (reader.HasRows) {
                    while (reader.Read()) {
                        if (reader.GetValue(0) == DBNull.Value) {
                            MessageBox.Show("No log data was found matching that serial number");
                            return null;
                        }
                        result = (string)reader.GetValue(0);
                    }
                    reader.Close();
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    return result;
                } else {
                    return null;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            btnAD.Enabled = true;
            btnAppList.Enabled = true;
            btnLogs.Enabled = true;
            string query = tbxSearch.Text.ToUpperInvariant();
            if (query.Length < 4) { return; }
            QueryTypes queryTypes = Query.CheckType(query);

            switch (queryTypes) {
                case QueryTypes.Name:
                case QueryTypes.UPN:
                case QueryTypes.DoDID:
                    MessageBox.Show(
                        "Entered query not detected as Hostname or ECN.",
                        "Query Parse Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Hand);
                    return;
                case QueryTypes.sAMAccountName:
                    query = GetHostnameFromDB(query);
                    queryTypes = QueryTypes.SerialNumber;
                    break;
                case QueryTypes.ECN:
                    query = GetHostnameFromDB(query);
                    break;
                case QueryTypes.Hostname:
                    break;
                default:
                    MessageBox.Show(
                        "Entered query not detected as Hostname or ECN.",
                        "Query Parse Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Hand);
                    return;
            }
            if (query is null && (queryTypes == QueryTypes.ECN || queryTypes == QueryTypes.SerialNumber)) {
                btnAD.Enabled = false;
                btnAppList.Enabled = false;
                btnLogs.Enabled = false;
                btnHardwareInfo.Enabled = true;
                DataTable table = new DataTable();
                table.Columns.Add("Results", typeof(int));
                if (queryTypes == QueryTypes.ECN) {
                    table.Columns.Add("ECN", typeof(string));
                } else {
                    table.Columns.Add("SN", typeof(string));
                }
                table.Columns.Add("Warning", typeof(string));
                table.Rows.Add(0, tbxSearch.Text.ToUpperInvariant(), "No logs found for this entry. You can try looking at EUD Hardware Info to view DMLSS data, if available.");
                dgvResults.DataSource = table;
                dgvResults.Rows[0].Cells[0].Selected = true;
                dgvResults.CurrentCell = dgvResults[0, 0];
                DataGridViewCellEventArgs arg = new DataGridViewCellEventArgs(0, 0);
                dgvResults_CellClick(dgvResults, arg);
            } else {
                selectedHostname = query;
                DirectorySearcher search = new DirectorySearcher {
                    SearchRoot = new DirectoryEntry(Properties.Resources.LDAPRoot)
                };
                search.PropertiesToLoad.Add("name");
                search.PropertiesToLoad.Add("description");
                search.PropertiesToLoad.Add("distinguishedname");
                search.Filter = String.Format("(name={0})", query);
                SearchResultCollection resultCollection = search.FindAll();
                if (resultCollection.Count > 0)
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("Hostname", typeof(string));
                    table.Columns.Add("Description", typeof(string));
                    table.Columns.Add("Distinguished Name", typeof(string));
                    foreach (SearchResult thisResult in resultCollection)
                    {
                        if (thisResult.Properties["description"].Count == 0)
                        {
                            table.Rows.Add(thisResult.Properties["name"][0], "", thisResult.Properties["distinguishedname"][0]);
                        }
                        else
                        {
                            table.Rows.Add(thisResult.Properties["name"][0], thisResult.Properties["description"][0], thisResult.Properties["distinguishedname"][0]);
                        }
                    }
                    dgvResults.DataSource = table;
                    btnAD.Enabled = false;
                    btnLogs.Enabled = false;
                    btnAppList.Enabled = false;
                    btnHardwareInfo.Enabled = false;
                    if (table.Rows.Count == 1)
                    {
                        dgvResults.Rows[0].Cells[0].Selected = true;
                        dgvResults.CurrentCell = dgvResults[0, 0];
                        DataGridViewCellEventArgs arg = new DataGridViewCellEventArgs(0, 0);
                        dgvResults_CellClick(dgvResults, arg);
                    }
                } else
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("Hostname", typeof(string));
                    table.Columns.Add("Warning", typeof(string));
                    table.Rows.Add(tbxSearch.Text.ToUpperInvariant(), "This machine is not in Active Directory. Logs and/or DMLSS data may be available.");
                    dgvResults.DataSource = table;
                    btnAD.Enabled = false;
                    btnLogs.Enabled = true;
                    btnAppList.Enabled = true;
                    btnHardwareInfo.Enabled = true;
                    dgvResults.Rows[0].Cells[0].Selected = true;
                    dgvResults.CurrentCell = dgvResults[0, 0];
                    DataGridViewCellEventArgs arg = new DataGridViewCellEventArgs(0, 0);
                    dgvResults_CellClick(dgvResults, arg);
                }
            }
        }

        private void dgvResults_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex == -1) { return; }
            DataGridViewCell cell = dgvResults.Rows[e.RowIndex].Cells[0];
            if (cell.Value.ToString() == "0" || Regex.IsMatch(cell.Value.ToString(), @"^\d{5,6}$")) {
                selectedECN = cell.Value.ToString();
            } else {
                selectedHostname = cell.Value.ToString();
                btnAD.Enabled = true;
                btnLogs.Enabled = true;
                btnAppList.Enabled = true;
                btnHardwareInfo.Enabled = true;
            }
        }

        private void btnLogs_Click(object sender, EventArgs e) {
            frmShowLogs frmShowLogs = new frmShowLogs();
            frmShowLogs.SetValues(QueryTypes.Hostname, this.selectedHostname);
            frmShowLogs.Show();
        }

        private void frmComputerSearch_Shown(object sender, EventArgs e) {
            if (preLoad is null) {
                return;
            }
            tbxSearch.Text = preLoad;
            this.btnSearch.PerformClick();
        }

        private void btnAD_Click(object sender, EventArgs e) {
            frmDisplayUserAD frmDisplayUserAD = new frmDisplayUserAD();
            frmDisplayUserAD.SetValues(QueryTypes.Hostname, this.selectedHostname);
            frmDisplayUserAD.Show();
        }

        private void btnAppList_Click(object sender, EventArgs e) {
            frmAppList frmAppList = new frmAppList();
            frmAppList.SetValue(this.selectedHostname);
            frmAppList.Show();
        }

        private void btnHardwareInfo_Click(object sender, EventArgs e) {
            frmEUDStatInfo frmEUDStatInfo = new frmEUDStatInfo();
            if (selectedECN is null) {
                frmEUDStatInfo.SetHostname(selectedHostname);
            } else {
                frmEUDStatInfo.SetEcn(selectedECN);
                frmEUDStatInfo.SetHostname(selectedHostname);
            }
            frmEUDStatInfo.Show();
        }

        private void dgvResults_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e) {
            if (e.RowIndex == -1 || e.ColumnIndex != 0) {
                return;
            }
            DataGridViewCell cell = dgvResults.Rows[e.RowIndex].Cells[0];
            this.selectedHostname = cell.Value.ToString();
            if (Program.IsAdmin) {
                enableObjectToolStripMenuItem.Enabled = true;
                deleteObjectToolStripMenuItem.Enabled = true;
            }
            if (IsEnabled()) {
                enableObjectToolStripMenuItem.Text = "Disable Object";
            } else {
                enableObjectToolStripMenuItem.Text = "Enable Object";
            }
            e.ContextMenuStrip = contextMenuStrip1;
        }

        private void pingToolStripMenuItem_Click(object sender, EventArgs e) {
            if (selectedHostname == null) {
                return;
            }
            using (System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping()) {
                System.Net.NetworkInformation.PingReply reply = p.Send(selectedHostname);
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success) {
                    Program.eventLog.WriteEntry($"ICMP echo to {reply.Address}", System.Diagnostics.EventLogEntryType.Information, 4001);
                    MessageBox.Show($"ICMP echo to {selectedHostname} succeeded.");
                } else {
                    MessageBox.Show($"ICMP echo to {selectedHostname} failed. Status code: {reply.Status}");
                }
            }
        }

        private void jumpToToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!System.IO.File.Exists(@"C:\Program Files\Bomgar\Representative Console\chat.gsc.health.mil\bomgar-rep.exe")) {
                MessageBox.Show("Bomgar not detected at static path.  Jump To failed.");
                return;
            }
            System.Diagnostics.ProcessStartInfo process = new System.Diagnostics.ProcessStartInfo() {
                FileName = @"C:\Program Files\Bomgar\Representative Console\chat.gsc.health.mil\bomgar-rep.exe",
                WorkingDirectory = @"C:\Program Files\Bomgar\Representative Console\chat.gsc.health.mil\",
                ErrorDialog = true,
                UseShellExecute = true,
                Verb = "runas"
            };
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)) {
                process.Verb = null;
            }
            process.Arguments = $@"--run-script ""type=rep&operation=generate&action=push_and_start_local&hostname={selectedHostname}""";
            System.Diagnostics.Process.Start(process);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            Clipboard.SetText(selectedHostname);
        }

        private void enableObjectToolStripMenuItem_Click(object sender, EventArgs e) {
            PrincipalContext context = new PrincipalContext(ContextType.Domain, "MHS");
            ComputerPrincipalEx computer = ComputerPrincipalEx.FindByIdentity(context, selectedHostname);
            if (enableObjectToolStripMenuItem.Text == "Enable Object") {
                computer.Enable();
            } else {
                computer.Disable();
            }
        }

        private void deleteObjectToolStripMenuItem_Click(object sender, EventArgs e) {
            PrincipalContext context = new PrincipalContext(ContextType.Domain, "MHS");
            ComputerPrincipalEx computer = ComputerPrincipalEx.FindByIdentity(context, selectedHostname);
            try {
                computer.Delete();
            } catch {
                ((DirectoryEntry)computer.GetUnderlyingObject()).DeleteTree();
            } 
        }
    }
}
