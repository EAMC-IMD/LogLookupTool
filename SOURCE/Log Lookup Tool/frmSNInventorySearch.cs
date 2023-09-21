using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log_Lookup_Tool {
    public partial class frmSNInventorySearch : Form {
        System.Media.SoundPlayer sound = new System.Media.SoundPlayer(Properties.Resources.Jeopardy_Music);
        public frmSNInventorySearch() {
            InitializeComponent();
        }

        private void btnGenerateCSV_Click(object sender, EventArgs e) {
            txtEcnList.Text = txtEcnList.Text.Replace(System.Environment.NewLine, ",");
            txtEcnList.Text = Regex.Replace(txtEcnList.Text, @"\s+", "");
            MessageBox.Show("Export beginning.  Application may be non-responsive for up to 45 seconds, depending on the dataset requested.");
            sound.Play();
            EUDLoggingDataSet.InventoryDataDataTable table =  inventoryDataTableAdapter.GetData(txtEcnList.Text);
            Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
            Program.eventLog.WriteEntry($"Inventory export", System.Diagnostics.EventLogEntryType.Information, 4101);

            if (table != null && table.Rows.Count == 0) {
                sound.Stop();
                MessageBox.Show("No records found with that criteria.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog {
                Filter = "Excel Worksheet (*.xlsx)|*.xlsx",
                FileName = $"Inventory List.xlsx"
            };
            bool fileError = false;
            if (sfd.ShowDialog() == DialogResult.OK) {
                if (File.Exists(sfd.FileName)) {
                    try {
                        File.Delete(sfd.FileName);
                    } catch (IOException ex) {
                        fileError = true;
                        sound.Stop();
                        MessageBox.Show($"Error writing file. {ex.Message}");
                    }
                }
                if (!fileError) {
                    table.ExportToExcel(sfd.FileName);
                    sound.Stop();
                    MessageBox.Show("Export complete!");
                }
            }
            sound.Stop();
        }

        private void btnGenerateByECN_Click(object sender, EventArgs e) {
            txtEcnList.Text = txtEcnList.Text.Replace(System.Environment.NewLine, ",");
            txtEcnList.Text = Regex.Replace(txtEcnList.Text, @"\s+", "");
            sound.Play();
            EUDLoggingDataSet.InventoryDataByECNDataTable table = inventoryDataByECNTableAdapter.GetData(txtEcnList.Text);
            Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
            Program.eventLog.WriteEntry($"Inventory export", System.Diagnostics.EventLogEntryType.Information, 4101);
            if (table != null && table.Rows.Count == 0) {
                MessageBox.Show("No records found with that criteria.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog {
                Filter = "Excel Worksheet (*.xlsx)|*.xlsx",
                FileName = $"Inventory List.xlsx"
            };
            bool fileError = false;
            if (sfd.ShowDialog() == DialogResult.OK) {
                if (File.Exists(sfd.FileName)) {
                    try {
                        File.Delete(sfd.FileName);
                    } catch (IOException ex) {
                        sound.Stop();
                        fileError = true;
                        MessageBox.Show($"Error writing file. {ex.Message}");
                    }
                }
                if (!fileError) {
                    table.ExportToExcel(sfd.FileName);
                    sound.Stop();
                }
            }
            sound.Stop();
        }
    }
}
