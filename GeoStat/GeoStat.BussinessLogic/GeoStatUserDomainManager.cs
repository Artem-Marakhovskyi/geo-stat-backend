using GeoStat.BussinessLogic.Interfaces;
using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeoStat.BussinessLogic
{
    public class GeoStatUserDomainManager : BaseDomainManager<GeoStatUserDto, GeoStatUser>, IGeoStatUserDomainManager 
    {
        public GeoStatUserDomainManager(
            GeoStatContext geoStatContext,
            HttpRequestMessage requestMessage)
            : base(geoStatContext, requestMessage)
        {
        }

        public SingleResult<GeoStatUserDto> FindByName(string userName)
        {
            return LookupEntity(model => model.Email == userName);
        }
    }
}
