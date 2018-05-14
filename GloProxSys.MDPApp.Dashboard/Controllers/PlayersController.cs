using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloProxSys.MDPApp.APIService;
using GloProxSys.MDPApp.Dashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace GloProxSys.MDPApp.Dashboard.Controllers
{
    public class PlayersController : Controller
    {
        IAPIDataService _api;
        IAnalyticDataService _dw;

        int CasinoID = 1;

        public PlayersController(IAPIDataService api, IAnalyticDataService dw)
        {
            _api = api;
            _dw = dw;
        }

        public IActionResult Index()
        {
            dynamic d = _dw.GetData(1, "AnnualTier", new { Year = 2018 });
            //var data = Convert.ChangeType(d.Data, d.Type);
            return View(d.Data);
        }

        public IActionResult Onsite()
        {
            var players = _api.OnsitePlayers(CasinoID).Result;
            var data = new OnsitePlayerModel
            {
                Players = players,
            };
            return View(data);
        }

        public IActionResult All()
        {
            return View();
        }
    }
}