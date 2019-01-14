using System.Net.Http;
using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;

namespace GeoStat.BussinessLogic
{
    public class LocationDomainManager : BaseDomainManager<LocationDto, Location>
    {
        public LocationDomainManager(
            GeoStatContext geoStatContext,
            HttpRequestMessage requestMessage) 
            : base(geoStatContext, requestMessage)
        {
        }
    } 
}
