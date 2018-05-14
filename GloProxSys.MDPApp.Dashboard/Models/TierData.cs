using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloProxSys.MDPApp.Dashboard.Models
{

    //Move to own dll?
    public class TierData
    {
        public string Tier { get; set; }
        public string Range { get; set; }
        public int Players { get; set; }
        public int Days { get; set; }
        public double Theoretical { get; set; }
        public double Actual { get; set; }
        public double Cost { get; set; }
    }
}
