using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloProxSys.MDPApp.API.Services;
using GloProxSys.MDPApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace GloProxSys.MDPApp.API.Controllers
{
    [Produces("application/json")]
    [Route("{casino}/Player")]
    public class PlayerController : Controller
    {
        IDataService _data;

        public PlayerController(IDataService data)
        {
            _data = data;
        }

        [HttpGet]
        public IEnumerable<PlayerProfile> Players(int casino)
        {
            //Querystring required????
            //Paging
            string sql = "SELECT p.*, c.Name as ClubLevelName, t.Name as TierName "
                       + "FROM Player p "
                       + "LEFT OUTER JOIN ClubLevel c ON (c.ClubLevelID = p.ClubLevelID) "
                       + "LEFT OUTER JOIN Tier t ON (t.TierID = p.TierID) "
                       + "WHERE p.CasinoID = @CasinoID "
                       + "ORDER BY p.LastName, p.FirstName";

            //Add balances?
            return _data.List<PlayerProfile>(sql, new { CasinoID = casino });
        }

        [HttpGet("{id}")]
        public Player Player(int casino, int id)
        {
            string sql = "SELECT p.*, c.Name as ClubLevelName, t.Name as TierName "
                       + "FROM Player p "
                       + "LEFT OUTER JOIN ClubLevel c ON (c.ClubLevelID = p.ClubLevelID) "
                       + "LEFT OUTER JOIN Tier t ON (t.TierID = p.TierID) "
                       + "WHERE p.CasinoID = @CasinoID "
                       + "AND PlayerID = @PlayerID";

            return _data.Get<Player>(sql, new { CasinoID = casino, PlayerID = id });
        }

        [HttpGet("{id}/Balance")]
        public PlayerBalance Balance(int casino, int id)
        {
            new DemoFunctions(_data).UpdatePlayerBalance(casino, id);

            string sql = "SELECT TOP 1 b.* FROM PlayerBalance b WHERE b.CasinoID = @CasinoID AND b.PlayerID = @PlayerID ORDER BY b.Date DESC";
            var bal = _data.Get<PlayerBalance>(sql, new { CasinoID = casino, PlayerID = id });
            if (bal == null) bal = new PlayerBalance();
            if (bal.Date != DateTime.UtcNow.Date)
            {
                _data.Save("INSERT INTO PlayerBalance (CasinoID, PlayerID, Points, Comps, FreeSpins, CoinIn, CoinOut, TheoreticalWin, ActualWin) VALUES (@CasinoID, @PlayerID, @Points, @FreeSpins, @CoinIn, @CoinOut, @Winnings)", 
                    new { CasinoID = casino, PlayerID = id, bal.Points, bal.Comps, bal.FreeSpins, bal.CoinIn, bal.CoinOut, bal.TheoreticalWin, bal.ActualWin }
                );
            }
            return bal;
        }

        [HttpGet("{id}/Profile")]
        public PlayerProfile Profile(int casino, int id)
        {
            string sql = "SELECT p.*, c.Name as ClubLevelName, t.Name as TierName "
                       + "FROM Player p "
                       + "LEFT OUTER JOIN ClubLevel c ON (c.ClubLevelID = p.ClubLevelID) "
                       + "LEFT OUTER JOIN Tier t ON (t.TierID = p.TierID) "
                       + "WHERE p.CasinoID = @CasinoID "
                       + "AND PlayerID = @PlayerID";

            return _data.Get<PlayerProfile>(sql, new { CasinoID = casino, PlayerID = id });
        }

        [HttpGet("{id}/Notes")]
        public IEnumerable<PlayerNote> Notes(int casino, int id)
        {
            //Paging
            string sql = "SELECT n.*, (s.FirstName + ' ' + s.LastName) as StaffName FROM PlayerNote n LEFT OUTER JOIN Staff s ON (s.StaffID = n.StaffID) WHERE n.PlayerID = @PlayerID ORDER BY n.Date desc";
            return _data.List<PlayerNote>(sql, new { CasinoID = casino, PlayerID = id });
        }
        [HttpPost("{id}/Notes")]
        public void AddNote(int casino, int id, [FromBody]NotePostData data)
        {
            if (data == null) throw new Exception("Missing Note Data");
            string sql = "INSERT INTO PlayerNote (PlayerID, StaffID, Content) VALUES(@PlayerID, @StaffID, @Content)";
            _data.Save(sql, new { PlayerID = id, data.StaffID, data.Content });
        }


        //[HttpGet("{id}/Incentives")]
        //public PlayerProfile Incentives(int casino, int id)
        //{
        //    string sql = "SELECT p.*, t.Name as TierName FROM Player p LEFT OUTER JOIN Tier t ON (t.TierID = p.TierID) WHERE p.CasinoID = @CasinoID AND PlayerID = @PlayerID";
        //    return _data.Get<PlayerProfile>(sql, new { CasinoID = casino, PlayerID = id });
        //}
    }
}