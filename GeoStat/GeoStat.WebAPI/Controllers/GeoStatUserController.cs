using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GeoStat.BussinessLogic.Interfaces;
using GeoStat.DTO;
using GeoStat.WebAPI.Filters;

namespace GeoStat.WebAPI.Controllers
{
    [AuthorisedIn]
    public class GeoStatUserController : BaseController<GeoStatUserDto>
    {
        public GeoStatUserController(
            IGeoStatUserDomainManager manager)
            : base(manager)
        {
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]GeoStatUserDto item)
        {
            var location = await DomainManager.InsertAsync(item);
            return this.Ok(location);
        }

        [HttpGet]
        public IQueryable<GeoStatUserDto> Get()
        {
            return this.Query();
        }
    }
}
