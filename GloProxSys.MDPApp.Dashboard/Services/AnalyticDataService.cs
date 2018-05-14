using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace GloProxSys.MDPApp.Dashboard.Services
{
    public class AnalyticDataService : IAnalyticDataService
    {

        public ExpandoObject GetData(int casino_id, string dataset, object parameters)
        {
            //Reflect into plug-in dll to get data????

            dynamic data = new ExpandoObject();
            data.Type = "Not Implemented";
            data.Data = "Not Implemented";

            return data;
        }
    }
}
