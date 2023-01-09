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

namespace EAMC_Log_Lookup_Tool {
    public partial class frmSNInventorySearch : Form {
        public frmSNInventorySearch() {
            InitializeComponent();
        }

        private void btnGenerateCSV_Click(object sender, EventArgs e) {
            txtEcnList.Text = txtEcnList.Text.Replace(System.Environment.NewLine, ",");
            txtEcnList.Text = Regex.Replace(txtEcnList.Text, @"\s+", "");

            EUDLoggingDataSet.InventoryDataDataTable table =  inventoryDataTableAdapter.GetData(txtEcnList.Text);

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
                        fileError = true;
                        MessageBox.Show($"Error writing file. {ex.Message}");
                    }
                }
                if (!fileError) {
                    table.ExportToExcel(sfd.FileName);
                }
            }
        }

        private void btnGenerateByECN_Click(object sender, EventArgs e) {
            txtEcnList.Text = txtEcnList.Text.Replace(System.Environment.NewLine, ",");
            txtEcnList.Text = Regex.Replace(txtEcnList.Text, @"\s+", "");

            EUDLoggingDataSet.InventoryDataByECNDataTable table = inventoryDataByECNTableAdapter.GetData(txtEcnList.Text);

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
                        fileError = true;
                        MessageBox.Show($"Error writing file. {ex.Message}");
                    }
                }
                if (!fileError) {
                    table.ExportToExcel(sfd.FileName);
                }
            }
        }
    }
}
