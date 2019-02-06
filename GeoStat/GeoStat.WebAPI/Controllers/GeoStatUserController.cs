using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GeoStat.BussinessLogic;
using GeoStat.BussinessLogic.Interfaces;
using GeoStat.DTO;
using GeoStat.WebAPI.Models;
using Microsoft.Azure.Mobile.Server.Tables;

namespace GeoStat.WebAPI.Controllers
{
    public class GeoStatUserController : BaseController<GeoStatUserDto>
    {
        public GeoStatUserController(
            IGeoStatUserDomainManager manager)
            : base(manager)
        {
        }

        [AuthorisedIn]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]GeoStatUserDto item, string token)
        {
            var location = await DomainManager.InsertAsync(item);
            return this.Ok(location);
        }

        [AuthorisedIn]
        [HttpGet]
        public IQueryable<GeoStatUserDto> Get(string token)
        {
            return this.Query();
        }
    }
}
