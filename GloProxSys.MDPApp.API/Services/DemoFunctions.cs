using GloProxSys.MDPApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloProxSys.MDPApp.API.Services
{
    public class DemoFunctions
    {
        IDataService _data;
        public DemoFunctions(IDataService data)
        {
            _data = data;
        }

        public void UpdatePlayerBalance(int casino, int player)
        {
            string sql = "SELECT b.* FROM PlayerBalance b WHERE b.CasinoID = @CasinoID AND b.PlayerID = @PlayerID AND b.Date = @Date";
            var bal = _data.Get<PlayerBalance>(sql, new { CasinoID = casino, PlayerID = player, Date = DateTime.UtcNow.Date });

            if (bal == null)
            {
                _data.Save("INSERT INTO PlayerBalance (CasinoID, PlayerID) VALUES (@CasinoID, @PlayerID)", new { CasinoID = casino, PlayerID = player });
                bal = new PlayerBalance();
            }

            var r = new Random();
            double jackpot = 0;
            if (r.Next(1, 10) == 4) jackpot = 1000 * r.NextDouble();

            bal.Points += r.Next(1, 100);
            bal.Comps += r.Next(1, 15);
            bal.FreeSpins += r.Next(1, 10);
            bal.CoinIn += (100 * r.NextDouble());
            bal.CoinOut += (100 * r.NextDouble() + jackpot);
            bal.TheoreticalWin = r.Next(600, 1500);
            bal.ActualWin = (bal.CoinOut - bal.CoinIn);
            bal.Jackpot = jackpot > 0;

            var u_sql = "UPDATE PlayerBalance SET "
                      + "Points = @Points, "
                      + "Comps = @Comps, "
                      + "FreeSpins = @FreeSpins, "
                      + "CoinIn = @CoinIn, "
                      + "CoinOut = @CoinOut,  "
                      + "TheoreticalWin = @TheoreticalWin, "
                      + "ActualWin = @ActualWin, "
                      + "Jackpot = @Jackpot "
                      + "WHERE CasinoID = @CasinoID AND PlayerID = @PlayerID AND Date = @Date";

            _data.Save(u_sql, new { bal.Points, bal.Comps, bal.FreeSpins, bal.CoinIn, bal.CoinOut, bal.TheoreticalWin, bal.ActualWin, Jackpot = (bal.Jackpot ? 1 : 0), CasinoID = casino, PlayerID = player, Date = DateTime.UtcNow.Date });
        }

    }
}
