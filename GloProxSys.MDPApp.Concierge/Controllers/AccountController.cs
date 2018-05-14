using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GloProxSys.MDPApp.APIService;
using GloProxSys.MDPApp.Concierge.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GloProxSys.MDPApp.Concierge.Controllers
{
    public class AccountController : Controller
    {
        int _casino = 1;
        IAPIDataService _data;

        public AccountController(IAPIDataService data)
        {
            _data = data;
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var casino = _data.CasinoSettings(_casino).Result;
            var staff = _data.AllStaff(_casino).Result;

            var data = new LoginPageModel
            {
                CasinoID = _casino,
                CasinoName = casino.Name,
                CasinoLogo = string.IsNullOrWhiteSpace(casino.Logo) ? string.Format("casino-{0}-logo.png", _casino) : casino.Logo,
                CasinoTheme = string.IsNullOrWhiteSpace(casino.Theme) ? "default" : casino.Theme,
                StaffList = staff.Select(x => new LoginPageModel.StaffPerson { ID = x.StaffID, Name = x.FirstName + " " + x.LastName}),
                ReturnUrl = ReturnUrl,
            };
            return View(data);
        }
         
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            var ReturnUrl = Request.Form["ReturnUrl"];
            if (string.IsNullOrWhiteSpace(ReturnUrl)) ReturnUrl = @"\";

            var staff_id = Request.Form["staff_id"];
            if (string.IsNullOrWhiteSpace(staff_id)) staff_id = "1";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, staff_id)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return Redirect(ReturnUrl);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}