using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using System.Data.SqlClient;

namespace Log_Lookup_Tool
{
    public partial class frmUserSearch : Form {

        string preLoad = null;
        private string selectedSamAccountName = null;
        private string edipi;
        public frmUserSearch() {
            InitializeComponent();
        }

        public void PreLoadSearchValue(string Samaccountname) {
            this.preLoad = Samaccountname;
        }

        private string GetSamAccountNameFromDB(string Query, QueryTypes queryType) {
            string sql;
            string result = null;
            switch (queryType) {
                case QueryTypes.DoDID:
                    sql = @"SELECT TOP(1) loggedOnUserSAM FROM EUDLoginData WHERE loggedOnUserUPN LIKE @upn+'%' ORDER BY logonTimestamp DESC";
                    break;
                case QueryTypes.UPN:
                    sql = @"SELECT TOP(1) loggedOnUserSAM FROM EUDLoginData WHERE loggedOnUserUPN=@upn ORDER BY logonTimestamp DESC";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid query type.");
            }
            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder {
                ["Server"] = Properties.Resources.SQLServer,
                ["Initial Catalog"] = Properties.Resources.Database,
                ["Integrated Security"] = true
            };
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString())) {
                sqlConnection.Open();
                Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                using (SqlCommand sqlCommand = new SqlCommand {
                    Connection = sqlConnection,
                    CommandText = sql
                }) {
                    sqlCommand.Parameters.Add("@upn", SqlDbType.VarChar);
                    sqlCommand.Parameters["@upn"].Value = Query;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    Program.eventLog.WriteEntry($"UPN to SAM lookup", System.Diagnostics.EventLogEntryType.Information, 4101);
                    while (reader.Read()) {
                        result = (string)reader.GetValue(0);
                    }
                    reader.Close();
                }
            }
            return result;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = tbxSearch.Text;
            if (query.Length < 4) { return; }
            QueryTypes queryTypes = Query.CheckType(query);

            switch (queryTypes) {
                case QueryTypes.sAMAccountName:
                case QueryTypes.Name:
                    break;
                case QueryTypes.UPN:
                case QueryTypes.DoDID:
                    query = GetSamAccountNameFromDB(query, queryTypes);
                    break;
                default:
                    MessageBox.Show(
                        "Entered query not detected as sAMAccountName, UPN, DoDID or user name (Last, First).",
                        "Query Parse Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Hand);
                    return;
            }

            DirectorySearcher search = new DirectorySearcher {
                SearchRoot = new DirectoryEntry(Properties.Resources.LDAPRoot)
            };
            search.PropertiesToLoad.Add("displayname");
            search.PropertiesToLoad.Add("samaccountname");
            search.PropertiesToLoad.Add("userprincipalname");
            SearchResultCollection resultCollection;
            DataTable table = new DataTable();
            table.Columns.Add("Display Name", typeof(string));
            table.Columns.Add("sAMAccountName", typeof(string));
            table.Columns.Add("UserPrincipalName", typeof(string));
            search.Filter = String.Format("(anr={0})", query);
            resultCollection = search.FindAll();
            if (resultCollection.Count == 0) {
                search.SearchRoot = new DirectoryEntry("LDAP://DC=med,DC=ds,DC=osd,DC=mil");
                resultCollection = search.FindAll();
            }
            foreach (SearchResult thisResult in resultCollection) {
                table.Rows.Add(thisResult.Properties["displayname"][0], thisResult.Properties["samaccountname"][0], thisResult.Properties["userprincipalname"][0]);
            }
            dgvResults.DataSource = table;
            btnAD.Enabled = false;
            btnLogs.Enabled = false;
            if (table.Rows.Count == 1) {
                dgvResults.Rows[0].Cells[0].Selected = true;
                dgvResults.CurrentCell = dgvResults[0, 0];
                DataGridViewCellEventArgs arg = new DataGridViewCellEventArgs(0, 0);
                dgvResults_CellClick(dgvResults, arg);
            }
        }

        private void dgvResults_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex == -1) { return; }
            DataGridViewCell cell = dgvResults.Rows[e.RowIndex].Cells[1];
            selectedSamAccountName = cell.Value.ToString();
            btnAD.Enabled = true;
            btnLogs.Enabled = true;
        }

        private void btnLogs_Click(object sender, EventArgs e) {
            frmShowLogs frmShowLogs = new frmShowLogs();
            frmShowLogs.SetValues(QueryTypes.sAMAccountName, selectedSamAccountName);
            frmShowLogs.Show();
        }

        private void btnAD_Click(object sender, EventArgs e) {
            frmDisplayUserAD frmDisplayUserAD = new frmDisplayUserAD();
            frmDisplayUserAD.SetValues(QueryTypes.sAMAccountName, selectedSamAccountName);
            frmDisplayUserAD.Show();
        }

        private void frmUserSearch_Shown(object sender, EventArgs e) {
            if (preLoad is null) {
                return;
            }
            tbxSearch.Text = preLoad;
            btnSearch.PerformClick();
        }

        private void dgvResults_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e) {
            if (e.RowIndex == -1 || e.ColumnIndex != 0) {
                return;
            }
            DataGridViewCell cell = dgvResults.Rows[e.RowIndex].Cells[1];
            selectedSamAccountName = cell.Value.ToString();
            edipi = dgvResults.Rows[e.RowIndex].Cells[2].Value.ToString().Substring(0, 10);
            e.ContextMenuStrip = contextMenuStrip1;
        }

        private void loginActivityToolStripMenuItem_Click(object sender, EventArgs e) {
            frmShowLogs frmShowLogs = new frmShowLogs();
            frmShowLogs.SetValues(QueryTypes.sAMAccountName, selectedSamAccountName);
            frmShowLogs.Show();
        }

        private void dgvResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex == -1) { return; }
            DataGridViewCell cell = dgvResults.Rows[e.RowIndex].Cells[1];
            selectedSamAccountName = cell.Value.ToString();
            frmShowLogs frmShowLogs = new frmShowLogs();
            frmShowLogs.SetValues(QueryTypes.sAMAccountName, selectedSamAccountName);
            frmShowLogs.Show();
        }

        private void eUDsInCustodyToolStripMenuItem_Click(object sender, EventArgs e) {
            frmReturnLaptop frmReturnLaptop = new frmReturnLaptop {
                EDIPI = edipi
            };
            frmReturnLaptop.Show();
        }
    }
}
