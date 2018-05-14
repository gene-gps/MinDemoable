using GloProxSys.MDPApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloProxSys.MDPApp.APIService
{
    public interface IAPIDataService
    {

        Task<Casino> CasinoSettings(int casino);

        Task<IEnumerable<PlayerProfile>> OnsitePlayers(int casino);
        Task<PlayerProfile> PlayerProfile(int casino, int id);
        Task<PlayerBalance> PlayerBalance(int casino, int id);

        Task<IEnumerable<Staff>> AllStaff(int casino);
        Task<Staff> StaffPerson(int casino, int id);
        void UpdateStaffStatus(int casino, int staff_id, string status);

        Task<IEnumerable<PlayerNote>> PlayerNotes(int casino, int id);
        void AddNote(int casino, int staff_id, int player_id, string note);
    }

    public class ApiConfig
    {
        public string Url { get; set; }
        public string Key { get; set; }
    }
}
