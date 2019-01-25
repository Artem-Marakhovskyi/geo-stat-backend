using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using GeoStat.WebAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Azure.Mobile.Server.Tables;

namespace GeoStat.WebAPI.Controllers
{
    [Route("api/account")]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("api/account/register/get")]
        public async Task<IdentityResult> Register()
        {
            var context = new GeoStatContext("MS_TableConnectionString");
            var manager = new UserManager<User>(new UserStore<User>(context));
            var user = await manager.CreateAsync(new User { Email = "1@gmail.com", UserName = "1@gmail.com" }, "Password123!");
            return user;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/account/register/post")]
        public async Task<string> Register([FromBody]UserDTO model)
        {
            var context = new GeoStatContext("MS_TableConnectionString");
            if (ModelState.IsValid)
            {
                var existingUser = context.Users.FirstOrDefault(x => x.Email == model.Email);
                if (existingUser != null)
                {
                    return "User already exist.";
                }

                var existingGeostatUser = context.GeoStatUsers.FirstOrDefault(x => x.Email == model.Email);
                if (existingGeostatUser != null)
                {
                    return "GeostatUser already exist.";
                }

                var user = new User { UserName = model.Email };

                var userManager = new UserManager<User>(new UserStore<User>(context));
                var userCreation = await userManager.CreateAsync(user, model.Password);

                var geostatUserManager = new GeoStatUserManager(new UserStore<User>(context));
                var geostatUser = context.GeoStatUsers.Add(new GeoStatUser { Email = model.Email, User = user });

                var tokenGenerator = new TokenGenerator();
                var token = TokenGenerator.GenerateToken(model.Email);

                context.SaveChanges();

                return token;
            }
            else
            {
                return "invalid model";
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/account/authorise/post")]
        public string Authorise([FromBody]UserDTO model)
        {
            var context = new GeoStatContext("MS_TableConnectionString");
            if(ModelState.IsValid)
            {
                var existingUser = context.Users.FirstOrDefault(x => x.Email == model.Email);
                var existingGeostatUser = context.GeoStatUsers.FirstOrDefault(x => x.Email == model.Email);
                if(existingUser != null && existingGeostatUser != null)
                {
                    var tokenGenerator = new TokenGenerator();
                    var token = TokenGenerator.GenerateToken(model.Email);
                    return token;
                }
                else
                {
                    return "user not found";
                }
            }
            else
            {
                return "model is not valid";
            }
        }

        [Route("api/account/register/test")]
        [System.Web.Mvc.HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]UserDTO model)
        {
            var user = new User();
            return this.Ok(user);
        }
    }
}