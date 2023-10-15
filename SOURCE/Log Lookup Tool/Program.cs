using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Log_Lookup_Tool
{
    static class Program {
        public static readonly Properties.Settings settings = Properties.Settings.Default;
        private static readonly Object _lock = new object();
        private static Assembly MainAssembly { get; set; }
        static ConcurrentDictionary<string, Assembly> LoadedAssemblies { get; } = new ConcurrentDictionary<string, Assembly>();

        public static string LockFile;

        public static EventLog eventLog;

        public static string ResolvedDBIP;

        public static bool IsAdmin;


        public static bool IsAllowed;

        public static string UserName;

        public static string CurrentPath;

        public static System.Collections.Generic.Dictionary<int, string> ScooterCodes = new System.Collections.Generic.Dictionary<int, string>();

        private static readonly string[] allowedGroups = settings.allowedGroups.Split(',');
        private static readonly string[] OUGroups = settings.OUGroups.Split(',');
        public static void GroupCheck() {
            IsAllowed = false;
            IsAdmin = false;
            try {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain)) {
                    using (UserPrincipal user = UserPrincipal.Current) {
                        UserName = user.SamAccountName;
                        if (user != null) {
                            var groups = user.GetAuthorizationGroups().Select(g => g.Name).ToList();
                            IsAllowed = allowedGroups.Any(groups.Contains);
                            IsAdmin = OUGroups.Any(groups.Contains);
                        }
                    }
                }
            } catch {
            }
        }
        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args) {
            AssemblyName assemblyName = new AssemblyName(args.Name);
            string path = $"{assemblyName.Name}.dll";
            if ((assemblyName.CultureInfo != null) && !assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture)) {
                path = $@"{assemblyName.CultureInfo}\{path}";
            }
            var assembly = LoadedAssemblies.GetOrAdd(path, LoadAssemblyFromEmbeddedResources);
            return assembly;
        }

        private static Assembly LoadAssemblyFromEmbeddedResources(string path) {
            lock(_lock) {
                using (Stream stream = MainAssembly.GetManifestResourceStream(path)) {
                    if (stream == null) { return null; }
                    byte[] assemblyRawBytes = new byte[stream.Length];
                    stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                    var assembly = Assembly.Load(assemblyRawBytes);
                    return assembly;
                }
            }
        }

        public static void RestartAsAdmin() {
            string Path = Assembly.GetExecutingAssembly().Location;
            Process process = new Process {
                StartInfo = {
                    FileName = Path,
                    UseShellExecute = true,
                    Verb = "runas"
                }
            };
            process.Start();
            eventLog.WriteEntry("Log Lookup Tool restarting with elevated credentials.", EventLogEntryType.Information, 5006);
            foreach (Form form in Application.OpenForms) {
                form.Close();
            }
            RemoveLockFile(LockFile);
        }

        public static void RemoveLockFile(string LockFile) {
            if (File.Exists(LockFile)) { File.Delete(LockFile); }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try {
                if (!EventLog.SourceExists("Log Lookup Tool")) {
                    try {
                        EventLog.CreateEventSource("Log Lookup Tool", "Application");
                        MessageBox.Show("Event log source created.  Application may now be run with standard credentials.");
                        Environment.Exit(0);
                    } catch (System.Security.SecurityException) {
                        MessageBox.Show("Due to STIG requirements, the first run of version 2.1.0.0+ must be run with administrative credentials. Exiting.");
                        Environment.Exit(1);
                    }
                }
            } catch (System.Security.SecurityException) {
                MessageBox.Show("Due to STIG requirements, the first run of version 2.1.0.0+ must be run with administrative credentials. Exiting.");
                Environment.Exit(1);
            }
            eventLog = new EventLog {
                Source = "Log Lookup Tool"
            };
            eventLog.WriteEntry("Log Lookup Tool startup.", EventLogEntryType.Information, 5000);
            GroupCheck();
            if (!IsAllowed) {
                MessageBox.Show("Could not determine authorization for program launch.  Exiting.");
                eventLog.WriteEntry("Group membership failure", EventLogEntryType.Information, 5001);
                eventLog.WriteEntry("Log Lookup Tool exit.", EventLogEntryType.Information, 5005);
                Environment.Exit(1);
            }
            System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(settings.SQLServer);
            if (hostEntry.AddressList.Length > 0) {
                ResolvedDBIP = hostEntry.AddressList[0].ToString();
            }
            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder {
                ["Server"] = settings.SQLServer,
                ["Initial Catalog"] = settings.Database,
                ["Integrated Security"] = true
            };
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString.ToString())) {
                try {
                    sqlConnection.Open();
                    eventLog.WriteEntry($"Extenal connection to {ResolvedDBIP}", EventLogEntryType.Information, 4001);
                } catch {
                    MessageBox.Show("Could not establish connection to SQL server.  Application will now exit. Please ensure network connectivity and open ports from this VLAN");
                    eventLog.WriteEntry("SQL connection failure", EventLogEntryType.Information, 5002);
                    eventLog.WriteEntry("Log Lookup Tool exit.", EventLogEntryType.Information, 5005);
                    Environment.Exit(1);
                }
            }
            MainAssembly = Assembly.GetExecutingAssembly();
            CurrentPath = Path.GetDirectoryName(MainAssembly.Location);
            string currentUserName = Environment.UserName;
            LockFile = $"{CurrentPath}\\LokFiles\\{currentUserName}.lok";
            try {
                using (StreamWriter lok = File.CreateText(LockFile)) {
                    lok.WriteLine(Environment.MachineName);
                    lok.Close();
                }
            } catch { }
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
            RemoveLockFile(LockFile);
            eventLog.WriteEntry("Log Lookup Tool exit.", EventLogEntryType.Information, 5005);
        }
    }
}
