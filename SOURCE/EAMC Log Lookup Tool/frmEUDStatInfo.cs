using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMC_Log_Lookup_Tool {
    public partial class frmEUDStatInfo : Form {
        private string hostname;
        public void SetValue (string Query) {
            this.hostname = Query;
        }
        public frmEUDStatInfo() {
            InitializeComponent();
        }

        private void frmEUDStatInfo_Shown(object sender, EventArgs e) {
            string sql;
            StringBuilder result = new StringBuilder();
            sql = @"SELECT computername, 
                    ECN, 
                    Dept, 
                    hrh, 
                    AcqDate, 
                    RunDate, 
                    CPUCount, 
                    CPU_Arch, 
                    CPU_Id, 
                    Manufacturer, 
                    Model, 
                    SerialNumber, 
                    OSVersion,
                    OSInstallDate,
                    PhysicalMemoryGB, 
                    HDD0_SizeGB, 
                    dbo.UTCToEST(data_timestamp) AS [timestamp]
                FROM dbo.FullHardwareData(@hostname)";
            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder();
            sqlConnectionString["Server"] = Properties.Resources.SQLServer;
            sqlConnectionString["Initial Catalog"] = Properties.Resources.Database;
            sqlConnectionString["Integrated Security"] = true;
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString());
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand {
                Connection = sqlConnection,
                CommandText = sql
            };
            sqlCommand.Parameters.Add("@hostname", SqlDbType.VarChar);
            sqlCommand.Parameters["@hostname"].Value = this.hostname;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read()) {
                result.AppendLine($"Hardware Data for {reader["computername"]} as of {reader["timestamp"]}");
                result.AppendLine($"CPU: {reader["CPU_Id"]}");
                result.AppendLine($"CPU Cores: {reader["CPUCount"]}");
                result.AppendLine($"CPU Arch: {reader["CPU_Arch"]}");
                result.AppendLine($"EUD Manufacturer: {reader["Manufacturer"]}");
                result.AppendLine($"EUD Model: {reader["Model"]}");
                result.AppendLine($"EUD SN: {reader["SerialNumber"]}");
                result.AppendLine($"EUD Physical Memory: {reader["PhysicalMemoryGB"]}GB");
                result.AppendLine($"EUD Primary HDD: {reader["HDD0_SizeGB"]}GB");
                result.AppendLine($"Windows Version: {reader["OSVersion"]}");
                result.AppendLine($"OS Install Date: {reader["OSInstallDate"]}");
                result.AppendLine("");
                if (reader["SerialNumber"] is System.DBNull) {
                    result.AppendLine("Inventory Data not available - no Serial Number available.");
                } else if (!Regex.IsMatch((string)reader["SerialNumber"], "VMware.*")) {
                    result.AppendLine($"Inventory Data for {reader["computername"]} as of {reader["RunDate"]}");
                    result.AppendLine($"ECN: {reader["ECN"]}");
                    result.AppendLine($"Department: {reader["Dept"]}");
                    result.AppendLine($"Custodian: {reader["hrh"]}");
                    result.AppendLine($"Acquisition Date: {reader["AcqDate"]}");
                } else {
                    result.AppendLine("Inventory Data not available for virtual devices.");
                }
                result.AppendLine("");
            } else {
                result.AppendLine($"No hardware data found for {this.hostname}");
            }
            result.AppendLine("");
            reader.Close();

            sql = @"SELECT MAX(data_timestamp) dts FROM EUDNetData WHERE computername=@hostname";
            sqlCommand = new SqlCommand {
                Connection = sqlConnection,
                CommandText = sql
            };
            sqlCommand.Parameters.Add("@hostname", SqlDbType.VarChar);
            sqlCommand.Parameters["@hostname"].Value = this.hostname;
            reader = sqlCommand.ExecuteReader();
            if (reader.HasRows) {
                reader.Read();
                var date = reader.GetValue(0);
                reader.Close();
                if (date is System.DBNull) {
                    result.AppendLine($"No adapter data available for {this.hostname}");
                } else {
                    sql = @"SELECT computername, 
                        lastMediaState, 
                        AdapterDesc, 
                        IPv4,(
                            SELECT VLAN FROM VLANs WHERE dbo.IPAddressIsInRange([IPv4], [VLAN_CIDR])=1
                        ) VLAN, (
                            SELECT VLAN_Desc FROM VLANs WHERE dbo.IPAddressIsInRange([IPv4], [VLAN_CIDR]) = 1
                        ) VLAN_Desc, 
                        MAC, 
                        dbo.UTCToEST(data_timestamp) AS[timestamp]
                    FROM EUDNetData 
                    WHERE computername = @hostname 
                        AND data_timestamp>= DATEADD(second, -20, @date) 
                    ORDER BY lastMediaState DESC ";
                    sqlCommand = new SqlCommand {
                        Connection = sqlConnection,
                        CommandText = sql
                    };
                    sqlCommand.Parameters.Add("@hostname", SqlDbType.VarChar);
                    sqlCommand.Parameters.Add("@date", SqlDbType.DateTime2);
                    sqlCommand.Parameters["@hostname"].Value = this.hostname;
                    sqlCommand.Parameters["@date"].Value = (DateTime)date;
                    reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows) {
                        result.AppendLine($"Adapter Data for {this.hostname} as of {date}");
                        while (reader.Read()) {
                            result.AppendLine($"Adapter: {reader["AdapterDesc"]}");
                            result.AppendLine($"Last State: {reader["lastMediaState"]}");
                            if (!(reader["IPv4"] is System.DBNull)) {
                                result.AppendLine($"IP Address: {reader["IPv4"]}");
                                result.AppendLine($"VLAN: {reader["VLAN"]}");
                                result.AppendLine($"VLAN Desc: {reader["VLAN_Desc"]}");
                            }
                            result.AppendLine($"MAC Address: {reader["MAC"]}");
                            result.AppendLine("");
                        }
                    } else {
                        result.AppendLine($"No adapter data found for {this.hostname}");
                    }
                }
            }
            reader.Close();

            sqlConnection.Close();
            sqlConnection.Dispose();

            tbxStatData.Text = result.ToString();
        }
    }
}
