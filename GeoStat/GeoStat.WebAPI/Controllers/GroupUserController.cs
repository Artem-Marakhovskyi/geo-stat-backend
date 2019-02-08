using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GeoStat.DTO;
using GeoStat.WebAPI.Models;
using Microsoft.Azure.Mobile.Server.Tables;

namespace GeoStat.WebAPI.Controllers
{
    public class GroupUserController : BaseController<GroupUserDto>
    {
        public GroupUserController(
            IDomainManager<GroupUserDto> manager)
            : base(manager)
        {
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]GroupUserDto item)
        {
            var location = await DomainManager.InsertAsync(item);
            return this.Ok(location);
        }

        [AuthorisedIn]
        [HttpGet]
        public IQueryable<GroupUserDto> Get(string token)
        {
            return this.Query();
        }
    }
}
