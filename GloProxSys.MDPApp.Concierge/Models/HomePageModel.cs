using GloProxSys.MDPApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloProxSys.MDPApp.Concierge.Models
{
    public class HomePageModel : BasePageModel
    {
        

        public IEnumerable<PlayerProfile> Players { get; set; }
    }

    public class PlayerProfileModel
    {
        public PlayerProfile Player { get; set; }
        public IEnumerable<PlayerNote> Notes { get; set; }
        public int TimezoneOffset { get; internal set; }
    }
}
