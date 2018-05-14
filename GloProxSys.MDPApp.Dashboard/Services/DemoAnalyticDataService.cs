using GloProxSys.MDPApp.Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace GloProxSys.MDPApp.Dashboard.Services
{
    public class DemoAnalyticDataService : IAnalyticDataService
    {
        IQueryable<TierData> _data;

        public DemoAnalyticDataService()
        {
            _data = new List<TierData>() {
                new TierData { Range = "500+", Tier = "Tier 1", Players = 134, Days = 392, Theoretical = 322090, Actual = 312834, Cost = 58327 },
                new TierData { Range = "250-500", Tier = "Tier 2", Players = 444, Days = 1328, Theoretical = 439010, Actual = 310305, Cost = 103695 },
                new TierData { Range = "175-250", Tier = "Tier 3", Players = 458, Days = 1353, Theoretical = 278925, Actual = 214343, Cost = 69745 },
                new TierData { Range = "100-175", Tier = "Tier 4", Players = 1103, Days = 3421, Theoretical = 450566, Actual = 421004, Cost = 124356 },
                new TierData { Range = "50-100", Tier = "Tier 5", Players = 1973, Days = 5959, Theoretical = 424344, Actual = 430945, Cost = 134502 },
                new TierData { Range = "25-50", Tier = "Tier 6", Players = 1857, Days = 5417, Theoretical = 198999, Actual = 190272, Cost = 58622 },
                new TierData { Range = "10-25", Tier = "Tier 7", Players = 1808, Days = 4936, Theoretical = 83204, Actual = 90215, Cost = 33337 },
                new TierData { Range = "0-10", Tier = "Tier 8", Players = 1799, Days = 3656, Theoretical = 18305, Actual = 2537, Cost = 18503 },
            }.AsQueryable();
        }

        

        public ExpandoObject GetData(int casino_id, string dataset, object parameters)
        {
            dynamic data = new ExpandoObject();

            switch ((dataset ?? "").ToLower())
            {
                case "annualtier":
                    data.Type = typeof(IQueryable<TierData>);
                    data.Data = _data;
                    break;
            }

            return data;
        }

    }




}
