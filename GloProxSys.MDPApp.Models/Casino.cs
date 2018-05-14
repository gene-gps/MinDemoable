using System;
using System.Collections.Generic;
using System.Text;

namespace GloProxSys.MDPApp.Models
{
    public class Casino
    {
        public int CasinoID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Settings { get; set; }
        public string Logo { get; set; }
        public string Theme { get; set; }
        public IEnumerable<ClubLevel> ClubLevels { get; set; }
    }


    public class ClubLevel
    {
        public int ClubLevelID { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
    }
}
