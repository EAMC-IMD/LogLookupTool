using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EAMC_Log_Lookup_Tool {
    class Query {

        public static QueryTypes CheckType (string Query) {
            switch (Query) {
                case var Val when new Regex(@"^(?![\s\S])").IsMatch(Val):
                    return QueryTypes.None;
                case var Val when new Regex(@"(?<surname>^[^,]*)(?:,\s?)(?<givenname>.*)").IsMatch(Val):
                    return QueryTypes.Name;
                case var Val when new Regex(@"^.*@mil").IsMatch(Val):
                    return QueryTypes.UPN;
                case var Val when new Regex(@"EAMC.*").IsMatch(Val):
                    return QueryTypes.Hostname;
                case var Val when new Regex(@"(?!.*@)(?:^\d{10}\d{0,6}$)").IsMatch(Val):
                    return QueryTypes.DoDID;
                case var Val when new Regex(@"(?<ecn>\d?\d{5})").IsMatch(Val):
                    return QueryTypes.ECN;
                default:
                    return QueryTypes.sAMAccountName;
            }
        }

    }
}
