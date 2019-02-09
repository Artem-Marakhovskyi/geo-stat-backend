using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GeoStat.DTO;
using GeoStat.WebAPI.Filters;
using Microsoft.Azure.Mobile.Server.Tables;

namespace GeoStat.WebAPI.Controllers
{
    [AuthorisedIn]
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
        public IQueryable<GroupDto> Get()
        {
            return this.Query();
        }
    }
}
