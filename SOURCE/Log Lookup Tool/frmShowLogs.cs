using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log_Lookup_Tool {
    public partial class frmShowLogs : Form {
        private QueryTypes queryType;
        private string query;
        private string selectedHostname = null;
        public frmShowLogs() {
            InitializeComponent();
        }

        public void SetValues (QueryTypes queryTypes, string key) {
            this.queryType = queryTypes;
            this.query = key;
        }

        private void frmShowLogs_Load(object sender, EventArgs e) {
            RefreshData();
            timer1.Enabled = true;
        }

        private void RefreshData() {
            if (this.queryType == QueryTypes.sAMAccountName) {
                EUDLoggingDataSet.vwLoggingDataTable table = vwLoggingTableAdapter.GetDataByUsername(query);
                dgvLogs.DataSource = table;
            } else if (this.queryType == QueryTypes.Hostname) {
                EUDLoggingDataSet.vwLoggingDataTable table = vwLoggingTableAdapter.GetDataByComputer(query);
                dgvLogs.DataSource = table;
            }
            Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
            Program.eventLog.WriteEntry($"Login logs retreived for {query}", System.Diagnostics.EventLogEntryType.Information, 4101);
            dgvLogs.Refresh();
        }

        private void dgvLogs_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex == -1) { return; }
            DataGridViewCell cell;
            if (e.ColumnIndex == 0) {
                cell = dgvLogs.Rows[e.RowIndex].Cells[0];
                frmComputerSearch frmComputerSearch = new frmComputerSearch();
                frmComputerSearch.PreLoadSearchValue(cell.Value.ToString());
                frmComputerSearch.Show();
            } else if (e.ColumnIndex == 1) {
                cell = dgvLogs.Rows[e.RowIndex].Cells[1];
                frmUserSearch frmUserSearch = new frmUserSearch();
                frmUserSearch.PreLoadSearchValue(cell.Value.ToString());
                frmUserSearch.Show();
            }
        }

		private void timer1_Tick(object sender, EventArgs e) {
            RefreshData();
		}

        private void dgvLogs_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e) {
            if (e.RowIndex == -1 || e.ColumnIndex != 0) {
                return;
            }
            DataGridViewCell cell = dgvLogs.Rows[e.RowIndex].Cells[0];
            selectedHostname = cell.Value.ToString();
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
            try {
                Clipboard.SetDataObject(selectedHostname, true, 10, 100);
            } catch (Exception) {

            }
        }

        private void dgvLogs_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.C && e.Control) {
                try {
                    Clipboard.SetDataObject(selectedHostname, true, 10, 100);
                } catch (Exception) {

                }
            }
        }
    }
}
