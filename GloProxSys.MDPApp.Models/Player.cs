using System;

namespace GloProxSys.MDPApp.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        public int CasinoID { get; set; }
        public int ClubLevelID { get; set; }
        public string ClubLevelName { get; set; }
        public int TierID { get; set; }
        public string TierName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MemberNumber { get; set; }
        //public double? PointBalance { get; set; }
        //public double? FreeSpins { get; set; }
        //public double CoinIn { get; set; }
        //public double CoinOut { get; set; }
        //public double Winnings { get; set; }
        //public bool Jackpot { get; set; }
    }

    public class PlayerNote
    {
        public int PlayerID { get; set; }
        public int StaffID { get; set; }
        public string StaffName { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
    }
    public class NotePostData {
        public int StaffID { get; set; }
        public string Content { get; set; }
    }
}
