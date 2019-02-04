using System.Net.Http;
using System.Threading.Tasks;
using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using System.Linq;

namespace GeoStat.BussinessLogic
{
    public class LocationDomainManager : BaseDomainManager<LocationDto, Location>
    {
        private GeoStatContext _geoStatContext;

        public LocationDomainManager(
            GeoStatContext geoStatContext,
            HttpRequestMessage requestMessage) 
            : base(geoStatContext, requestMessage)
        {
            _geoStatContext = geoStatContext;
        }

        public IQueryable<Location> GetLocationsByUserId(string userId, string id)
        {
            if (userId == id || CheckPermissionsForGroupMember(userId, id))
            {
                var locationsOfUser = _geoStatContext.Locations.Where(l => l.UserId == id);
                return locationsOfUser;
            }

            return null;
        }

        public IQueryable<Location> GetLocationsByGroupId(string userId, string groupId)
        {
            if (CheckPermissionsForGroup(userId, groupId))
            {
                var groupMembersId = _geoStatContext.GroupUsers
                    .Where(u => u.GroupId == groupId)
                    .Select(u => u.UserId);

                var locationsOfGroupMembers = _geoStatContext.Locations
                    .Where(l => groupMembersId.Contains(l.UserId));

                return locationsOfGroupMembers;
            }

            return null;
        }

        private bool CheckPermissionsForGroup(string userId, string groupId)
        {
            var groupUsersId = _geoStatContext.GroupUsers
                .Where(u => u.GroupId == groupId)
                .Select(u => u.UserId);

            if (groupUsersId.Contains(userId)) return true;

            return false;
        }

        private bool CheckPermissionsForGroupMember(string userId, string groupMemberId)
        {
            var groupsOfMember = _geoStatContext.GroupUsers
                .Where(u => u.UserId == groupMemberId)
                .Select(u => u.GroupId);

            foreach (var groupId in groupsOfMember)
            {
                if (CheckPermissionsForGroup(userId, groupId))
                {
                    return true;
                }
            }

            return false;
        }
    } 
}
