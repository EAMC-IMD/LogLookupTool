using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CAC;

namespace Log_Lookup_Tool {
    public partial class frmInCustody : Form {
        public frmInCustody() {
            InitializeComponent();
        }

        private void frmInCustody_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'eUDCustody.Custody' table. You can move, or remove it, as needed.
            this.custodyTableAdapter.FillInCustody(this.eUDCustody.Custody);
            Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
            Program.eventLog.WriteEntry($"EUD in CSC Custody Data Retreived", System.Diagnostics.EventLogEntryType.Information, 4101);
            dataGridView.DataSource = eUDCustody.Custody;
            dataGridView.Refresh();
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            bool needsADLookup = false;
            if (e.ColumnIndex != dataGridView.Columns["CustomerName"].Index)
                return;
            if (String.IsNullOrEmpty((string)dataGridView.Rows[e.RowIndex].Cells["DoDID"].Value))
                return;
            if (dataGridView.Rows[e.RowIndex].Cells["fullScanDataGridViewTextBoxColumn"].Value == DBNull.Value)
                needsADLookup = true;
            if (!needsADLookup && String.IsNullOrEmpty((string)dataGridView.Rows[e.RowIndex].Cells["fullScanDataGridViewTextBoxColumn"].Value))
                needsADLookup = true;
            Scan scan = new Scan(dataGridView.Rows[e.RowIndex].Cells["fullScanDataGridViewTextBoxColumn"].Value.ToString());
            if (scan.Surname is null)
                needsADLookup = true;
            if (needsADLookup) {
                string dodid = dataGridView.Rows[e.RowIndex].Cells["DoDID"].Value.ToString();
                DirectoryEntry rootDSE = new DirectoryEntry(Program.settings.LDAPRoot);
                var defaultNamingContext = rootDSE.Properties["defaultNamingContext"].Value;
                using (DirectorySearcher directorySearcher = new DirectorySearcher(@"LDAP://" + defaultNamingContext)) {
                    directorySearcher.Filter = $"(userprincipalname={dodid}*)";
                    SearchResult result = directorySearcher.FindOne();
                    if (result is null) {
                        return;
                    }
                    DirectoryEntry ADUser = result.GetDirectoryEntry();
                    string name = $"{ADUser.Properties["sn"][0].ToString().ToNameCase()}, {ADUser.Properties["givenname"][0].ToString().ToNameCase()}";
                    dataGridView.Rows[e.RowIndex].Cells["CustomerName"].Value = name;
                }
            } else {
                dataGridView.Rows[e.RowIndex].Cells["CustomerName"].Value = $"{scan.Surname.ToNameCase()}, {scan.FirstName.ToNameCase()}";
            }
        }
    }
}
