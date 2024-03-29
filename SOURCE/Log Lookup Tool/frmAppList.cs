﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log_Lookup_Tool {
    public partial class frmAppList : Form {
        private string hostname;

        public void SetValue(string Query) {
            hostname = Query;
        }
        public frmAppList() {
            InitializeComponent();
        }

        private void frmAppList_Load(object sender, EventArgs e) {
            EUDLoggingDataSet.ApplicationsDataTable table = applicationsTableAdapter.GetData(hostname);
            Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
            Program.eventLog.WriteEntry($"Full application list for {hostname}", System.Diagnostics.EventLogEntryType.Information, 4101);
            dgvAppList.DataSource = table;
            Text += hostname;
        }

        private void btnExport_Click(object sender, EventArgs e) {
            if (dgvAppList.Rows.Count > 0) {
                SaveFileDialog sfd = new SaveFileDialog {
                    Filter = "CSV (*.csv)|*.csv",
                    FileName = $"{hostname} Application List.csv"
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
                        StringBuilder output = new StringBuilder();
                        try {
                            int columnCount = dgvAppList.ColumnCount;                    
                            var headers = dgvAppList.Columns.Cast<DataGridViewColumn>();
                            output.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));
                            foreach (DataGridViewRow row in dgvAppList.Rows) {
                                var cells = row.Cells.Cast<DataGridViewCell>();
                                output.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
                            }
                        } catch {
                            MessageBox.Show($"Error generating dataset to write.");
                        }
                        File.WriteAllText(sfd.FileName, output.ToString(), Encoding.UTF8);
                    }
                }
            }
        }
    }
}
