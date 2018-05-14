using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GloProxSys.MDPApp.API.Services;
using GloProxSys.MDPApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GloProxSys.MDPApp.API.Controllers
{
    [Produces("application/json")]
    [Route("{casino}/Staff")]
    public class StaffController : Controller
    {
        IDataService _data;

        public StaffController(IDataService data)
        {
            _data = data;
        }

        [HttpGet]
        public IEnumerable<Staff> List(int casino)
        {
            //Querystring required????
            //Paging
            string sql = "SELECT * FROM Staff WHERE CasinoID = @CasinoID";
            var data = _data.List<Staff>(sql, new { CasinoID = casino });
            foreach(var d in data)
            {
                d.PhotoUrl = string.Format("https://s3.amazonaws.com/gps-app-resources/photos/staff-{0}-{1}.jpg", d.CasinoID, d.StaffID);
            }
            return data;

            /*
             * use soemthing like this to determine if photo exists
             * varlistObjectsRequest = new ListObjectsRequest()
                        .WithBucketName(bucketName)
                        .WithPrefix("blabla");
                if not, send placeholder url
                see https://docs.aws.amazon.com/AmazonS3/latest/dev/ListingObjectKeysUsingNetSDK.html
             */
        }

        [HttpGet("{id}")]
        public Staff Get(int casino, int id)
        {
            string sql = "SELECT * FROM Staff WHERE CasinoID = @CasinoID AND StaffID = @StaffID";
            var data = _data.Get<Staff>(sql, new { CasinoID = casino, StaffID = id });
            if (data != null) data.PhotoUrl = string.Format("https://s3.amazonaws.com/gps-app-resources/photos/staff-{0}-{1}.jpg", data.CasinoID, data.StaffID);
            return data;
        }

        [HttpPost("{id}/status")]
        public void UpdateStatus(int casino, int id, [FromBody] StaffStatusPostData data)
        {
            if (data != null && !string.IsNullOrWhiteSpace(data.Status))
            {
                string sql = "UPDATE Staff SET Status = @Status WHERE CasinoID = @CasinoID AND StaffID = @StaffID";
                _data.Save(sql, new { CasinoID = casino, StaffID = id, Status = data.Status });
            }
        }
    }
}
 