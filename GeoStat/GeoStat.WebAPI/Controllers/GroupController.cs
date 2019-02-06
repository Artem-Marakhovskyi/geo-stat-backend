using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GeoStat.DTO;
using GeoStat.WebAPI.Models;
using Microsoft.Azure.Mobile.Server.Tables;

namespace GeoStat.WebAPI.Controllers
{
    public class GroupController : BaseController<GroupDto>
    {
        public GroupController(
            IDomainManager<GroupDto> manager)
            : base(manager)
        {
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]GroupDto item)
        {
            var location = await DomainManager.InsertAsync(item);
            return this.Ok(location);
        }

        [HttpGet]
        public IQueryable<GroupDto> Get(string accessToken, string refreshToken)
        {
            var tokenCheck = new TokenChecker().CheckToken(accessToken, refreshToken);
            var tokenGenerator = new TokenGenerator();
            if(tokenCheck)
            {
                var possibleRefresh = tokenGenerator.RefreshAccessToken(accessToken, refreshToken);
                if (possibleRefresh.CustomResponse == TokenGenerationResponses.Responses.Success)
                {
                    accessToken = possibleRefresh.ResponseString;
                }
                else
                {
                    //bad request USER IS NOT ALLOWED TO THIS ACTION: REFRESH TOKEN INVALID
                }
            }
            return this.Query();
        }
    }
}
