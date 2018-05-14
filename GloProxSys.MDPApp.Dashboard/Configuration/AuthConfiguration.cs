using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasinoDashboard.Configuration
{
    public class AuthConfiguration
    {
        public string Region { get; set; }
        public string UserPoolID { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
    }
}
