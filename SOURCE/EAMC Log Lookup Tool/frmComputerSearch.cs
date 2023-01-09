using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMC_Log_Lookup_Tool {
    public partial class frmComputerSearch : Form {

        string preLoad = null;
        private string selectedHostname = null;
        public frmComputerSearch() {
            InitializeComponent();
        }

        public void PreLoadSearchValue (string Hostname) {
            this.preLoad = Hostname;
        }

        private string GetHostnameFromDB(string Query) {
            string sql;
            string result = null;
            sql = @"SELECT [dbo].SNfromECN(@ecn) AS hostname";
            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder();
            sqlConnectionString["Server"] = Properties.Resources.SQLServer;
            sqlConnectionString["Initial Catalog"] = Properties.Resources.Database;
            sqlConnectionString["Integrated Security"] = true;
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString());
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = sql;
            sqlCommand.Parameters.Add("@ecn", SqlDbType.VarChar);
            sqlCommand.Parameters["@ecn"].Value = Query;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read()) {
                result = (string)reader.GetValue(0);
            }
            reader.Close();
            sqlConnection.Close();
            sqlConnection.Dispose();
            return result;
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            string query = tbxSearch.Text.ToUpperInvariant();
            if (query.Length < 4) { return; }
            QueryTypes queryTypes = Query.CheckType(query);

            switch (queryTypes) {
                case QueryTypes.sAMAccountName:
                case QueryTypes.Name:
                case QueryTypes.UPN:
                case QueryTypes.DoDID:
                    MessageBox.Show(
                        "Entered query not detected as Hostname or ECN.",
                        "Query Parse Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Hand);
                    return;
                case QueryTypes.ECN:
                    query = GetHostnameFromDB(query);
                    if (query is null) {
                        MessageBox.Show(
                            "No hostname found for that ECN",
                            "Query Parse Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Hand);
                        return;
                    }
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

            DirectorySearcher search = new DirectorySearcher();
            search.SearchRoot = new DirectoryEntry("LDAP://OU=Workstations,OU=EAMC,OU=DoD,DC=med,DC=ds,DC=osd,DC=mil");
            search.PropertiesToLoad.Add("name");
            search.PropertiesToLoad.Add("description");
            search.PropertiesToLoad.Add("distinguishedname");
            SearchResultCollection resultCollection = null;
            DataTable table = new DataTable();
            table.Columns.Add("Hostname", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Distinguished Name", typeof(string));
            search.Filter = String.Format("(name={0})", query);
            resultCollection = search.FindAll();
            foreach (SearchResult thisResult in resultCollection) {
                if (thisResult.Properties["description"].Count == 0) {
                    table.Rows.Add(thisResult.Properties["name"][0], "", thisResult.Properties["distinguishedname"][0]);
                } else {
                    table.Rows.Add(thisResult.Properties["name"][0], thisResult.Properties["description"][0], thisResult.Properties["distinguishedname"][0]);
                }  
            }
            dgvResults.DataSource = table;
            btnAD.Enabled = false;
            btnLogs.Enabled = false;
            btnAppList.Enabled = false;
            btnHardwareInfo.Enabled = false;
            if (table.Rows.Count == 1) {
                dgvResults.Rows[0].Cells[0].Selected = true;
                dgvResults.CurrentCell = dgvResults[0, 0];
                DataGridViewCellEventArgs arg = new DataGridViewCellEventArgs(0, 0);
                dgvResults_CellClick(dgvResults, arg);
            }
        }

        private void dgvResults_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex == -1) { return; }
            DataGridViewCell cell = dgvResults.Rows[e.RowIndex].Cells[0];
            this.selectedHostname = cell.Value.ToString();
            btnAD.Enabled = true;
            btnLogs.Enabled = true;
            btnAppList.Enabled = true;
            btnHardwareInfo.Enabled = true;
        }

        private void btnLogs_Click(object sender, EventArgs e) {
            frmShowLogs frmShowLogs = new frmShowLogs();
            frmShowLogs.SetValues(QueryTypes.Hostname, this.selectedHostname);
            frmShowLogs.ShowDialog();
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
            frmEUDStatInfo.SetValue(this.selectedHostname);
            frmEUDStatInfo.Show();
        }
    }
}
