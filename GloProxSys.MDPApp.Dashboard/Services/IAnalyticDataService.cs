using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace GloProxSys.MDPApp.Dashboard.Services
{
    public interface IAnalyticDataService
    {
        ExpandoObject GetData(int casino_id, string dataset, object parameters);
    }
}
