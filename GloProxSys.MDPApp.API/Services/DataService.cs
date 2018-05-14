using GloProxSys.MDPApp.API.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace GloProxSys.MDPApp.API.Services
{
    public class DataService : IDataService
    {
        DataConfig _config;
        public DataService(DataConfig config)
        {
            _config = config;
        }


        public IEnumerable<T> List<T>(string sql, object parameters, int page = 1, int size = 0)
        {
            IEnumerable<T> data = new List<T>();
            using (var db = new SqlConnection(this.ConnectionString))
            {
                data = db.Query<T>(sql, parameters);
            }
            return data;
        }

        public T Get<T>(string sql, object parameters)
        {
            T data = default(T);
            using (var db = new SqlConnection(this.ConnectionString))
            {
                data = db.QuerySingleOrDefault<T>(sql, parameters);
            }
            return data;
        }

        public void Save(string sql, object parameters)
        {
            int n_rows = 0;
            using (var db = new SqlConnection(this.ConnectionString))
            {
                n_rows = db.Execute(sql, parameters);
            }
        }


        private string ConnectionString
        {
            get => string.Format("Server={0};Database=;User Id={1};Password={2};Connect Timeout=30;", _config.Server, _config.UserName, _config.Password);
        }
    }
}
