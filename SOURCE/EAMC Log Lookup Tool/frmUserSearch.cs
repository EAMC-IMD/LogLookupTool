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

namespace EAMC_Log_Lookup_Tool
{
    public partial class frmUserSearch : Form {

        string preLoad = null;
        private string selectedSamAccountName = null;
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
            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder();
            sqlConnectionString["Server"] = Properties.Resources.SQLServer;
            sqlConnectionString["Initial Catalog"] = Properties.Resources.Database;
            sqlConnectionString["Integrated Security"] = true;
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString());
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = sql;
            sqlCommand.Parameters.Add("@upn", SqlDbType.VarChar);
            sqlCommand.Parameters["@upn"].Value = Query;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read()) {
                result = (string)reader.GetValue(0);
            }
            reader.Close();
            sqlConnection.Close();
            sqlConnection.Dispose();
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

            DirectorySearcher search = new DirectorySearcher();
            search.SearchRoot = new DirectoryEntry("LDAP://OU=Users,OU=EAMC,OU=DoD,DC=med,DC=ds,DC=osd,DC=mil");
            search.PropertiesToLoad.Add("displayname");
            search.PropertiesToLoad.Add("samaccountname");
            search.PropertiesToLoad.Add("userprincipalname");
            SearchResultCollection resultCollection = null;
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
            this.selectedSamAccountName = cell.Value.ToString();
            btnAD.Enabled = true;
            btnLogs.Enabled = true;
        }

        private void btnLogs_Click(object sender, EventArgs e) {
            frmShowLogs frmShowLogs = new frmShowLogs();
            frmShowLogs.SetValues(QueryTypes.sAMAccountName, this.selectedSamAccountName);
            frmShowLogs.ShowDialog();
        }

        private void btnAD_Click(object sender, EventArgs e) {
            frmDisplayUserAD frmDisplayUserAD = new frmDisplayUserAD();
            frmDisplayUserAD.SetValues(QueryTypes.sAMAccountName, this.selectedSamAccountName);
            frmDisplayUserAD.Show();
        }

        private void frmUserSearch_Shown(object sender, EventArgs e) {
            if (preLoad is null) {
                return;
            }
            tbxSearch.Text = preLoad;
            this.btnSearch.PerformClick();
        }
    }
}
