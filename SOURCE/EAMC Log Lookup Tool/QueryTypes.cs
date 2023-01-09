using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMC_Log_Lookup_Tool {
    public enum QueryTypes {
        None = 0,
        Hostname = 1,
        sAMAccountName = 2,
        UPN = 3,
        DoDID = 4,
        Name = 5,
        ECN = 6
    }
}
