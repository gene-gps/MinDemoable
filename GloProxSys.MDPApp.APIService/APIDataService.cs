using GloProxSys.MDPApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GloProxSys.MDPApp.APIService
{
    public class APIDataService : IAPIDataService, IDisposable
    {
        //Can't reuse httpclient in same request.  see https://github.com/dotnet/corefx/issues/25800
        //HttpClient _http;

        ApiConfig _config;
        public APIDataService(ApiConfig config)
        {
            _config = config;
            //_http = new HttpClient();
            //_http.BaseAddress = new Uri(config.Url);
            //_http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //if (!string.IsNullOrWhiteSpace(config.Key))
            //{
            //    _http.DefaultRequestHeaders.Authorization = (new AuthenticationHeaderValue("ApiKey", config.Key));
            //}
        }

        private HttpClient GetHttpClient()
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri(_config.Url);
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrWhiteSpace(_config.Key))
            {
                http.DefaultRequestHeaders.Authorization = (new AuthenticationHeaderValue("ApiKey", _config.Key));
            }
            return http;
        }

        public async Task<Casino> CasinoSettings(int casino)
        {
            Casino data = null;

            using (var http = GetHttpClient())
            {
                var r = await http.GetAsync(casino + "/casino");
                if (r.IsSuccessStatusCode)
                {
                    data = await r.Content.ReadAsStringAsync().ContinueWith(x =>
                    {
                        return JsonConvert.DeserializeObject<Casino>(x.Result);
                    });
                }
            }
   
            return data;
        }

        public async Task<IEnumerable<PlayerProfile>> OnsitePlayers(int casino)
        {
            IEnumerable<PlayerProfile> data = new List<PlayerProfile>();

            using (var http = GetHttpClient())
            {
                var r = await http.GetAsync(casino + "/player");
                if (r.IsSuccessStatusCode)
                {
                    data = await r.Content.ReadAsStringAsync().ContinueWith(x =>
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<PlayerProfile>>(x.Result);
                    });
                }
            }
            foreach(var p in data)
            {
                var bal = PlayerBalance(casino, p.PlayerID).Result;
                if (bal != null)
                {
                    p.PointBalance = bal.Points;
                    p.Comps = bal.Comps;
                    p.FreeSpins = bal.FreeSpins;
                    p.CoinIn = bal.CoinIn;
                    p.CoinOut = bal.CoinOut;
                    p.TheoreticalWin = bal.TheoreticalWin;
                    p.ActualWin = bal.ActualWin;
                    p.Jackpot = bal.Jackpot;
                }
            }
            return data;
        }

        public async Task<PlayerProfile> PlayerProfile(int casino, int id)
        {
            PlayerProfile data = null;

            using (var http = GetHttpClient())
            {
                var r = await http.GetAsync(casino + "/player/" + id + "/profile");
                if (r.IsSuccessStatusCode)
                {
                    data = await r.Content.ReadAsStringAsync().ContinueWith(x =>
                    {
                        return JsonConvert.DeserializeObject<PlayerProfile>(x.Result);
                    });
                }
            }
            var bal = PlayerBalance(casino, id).Result;
            if (data != null && bal != null)
            {
                data.PointBalance = bal.Points;
                data.Comps = bal.Comps;
                data.FreeSpins = bal.FreeSpins;
                data.CoinIn = bal.CoinIn;
                data.CoinOut = bal.CoinOut;
                data.TheoreticalWin = bal.TheoreticalWin;
                data.ActualWin = bal.ActualWin;
                data.Jackpot = bal.Jackpot;
            }
            return data;
        }

        public async Task<PlayerBalance> PlayerBalance(int casino, int id)
        {
            PlayerBalance data = null;

            using (var http = GetHttpClient())
            {
                var r = await http.GetAsync(casino + "/player/" + id + "/balance");
                if (r.IsSuccessStatusCode)
                {
                    data = await r.Content.ReadAsStringAsync().ContinueWith(x =>
                    {
                        return JsonConvert.DeserializeObject<PlayerBalance>(x.Result);
                    });
                }
            }
 
            return data;
        }

        public async Task<IEnumerable<PlayerNote>> PlayerNotes(int casino, int id)
        {
            IEnumerable<PlayerNote> data = new List<PlayerNote>();

            using (var http = GetHttpClient())
            {
                var r = await http.GetAsync(casino + "/player/" + id + "/notes");
                if (r.IsSuccessStatusCode)
                {
                    data = await r.Content.ReadAsStringAsync().ContinueWith(x =>
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<PlayerNote>>(x.Result);
                    });
                }
            }

            return data;
        }


        public async Task<Staff> StaffPerson(int casino, int id)
        {
            Staff data = null;

            using (var http = GetHttpClient())
            {
                var r = await http.GetAsync(casino + "/staff/" + id);
                if (r.IsSuccessStatusCode)
                {
                    data = await r.Content.ReadAsStringAsync().ContinueWith(x =>
                    {
                        return JsonConvert.DeserializeObject<Staff>(x.Result);
                    });
                }
            }

            return data;
        }

        public async Task<IEnumerable<Staff>> AllStaff(int casino)
        {
            IEnumerable<Staff> data = new List<Staff>();

            using (var http = GetHttpClient())
            {
                var r = await http.GetAsync(casino + "/staff");
                if (r.IsSuccessStatusCode)
                {
                    data = await r.Content.ReadAsStringAsync().ContinueWith(x =>
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<Staff>>(x.Result);
                    });
                }
            }

            return data;
        }

        public async void UpdateStaffStatus(int casino, int staff_id, string status)
        {
            var data = new StaffStatusPostData
            {
                Status = status,
            };
            var content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            using (var http = GetHttpClient())
            {
                var r = await http.PostAsync(casino + "/staff/" + staff_id + "/status", content);
                if (!r.IsSuccessStatusCode)
                {
                    throw new Exception(r.ReasonPhrase);
                }
            }
        }

        public async void AddNote(int casino, int staff_id, int player_id, string note)
        {
            var data = new NotePostData
            {
                StaffID = staff_id,
                Content = note,
            };
            var content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            using (var http = GetHttpClient())
            {
                var r = await http.PostAsync(casino + "/player/" + player_id + "/notes", content);
                if (!r.IsSuccessStatusCode)
                {
                    throw new Exception(r.ReasonPhrase);
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //_http.Dispose();
                    //_http = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
