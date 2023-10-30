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
                    ["Server"] = Program.settings.SQLServer,
                    ["Initial Catalog"] = Program.settings.Database,
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
                    ["Server"] = Program.settings.SQLServer,
                    ["Initial Catalog"] = Program.settings.Database,
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
                    SearchRoot = new DirectoryEntry(Program.settings.LDAPRoot)
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
                decommissionObjectToolStripMenuItem.Enabled = true;
                nukeGroupPolicyToolStripMenuItem.Enabled = true;
            }
            if (Program.IsAdmin && Program.IsDentac) {
                dENTACPostImageToolStripMenuItem.Visible = true;
                dENTACDecommToolStripMenuItem.Visible = true;
                dCVRemediateToolStripMenuItem.Visible = true;
            }
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("VisualStudioEdition")))
                decommissionObjectToolStripMenuItem.Enabled = true;
            enableObjectToolStripMenuItem.Text = IsEnabled() ? "Disable Object" : "Enable Object";
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
        private void dENTACPostImageToolStripMenuItem_Click(object sender, EventArgs e) {
            PrincipalContext context = new PrincipalContext(ContextType.Domain, "MHS");
            ComputerPrincipalEx computer = ComputerPrincipalEx.FindByIdentity(context, _selectedHostname);
            string currentDescription = computer.Description;
            DENTACDesc dENTACDesc = new DENTACDesc() {
                Description = currentDescription
            };
            dENTACDesc.ShowDialog();
            string newDesc = dENTACDesc.Description;
            dENTACDesc.Dispose();
            StringBuilder results = new StringBuilder();
            try {
                string targetDesktop = $@"\\{_selectedHostname}\c$\Users\Public\Desktop\";
                string[] cdaShortcuts = Directory.GetFiles(targetDesktop, "CDA*.url");
                foreach (string shortcut in cdaShortcuts) {
                    File.Delete(shortcut);
                }
                if (File.Exists($@"{targetDesktop}\CDA.lnk")) 
                    File.Delete($@"{targetDesktop}\CDA.lnk");
                foreach (string url in Directory.GetFiles(@"\\eamca700500\a$\1 Software\1 Imaging\2 Shortcuts for Desktops\", "*.url")) {
                    string targetFile = Path.Combine(targetDesktop, Path.GetFileName(url));
                    File.Copy(url, targetFile, true);
                }
                File.Copy(@"\\eamca700500\a$\1 Software\1 Imaging\2 Shortcuts for Desktops\XV.lnk", targetDesktop + "XV.lnk", true);
                File.Copy(@"\\eamca700500\a$\1 Software\1 Imaging\Select-DefaultPrinter.exe", $@"\\{_selectedHostname}\c$\AdminTools\Select-DefaultPrinter.exe", true);
                results.AppendLine("Desktop set.");
            } catch (UnauthorizedAccessException) {
                results.AppendLine("Could not set Desktop - insufficient access.");
            } catch (IOException) {
                results.AppendLine("Could not set Desktop - target offline");
            }
            try {
                if (newDesc == "")
                    newDesc = null;
                computer.Description = newDesc;
                computer.Save();
                results.AppendLine("Target AD object description set.");
            } catch {
                results.AppendLine("Could not update target description in AD.");
            }
            Regex regexMain = new Regex(@"(?<target>OU=(Desktops|Laptops),OU=(Clinical|Admin))");
            Regex regexDead = new Regex(@"OU=Dead Computers");
            Match matchMain = regexMain.Match(computer.DistinguishedName);
            Match matchDead = regexDead.Match(computer.DistinguishedName);
            bool cantMove = false;
            DirectoryEntry targetLoc = new DirectoryEntry();
            if (matchMain.Success) {
                try {
                    targetLoc = new DirectoryEntry(@"LDAP://" + matchMain.Groups["target"].Value + @",OU=DENTAC,OU=Dental,OU=Windows10,OU=Workstations,OU=EAMC,OU=DoD,DC=med,DC=ds,DC=osd,DC=mil");
                    DirectoryEntry currentObject = (DirectoryEntry)computer.GetUnderlyingObject();
                    currentObject.MoveTo(targetLoc);
                    currentObject.Close();
                    results.AppendLine("Target AD object moved to DENTAC OU.");
                } catch {
                    results.AppendLine("Could not move target AD object.");
                }
            } else if (matchDead.Success) {
                switch (computer.Name.Substring(4, 2)) {
                    case "NB":
                        targetLoc = new DirectoryEntry(@"LDAP://Laptops,OU=Clinical,OU=DENTAC,OU=Dental,OU=Windows10,OU=Workstations,OU=EAMC,OU=DoD,DC=med,DC=ds,DC=osd,DC=mil");
                        break;
                    case "WK":
                        targetLoc = new DirectoryEntry(@"LDAP://Desktops,OU=Clinical,OU=DENTAC,OU=Dental,OU=Windows10,OU=Workstations,OU=EAMC,OU=DoD,DC=med,DC=ds,DC=osd,DC=mil");
                        break;
                    default:
                        cantMove = true;
                        break;
                }
                if (!cantMove) {
                    try {
                        DirectoryEntry currentObject = (DirectoryEntry)computer.GetUnderlyingObject();
                        currentObject.MoveTo(targetLoc);
                        currentObject.Close();
                        computer.Enable();
                        results.AppendLine("Target AD object moved to DENTAC OU.");
                    } catch {
                        results.AppendLine("Could not move target AD object.");
                    }
                } else {
                    results.AppendLine("Computer not moved.  Currently in Dead Computers, could not determine correct target OU");
                }
            } else {
                results.AppendLine("Could not move target to DENTAC OU - does not exist in a supported source");
            }
            targetLoc.Close();
            MessageBox.Show(results.ToString());
            btnSearch.PerformClick();
        }
        private void dENTACDecommToolStripMenuItem_Click(object sender, EventArgs e) {
            Extensions.DecomComputer(_selectedHostname);
            string dcvPath = $@"\\{Properties.Resources.DCVServer}\{Properties.Resources.DCVRootFolder}\Configs";
            string archivePath = $@"\\{Properties.Resources.DCVServer}\{Properties.Resources.DCVRootFolder}\ArchivedConfigs";
            DirectoryInfo dcvInfo = new DirectoryInfo(dcvPath);
            foreach (FileInfo config in dcvInfo.GetFiles($"{_selectedHostname}*.*")) {
                try {
                    config.MoveTo($@"{archivePath}\{config.Name}");
                } catch {
                    try {
                        config.Delete();
                    } catch {
                        MessageBox.Show($"Failed to archive or remove DCV licenses files for {_selectedHostname} at {dcvPath}");
                    }
                }
            }
            btnSearch.PerformClick();
        }

        private void decommissionObjectToolStripMenuItem_Click(object sender, EventArgs e) {
            Extensions.DecomComputer(_selectedHostname);
        }

        private void dCVRemediateToolStripMenuItem_Click(object sender, EventArgs e) {
            if (DCVUtil.RemediateConfig(_selectedHostname) == DCVUtil.Result.Success) {
                System.Media.SystemSounds.Beep.Play();
            } else {
                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        private void RecursiveDelete(DirectoryInfo baseDir) {
            if (!baseDir.Exists)
                return;
            foreach (var dir in baseDir.EnumerateDirectories()) {
                RecursiveDelete(dir);
            }

            try {
                baseDir.Delete(true);
            }
            catch { }
        }

        private void UpdateGPO() {
            try {
                var connectionOptions = new ConnectionOptions {
                    Impersonation = ImpersonationLevel.Impersonate
                };
                var scope = new ManagementScope($@"\\{_selectedHostname}\root\cimv2", connectionOptions);
                scope.Connect();
                var managementClass = new ManagementClass(scope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
                using (var inparams = managementClass.GetMethodParameters("Create")) {
                    inparams["CommandLine"] = "GPUpdate /force";
                    _ = managementClass.InvokeMethod("Create", inparams, null);
                }
            }
            catch (Exception ex) { }
        }
        private void nukeGroupPolicyToolStripMenuItem_Click(object sender, EventArgs e) {
            RecursiveDelete(new DirectoryInfo($@"\\{_selectedHostname}\c$\ProgramData\Microsoft\Group Policy\History"));
            File.Delete($@"\\{_selectedHostname}\c$\Windows\System32\GroupPolicy\Machine\Registry.pol");
            File.Delete($@"\\{_selectedHostname}\c$\Windows\System32\GroupPolicy\User\Registry.pol");
            File.Delete($@"\\{_selectedHostname}\c$\Windows\System32\GroupPolicy\gpt.ini");
            UpdateGPO();
        }
    }
}
