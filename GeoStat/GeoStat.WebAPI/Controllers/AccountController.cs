using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GeoStat.BussinessLogic.Access;
using GeoStat.DTO;
using Newtonsoft.Json;

namespace GeoStat.WebAPI.Controllers
{
    [Route("api/account")]
    public class AccountController : ApiController
    {
        private readonly IAccountDomainManager _accountDomainManager;
        
        public AccountController(IAccountDomainManager accountDomainManager)
        {
            _accountDomainManager = accountDomainManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/account/register")]
        public async Task<HttpResponseMessage> Register([FromBody]UserDTO model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var registerResult = await _accountDomainManager.Register(model);

                    return Request.CreateResponse(
                        HttpStatusCode.OK,
                        registerResult);
                }
                catch (InvalidOperationException ex)
                {
                    return Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        ex);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/account/auth")]
        public async Task<HttpResponseMessage> Authorise([FromBody]AuthorisationUserDTO model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var authResult = await _accountDomainManager.Authorise(model);

                    return Request.CreateResponse(
                        HttpStatusCode.OK,
                        authResult);
                }
                catch (InvalidOperationException ex)
                {
                    return Request.CreateBadRequestResponse(
                        "Error while authorization process",
                        ex);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}