using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMC_Log_Lookup_Tool
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnUser_Click(object sender, EventArgs e) {
            frmUserSearch frmUserSearch = new frmUserSearch();
            frmUserSearch.ShowDialog();
        }

        private void btnEUD_Click(object sender, EventArgs e) {
            frmComputerSearch frmComputerSearch = new frmComputerSearch();
            frmComputerSearch.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'eUDLoggingDataSet.vwHardwareList' table. You can move, or remove it, as needed.
            this.vwHardwareListTableAdapter.Fill(this.eUDLoggingDataSet.vwHardwareList);

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
                    MessageBox.Show("One moment while the file is saved");
                    this.vwHardwareListTableAdapter.GetData().ExportToExcel(sfd.FileName);
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
            frmSNInventorySearch.ShowDialog();
        }
    }
}
