using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloProxSys.MDPApp.Concierge.Models
{
    public class LoginPageModel : BasePageModel
    {
        public IEnumerable<StaffPerson> StaffList { get; set; }
        public string ReturnUrl { get; internal set; }

        public class StaffPerson
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
    }
}
