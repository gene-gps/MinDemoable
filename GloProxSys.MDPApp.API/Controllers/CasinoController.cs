using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloProxSys.MDPApp.API.Services;
using GloProxSys.MDPApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GloProxSys.MDPApp.API.Controllers
{
    [Produces("application/json")]
    [Route("{casino}/Casino")]
    public class CasinoController : Controller
    {
        IDataService _data;

        public CasinoController(IDataService data)
        {
            _data = data;
        }


        [HttpGet]
        public Casino Get(int casino)
        {
            Casino data = null;
            string sql = "SELECT * FROM Casino WHERE CasinoID = @CasinoID";
            data = _data.Get<Casino>(sql, new { CasinoID = casino });
            if(data != null)
            {
                string t_sql = "SELECT * from ClubLevel Where CasinoID = @CasinoID ORDER BY Sort";
                data.ClubLevels = _data.List<ClubLevel>(t_sql, new { CasinoID = casino });
            }
            return data;
        }

    }
}