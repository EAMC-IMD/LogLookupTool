using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace EAMC_Log_Lookup_Tool {
    class SafeNativeMethods {

        [DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool ConvertSidToStringSid([MarshalAs(UnmanagedType.LPArray)] byte[] pSID, out IntPtr ptrSid);

        public static string GetSidString(byte[] sid) {
            IntPtr ptrSid;
            string sidString;
            if (!ConvertSidToStringSid(sid, out ptrSid))
                throw new System.ComponentModel.Win32Exception();
            try {
                sidString = Marshal.PtrToStringAuto(ptrSid);
            } finally {
                Marshal.FreeHGlobal(ptrSid);
            }
            return sidString;
        }

    }
}
