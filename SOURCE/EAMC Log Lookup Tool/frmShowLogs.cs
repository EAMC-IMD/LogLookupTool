using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMC_Log_Lookup_Tool {
    public partial class frmShowLogs : Form {
        private QueryTypes queryType;
        private string query;
        public frmShowLogs() {
            InitializeComponent();
        }

        public void SetValues (QueryTypes queryTypes, string key) {
            this.queryType = queryTypes;
            this.query = key;
        }

        private void frmShowLogs_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'eUDLoggingDataSet.vwLogging' table. You can move, or remove it, as needed.
            if (this.queryType == QueryTypes.sAMAccountName) {
                EUDLoggingDataSet.vwLoggingDataTable table = vwLoggingTableAdapter.GetDataByUsername(query);
                dgvLogs.DataSource = table;
            } else if (this.queryType == QueryTypes.Hostname) {
                EUDLoggingDataSet.vwLoggingDataTable table = vwLoggingTableAdapter.GetDataByComputer(query);
                dgvLogs.DataSource = table;
            }
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
    }
}
