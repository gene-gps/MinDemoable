using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GloProxSys.MDPApp.Concierge.Models;
using GloProxSys.MDPApp.APIService;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GloProxSys.MDPApp.Concierge.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        int _casino = 1;
        int _staff = 1;

        IAPIDataService _data;

        public HomeController(IAPIDataService data)
        {
            _data = data;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User != null && context.HttpContext.User.Claims != null)
            {
                int id = 0;
                var id_claim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (id_claim != null && int.TryParse(id_claim.Value, out id)) _staff = id;
            }
            base.OnActionExecuting(context);
        }

        public IActionResult Index()
        {
            var casino = _data.CasinoSettings(_casino).Result;
            var staffperson = _data.StaffPerson(_casino, _staff).Result;
            var players = _data.OnsitePlayers(_casino).Result;
            var data = new HomePageModel
            {
                CasinoID = _casino,
                CasinoName = casino.Name,
                CasinoLogo = string.IsNullOrWhiteSpace(casino.Logo) ? string.Format("casino-{0}-logo.png", _casino) : casino.Logo,
                CasinoTheme = string.IsNullOrWhiteSpace(casino.Theme) ? "default" : casino.Theme,
                StaffID = _staff,
                StaffName = staffperson.FirstName + " " + staffperson.LastName,
                StaffStatus = staffperson.Status,
                Players = players,
            };

            return View(data);
        }

        public IActionResult Profile(int id)
        {
            int offset = 0;
            if(Request.Headers.ContainsKey("X-Timezone-Offset"))
            {
                int n = 0;
                if (int.TryParse(Request.Headers["X-Timezone-Offset"], out n)) offset = n;
            }
            var player = _data.PlayerProfile(_casino, id).Result;
            var notes = _data.PlayerNotes(_casino, id).Result;
            var data = new PlayerProfileModel
            {
                Player = player,
                Notes = notes,
                TimezoneOffset = offset,
            };

            return View(data);
        }

        public IActionResult AddNote(int player, string note)
        {
            if (!string.IsNullOrWhiteSpace(note))
            {
                _data.AddNote(_casino, _staff, player, note);
                
            }
            return Json(_data.PlayerNotes(_casino, player).Result);
        }

        //Move to own controller
        public void UpdateStaffStatus(string status)
        {
            if (!string.IsNullOrWhiteSpace(status))
            {
                _data.UpdateStaffStatus(_casino, _staff, status);

            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
