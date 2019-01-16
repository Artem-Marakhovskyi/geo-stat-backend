using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using System.Net.Http;

namespace GeoStat.BussinessLogic
{
    public class GroupDomainManager : BaseDomainManager<GroupDto, Group>
    {
        public GroupDomainManager(
            GeoStatContext geoStatContext,
            HttpRequestMessage requestMessage)
            : base(geoStatContext, requestMessage)
        {
        }
    }
}
