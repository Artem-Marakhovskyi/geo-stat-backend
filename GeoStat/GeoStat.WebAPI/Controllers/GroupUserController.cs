using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GeoStat.DTO;
using GeoStat.WebAPI.Filters;
using Microsoft.Azure.Mobile.Server.Tables;

namespace GeoStat.WebAPI.Controllers
{
    [AuthorisedIn]
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

        [HttpGet]
        public IQueryable<GroupUserDto> Get()
        {
            return this.Query();
        }
    }
}
