using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using System.Net.Http;

namespace GeoStat.BussinessLogic
{
    public class GeoStatUserDomainManager : BaseDomainManager<GeoStatUserDto, GeoStatUser>
    {
        public GeoStatUserDomainManager(
            GeoStatContext geoStatContext,
            HttpRequestMessage requestMessage)
            : base(geoStatContext, requestMessage)
        {
        }
    }
}
