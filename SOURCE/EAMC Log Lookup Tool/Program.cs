using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMC_Log_Lookup_Tool
{
    static class Program
    {
        private static readonly Object _lock = new object();
        private static Assembly MainAssembly { get; set; }
        static ConcurrentDictionary<string, Assembly> LoadedAssemblies { get; } = new ConcurrentDictionary<string, Assembly>();

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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string currentFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string currentUserName = Environment.UserName;
            string targetFile = $"{currentFilePath}\\{currentUserName}.lok";
            try {
                File.CreateText(targetFile).Close();
            } catch { }
            MainAssembly = Assembly.GetExecutingAssembly();
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
            if (File.Exists(targetFile)) { File.Delete(targetFile); }
        }
    }
}
