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

namespace Log_Lookup_Tool {
    public partial class frmDisplayUserAD : Form {
        private string query;
        private QueryTypes queryType;
        public void SetValues(QueryTypes QueryType, string Username) {
            this.queryType = QueryType;
            this.query = Username;
        }
        public frmDisplayUserAD() {
            InitializeComponent();
        }

        [Flags]
        private enum ADUserAccountControl : long {
            SCRIPT = 0x0001,
            ACCOUNTDISABLE = 0x0002,
            HOMEDIR_REQUIRED = 0x0008,
            LOCKOUT = 0x0010,
            PASSWD_NOTREQD = 0x0020,
            PASSWD_CANT_CHANGE = 0x0040,
            ENCRYPTED_TEXT_PWD_ALLOWED = 0x0080,
            TEMP_DUPLICATE_ACCOUNT = 0x0100,
            NORMAL_ACCOUNT = 0x0200,
            INTERDOMAIN_TRUST_ACCOUNT = 0x0800,
            WORKSTATION_TRUST_ACCOUNT = 0x1000,
            SERVER_TRUST_ACCOUNT = 0x2000,
            DONT_EXPIRE_PASSWORD = 0x10000,
            MNS_LOGON_ACCOUNT = 0x20000,
            SMARTCARD_REQUIRED = 0x40000,
            TRUSTED_FOR_DELEGATION = 0x80000,
            NOT_DELEGATED = 0x100000,
            USE_DES_KEY_ONLY = 0x200000,
            DONT_REQ_PREAUTH = 0x400000,
            PASSWORD_EXPIRED = 0x800000,
            TRUSTED_TO_AUTH_FOR_DELEGATION = 0x1000000,
            PARTIAL_SECRETS_ACCOUNT = 0x04000000,
        }

        private static readonly string DefaultScooterCodePath = $"{Program.CurrentPath}\\Data Files\\DeptCodes.csv";
        private static readonly string FallbackScooterCodePath = Program.settings.FallBackScooterPath;

        private string ConvertDeptCodeToString(object DeptCode) {
            if (DeptCode is null)
                return String.Empty;
            if (!(DeptCode is String) && !(DeptCode is int)) {
                return String.Empty;
            }
            if (DeptCode is int @int)
                return Program.ScooterCodes[@int];
            if (int.TryParse((string)DeptCode, out int code)) {
                if (Program.ScooterCodes.ContainsKey(code))
                    return Program.ScooterCodes[code];
                return String.Empty;
            } else {
                return (string)DeptCode;
            }
        }

        private StringBuilder GetUserData() {
            DirectorySearcher search = new DirectorySearcher {
                SearchRoot = new DirectoryEntry(Program.settings.LDAPRoot)
            };
            search.PropertiesToLoad.Add("displayname");
            search.PropertiesToLoad.Add("distinguishedname");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("useraccountcontrol");
            search.PropertiesToLoad.Add("homedirectory");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("msds-cloudextensionattribute1");
            search.PropertiesToLoad.Add("msds-phoneticcompanyname");
            search.PropertiesToLoad.Add("telephonenumber");
            search.PropertiesToLoad.Add("samaccountname");
            search.PropertiesToLoad.Add("scriptpath");
            search.PropertiesToLoad.Add("objectsid");
            search.PropertiesToLoad.Add("userprincipalname");
            search.Filter = String.Format("(anr={0})", this.query);
            SearchResult result = search.FindOne();
            StringBuilder ADProperties = new StringBuilder();
            if (result == null) {
                ADProperties.AppendLine($"No AD object matching {this.query} found!");
                return ADProperties;
            }
            var vaProperty = result.Properties["useraccountcontrol"];
            string enabled;
            string locked;
            ADUserAccountControl userAccountControl = (ADUserAccountControl)Int32.Parse(vaProperty[0].ToString());
            if ((userAccountControl & ADUserAccountControl.ACCOUNTDISABLE) == ADUserAccountControl.ACCOUNTDISABLE) {
                enabled = "False";
            } else {
                enabled = "True";
            }
            string sid = SafeNativeMethods.GetSidString((byte[])result.Properties["objectSid"][0]);
            if ((userAccountControl & ADUserAccountControl.LOCKOUT) == ADUserAccountControl.LOCKOUT) {
                locked = "True";
            } else {
                locked = "False";
            }
            ADProperties.AppendLine($"AD Properties for {this.query}");
            ADProperties.AppendLine("");
            ADProperties.AppendLine($"Display Name: {result.Properties["displayname"][0]}");
            ADProperties.AppendLine($"Distinguished Name: {result.Properties["distinguishedname"][0]}");
            ADProperties.AppendLine($"Email Address: {result.Properties["mail"][0]}");
            ADProperties.AppendLine($"Enabled: {enabled}");
            ADProperties.AppendLine($"Home Directory: {result.Properties["homedirectory"][0]}");
            ADProperties.AppendLine($"Locked: {locked}");
            ADProperties.AppendLine($"ms-DS-cloudExtensionAttribute1: {result.Properties["msds-cloudextensionattribute1"][0]}");
            if (result.Properties["telephonenumber"].Count != 0) {
                ADProperties.AppendLine($"Office Phone: {result.Properties["telephonenumber"][0]}");
            }
            ADProperties.AppendLine($"sAMAccountName: {result.Properties["samaccountname"][0]}");
            ADProperties.AppendLine($"Logon Script: {result.Properties["scriptpath"][0]}");
            ADProperties.AppendLine($"SID: {sid}");
            ADProperties.AppendLine($"UserPrincipalName: {result.Properties["userprincipalname"][0]}");
            if (result.Properties["msds-phoneticcompanyname"].Count != 0) {
                ADProperties.AppendLine($"msDS-PhoeneticCompanyName: {result.Properties["msds-phoneticcompanyname"][0]}");
                ADProperties.AppendLine($"Department: {ConvertDeptCodeToString(result.Properties["msds-phoneticcompanyname"][0])}");
            }
            return ADProperties;
        }

        private StringBuilder GetComputerData() {
            DirectorySearcher search = new DirectorySearcher {
                SearchRoot = new DirectoryEntry(Program.settings.LDAPWorkstation)
            };
            search.PropertiesToLoad.Add("name");
            search.PropertiesToLoad.Add("dnshostname");
            search.PropertiesToLoad.Add("distinguishedname");
            search.PropertiesToLoad.Add("useraccountcontrol");
            search.PropertiesToLoad.Add("description");
            search.PropertiesToLoad.Add("objectsid");
            search.Filter = String.Format("(name={0})", this.query);
            SearchResult result = search.FindOne();
            StringBuilder ADProperties = new StringBuilder();
            if (result == null) {
                ADProperties.AppendLine($"No AD object matching {this.query} found!");
                return ADProperties;
            }
            var vaProperty = result.Properties["useraccountcontrol"];
            string enabled;
            ADUserAccountControl userAccountControl = (ADUserAccountControl)Int32.Parse(vaProperty[0].ToString());
            if ((userAccountControl & ADUserAccountControl.ACCOUNTDISABLE) == ADUserAccountControl.ACCOUNTDISABLE) {
                enabled = "False";
            } else {
                enabled = "True";
            }
            string sid = SafeNativeMethods.GetSidString((byte[])result.Properties["objectSid"][0]);
            ADProperties.AppendLine($"AD Properties for {this.query}");
            ADProperties.AppendLine("");
            ADProperties.AppendLine($"Hostname: {result.Properties["name"][0]}");
            ADProperties.AppendLine($"FQDN: {result.Properties["dnshostname"][0]}");
            ADProperties.AppendLine($"Distinguished Name: {result.Properties["distinguishedname"][0]}");
            ADProperties.AppendLine($"Enabled: {enabled}");
            if (result.Properties["description"].Count != 0) {
                ADProperties.AppendLine($"Description: {result.Properties["description"][0]}");
            }
            ADProperties.AppendLine($"SID: {sid}");
            return ADProperties;
        }

        private void frmDisplayUserAD_Shown(object sender,  EventArgs e) {
            StringBuilder ADProperties;
            if (this.queryType == QueryTypes.sAMAccountName) {
                if (Program.ScooterCodes.Count == 0) {
                    try {
                        string target = DefaultScooterCodePath;
                        if (!System.IO.File.Exists(DefaultScooterCodePath)) {
                            target = FallbackScooterCodePath;
                        }
                        using (System.IO.StreamReader reader = new System.IO.StreamReader(target)) {
                            string line;
                            while ((line = reader.ReadLine()) != null) {
                                string[] fields = line.Split(',');
                                int code = -1;
                                if (int.TryParse(fields[0], out code)) {
                                    Program.ScooterCodes.Add(code, fields[1]);
                                }
                            }
                        }
                    } catch { }
                }
                ADProperties = GetUserData();
            } else {
                ADProperties = GetComputerData();
            }
            
            txtADInfo.Text = ADProperties.ToString();
            return;
        }
    }
}
