using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace Log_Lookup_Tool {
    [DirectoryRdnPrefix("CN")]
    [DirectoryObjectClass("computer")]
    class ComputerPrincipalEx : ComputerPrincipal {
        const int ADS_UF_ACCOUNTDISABLE = 0x00000002;
        [Flags()]
        public enum UAC : int {
            SCRIPT = 0x00000001,
            ACCOUNTDISABLE = 0x00000002,
            HOMEDIR_REQUIRED = 0x00000008,
            LOCKOUT = 0x00000010,
            PASSWD_NOTREQD = 0x00000020,
            PASSWD_CANT_CHANGE = 0x00000040,
            ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0x00000080,
            TEMP_DUPLICATE_ACCOUNT = 0x00000100,
            NORMAL_ACCOUNT = 0x00000200,
            INTERDOMAIN_TRUST_ACCOUNT = 0x00000800,
            WORKSTATION_TRUST_ACCOUNT = 0x00001000,
            SERVER_TRUST_ACCOUNT = 0x00002000,
            Unused1 = 0x00004000,
            Unused2 = 0x00008000,
            DONT_EXPIRE_PASSWD = 0x00010000,
            MNS_LOGON_ACCOUNT = 0x00020000,
            SMARTCARD_REQUIRED = 0x00040000,
            TRUSTED_FOR_DELEGATION = 0x00080000,
            NOT_DELEGATED = 0x00100000,
            USE_DES_KEY_ONLY = 0x00200000,
            DONT_REQUIRE_PREAUTH = 0x00400000,
            PASSWORD_EXPIRED = 0x00800000,
            TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION = 0x01000000,
            PARTIAL_SECRETS_ACCOUNT = 0x04000000,
            USE_AES_KEYS = 0x08000000
        }
        public ComputerPrincipalEx(PrincipalContext context) : base(context) { }
        public ComputerPrincipalEx(PrincipalContext context, string samAccountName, string password, bool enabled) : base(context, samAccountName, password, enabled) { }
        public static new ComputerPrincipalEx FindByIdentity(PrincipalContext context, string identityValue) {
            return (ComputerPrincipalEx)FindByIdentityWithType(context, typeof(ComputerPrincipalEx), identityValue);
        }
        public static new ComputerPrincipalEx FindByIdentity(PrincipalContext context, IdentityType identityType, string identityValue) {
            return (ComputerPrincipalEx)FindByIdentityWithType(context, typeof(ComputerPrincipalEx), identityType, identityValue);
        }
        //new public string ExtensionGet(String ExtendedAttribute) {
        //    try {
        //        if (base.ExtensionGet(ExtendedAttribute).Length != 1) {
        //            return null;
        //        } else {
        //            return (string)base.ExtensionGet(ExtendedAttribute)[0];
        //        }
        //    } catch {
        //        return null;
        //    }
        //}
        //public new void ExtensionSet(string ExtendedAttribute, object value) {
        //    try {
        //        base.ExtensionSet(ExtendedAttribute, value);
        //        base.Save();
        //    } catch {
        //        return;
        //    }
        //}
        [DirectoryProperty("userAccountControl")]
        public UAC? UserAccountControl {
            get {
                if (ExtensionGet("userAccountControl").Length != 1)
                    return null;
                return (UAC)ExtensionGet("userAccountControl")[0];
            }
            set => ExtensionSet("userAccountControl", (int)value);
        }
        public void Enable() {
            if (UserAccountControl is null)
                return;
            UserAccountControl &= ~UAC.ACCOUNTDISABLE;
            DirectoryEntry de = (DirectoryEntry)this.GetUnderlyingObject();
            de.Properties["userAccountControl"].Value = (int)UserAccountControl;
            de.CommitChanges();
        }
        public void Disable() {
            if (UserAccountControl is null)
                return;
            UserAccountControl |= UAC.ACCOUNTDISABLE;
            DirectoryEntry de = (DirectoryEntry)this.GetUnderlyingObject();
            de.Properties["userAccountControl"].Value = (int)UserAccountControl;
            de.CommitChanges();
        }
        public bool IsEnabled() {
            if (UserAccountControl is null)
                return false;
            if (((UAC)UserAccountControl).HasFlag(UAC.ACCOUNTDISABLE)) {
                return false;
            }
            return true;
        }
    }
}
