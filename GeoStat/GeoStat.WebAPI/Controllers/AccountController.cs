using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GeoStat.BussinessLogic;
using GeoStat.BussinessLogic.Interfaces;
using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using GeoStat.WebAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Azure.Mobile.Server.Tables;
using Newtonsoft.Json.Linq;

namespace GeoStat.WebAPI.Controllers
{
    [Route("api/account")]
    public class AccountController : ApiController
    {
        private readonly IAccountDomainManager _accountDomainManager;

        public AccountController()
        {

        }

        public AccountController(IAccountDomainManager accountDomainManager)
        {
            _accountDomainManager = accountDomainManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/account/register/post")]
        public async Task<HttpResponseMessage> Register([FromBody]UserDTO model)
        {
            if(ModelState.IsValid)
            {
                var registration = await _accountDomainManager.Register(model);
                if (registration != "ok")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, registration);
                }

                var registratedUserId = await _accountDomainManager.FindUserId(model);

                var tokenGenerator = new TokenGenerator();
                var token = tokenGenerator.GenerateToken(model.Email, registratedUserId);

                var jsonGenerator = new JsonGenerator();
                var json = jsonGenerator.GenerateJson(token);

                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/account/authorise/post")]
        public async Task<HttpResponseMessage> Authorise([FromBody]AuthorisationUserDTO model)
        {

            if(ModelState.IsValid)
            {
                var authorisation = await _accountDomainManager.Authorise(model);

                if (authorisation != "ok")
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, authorisation);
                }

                var authorisedUserId = await _accountDomainManager.FindUserId(model);

                var tokenGenerator = new TokenGenerator();
                var token = tokenGenerator.GenerateToken(model.Email, authorisedUserId);

                var jsonGenerator = new JsonGenerator();
                var json = jsonGenerator.GenerateJson(token);

                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}