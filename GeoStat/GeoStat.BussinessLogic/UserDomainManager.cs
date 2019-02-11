using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace GeoStat.BussinessLogic
{
    public class GeoStatUserDomainManager : BaseDomainManager<GeoStatUserDto, GeoStatUser>
    {
        private readonly GeoStatContext _geoStatContext;

        public GeoStatUserDomainManager(
            GeoStatContext geoStatContext,
            HttpRequestMessage requestMessage)
            : base(geoStatContext, requestMessage)
        {
            _geoStatContext = geoStatContext;
        }
    }
}
