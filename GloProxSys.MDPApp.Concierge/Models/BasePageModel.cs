using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloProxSys.MDPApp.Concierge.Models
{
    public class BasePageModel
    {
        public int CasinoID { get; set; }
        public string CasinoLogo { get; set; }
        public string CasinoTheme { get; set; }
        public string CasinoName { get; set; }

        public int StaffID { get; set; }
        public string StaffName { get; set; }
        public string StaffStatus { get; set; }
    }
}
