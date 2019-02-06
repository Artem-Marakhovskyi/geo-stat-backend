using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GeoStat.DTO;
using GeoStat.WebAPI.Models;
using Microsoft.Azure.Mobile.Server.Tables;

namespace GeoStat.WebAPI.Controllers
{
    public class LocationController : BaseController<LocationDto>
    {
        public LocationController(
            IDomainManager<LocationDto> manager)
            : base(manager)
        {
        }

        [AuthorisedIn]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]LocationDto item, string token)
        {
            var location = await DomainManager.InsertAsync(item);
            return this.Ok(location);
        }

        [AuthorisedIn]
        [HttpGet]
        public IQueryable<LocationDto> Get(string token)
        {
            return this.Query();
        }
    }
}
