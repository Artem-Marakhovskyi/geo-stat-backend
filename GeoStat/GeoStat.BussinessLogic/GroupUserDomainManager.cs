using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using System.Net.Http;

namespace GeoStat.BussinessLogic
{
    public class GroupUserDomainManager : BaseDomainManager<GroupUserDto, GroupUser>
    {
        public GroupUserDomainManager(
            GeoStatContext geoStatContext,
            HttpRequestMessage requestMessage)
            : base(geoStatContext, requestMessage)
        {
        }
    }
}
