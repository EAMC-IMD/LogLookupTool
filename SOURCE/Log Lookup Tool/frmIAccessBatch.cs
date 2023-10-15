using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log_Lookup_Tool {
    public partial class frmIAccessBatch : Form {
        public frmIAccessBatch() {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if (!Int32.TryParse(txtStart.Text, out int start)) {
                MessageBox.Show("Start number cannot be parsed as an integer.");
                return;
            }
            if (!Int32.TryParse(txtEnd.Text, out int end)) {
                MessageBox.Show("Start number cannot be parsed as an integer.");
                return;
            }
            string query = @"SELECT COUNT(badge_id) FROM iaccess_badges WHERE badge_serial=@checkval";
            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder {
                ["Server"] = settings.SQLServer,
                ["Initial Catalog"] = settings.Database,
                ["Integrated Security"] = true
            };
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString())) {
                sqlConnection.Open();
                Program.eventLog.WriteEntry($"Extenal connection to {Program.ResolvedDBIP}", System.Diagnostics.EventLogEntryType.Information, 4001);
                using (SqlCommand sqlCommand = new SqlCommand {
                    Connection = sqlConnection,
                    CommandText = query
                }) {
                    sqlCommand.Parameters.Add("@checkval", SqlDbType.Int);
                    sqlCommand.Parameters["@checkval"].Value = start;
                    if ((int)sqlCommand.ExecuteScalar() > 0) {
                        MessageBox.Show("Start number already exists in logs.");
                        return;
                    }
                }
                using (SqlCommand sqlCommand = new SqlCommand {
                    Connection = sqlConnection,
                    CommandText = query
                }) {
                    sqlCommand.Parameters.Add("@checkval", SqlDbType.Int);
                    sqlCommand.Parameters["@checkval"].Value = end;
                    if ((int)sqlCommand.ExecuteScalar() > 0) {
                        MessageBox.Show("End number already exists in logs.");
                        return;
                    }
                }
                query = "dbo.AddiAccessBatch";
                using (SqlCommand sqlCommand = new SqlCommand {
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = query
                }) {
                    sqlCommand.Parameters.Add("@startnum", SqlDbType.Int);
                    sqlCommand.Parameters.Add("@endnum", SqlDbType.Int);
                    sqlCommand.Parameters["@startnum"].Value = start;
                    sqlCommand.Parameters["@endnum"].Value = end;
                    try {
                        sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("Batch added successfully.");
                        txtStart.Clear();
                        txtEnd.Clear();
                    } catch {
                        MessageBox.Show("Failed to add batch.");
                    }
                }
            }
        }
    }
}
