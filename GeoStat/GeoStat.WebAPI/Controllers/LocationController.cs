using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GeoStat.BussinessLogic;
using GeoStat.DTO;
using Microsoft.Azure.Mobile.Server.Tables;

namespace GeoStat.WebAPI.Controllers
{
    public class LocationController : BaseController<LocationDto>
    {
        private readonly ILocationDomainManager _locationDomainManager;
        public LocationController(
            ILocationDomainManager manager)
            : base(manager)
        {
            _locationDomainManager = manager;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]LocationDto item)
        {
            var location = await DomainManager.InsertAsync(item);
            return this.Ok(location);
        }

        [HttpGet]
        public IQueryable<LocationDto> Get()
        {
            return this.Query();
        }

        [HttpGet]
        [Route("Location/{userId}")]
        public IQueryable<LocationDto> GetById(string userId)
        {
            var locations = _locationDomainManager.GetLocationsByUserId("142b45ad60c94d61811f1849fd1c5559", userId);
            return null;
        }
    }
}
