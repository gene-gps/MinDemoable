using GloProxSys.MDPApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloProxSys.MDPApp.API.Services
{
    public interface IDataService
    {

        IEnumerable<T> List<T>(string sql, object parameters, int page = 1, int size = 0);
        T Get<T>(string sql, object parameters);
        void Save(string sql, object parameters);

    }
}
