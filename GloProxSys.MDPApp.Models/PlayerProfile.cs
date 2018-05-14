using System;
using System.Collections.Generic;
using System.Text;

namespace GloProxSys.MDPApp.Models
{
    public class PlayerProfile
    {
        public int PlayerID { get; set; }
        public int CasinoID { get; set; }
        public int ClubLevelID { get; set; }
        public string ClubLevelName { get; set; }
        public int TierID { get; set; }
        public string TierName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string MemberNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string PreferredName { get; set; }
        public int PointBalance { get; set; }
        public double FreeSpins { get; set; }
        public double Comps { get; set; }
        public double CoinIn { get; set; }
        public double CoinOut { get; set; }
        public double TheoreticalWin { get; set; }
        public double ActualWin { get; set; }
        public bool Jackpot { get; set; }
        public string PhotoUrl { get; set; }
        public string Profile { get; set; }
    }
}
