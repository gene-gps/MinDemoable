using System;
using System.Collections.Generic;
using System.Text;

namespace GloProxSys.MDPApp.Models
{
    public class PlayerBalance
    {
        public DateTime Date { get; set; }
        public int Points { get; set; }
        public double Comps { get; set; }
        public double FreeSpins { get; set; }
        public double CoinIn { get; set; }
        public double CoinOut { get; set; }
        public double TheoreticalWin { get; set; }
        public double ActualWin { get; set; }
        public bool Jackpot { get; set; }
    }
}
