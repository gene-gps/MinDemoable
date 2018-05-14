using System;
using System.Collections.Generic;
using System.Text;

namespace GloProxSys.MDPApp.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public int CasinoID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Status { get; set; }
        public string PhotoUrl { get; set; }
        public string Profile { get; set; }
    }
    public class StaffStatusPostData
    {
        public string Status { get; set; }
    }

}
