using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GeoStat.DTO;
using GeoStat.Entities;
using GeoStat.WebAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Azure.Mobile.Server.Tables;

namespace GeoStat.WebAPI.Controllers
{
    [Route("api/values")]
    public class AccountController : ApiController
    {

        [System.Web.Mvc.HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]UserDTO item)
        {
            var user = new User();
            return this.Ok(user);
        }
    }
}