using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloProxSys.MDPApp.APIService;
using Microsoft.AspNetCore.Mvc;

namespace GloProxSys.MDPApp.Dashboard.Controllers
{
    public class ConciergeController : Controller
    {
        IAPIDataService _data;

        public ConciergeController(IAPIDataService data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Activity()
        {
            return View();
        }

        public IActionResult Manage()
        {
            return View();
        }
    }
}