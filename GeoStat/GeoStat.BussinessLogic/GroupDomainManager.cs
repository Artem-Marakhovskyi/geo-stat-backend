using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace GeoStat.BussinessLogic
{
    public class GroupDomainManager : BaseDomainManager<GroupDto, Group>
    {
        private readonly GeoStatContext _geoStatContext;

        public GroupDomainManager(
            GeoStatContext geoStatContext,
            HttpRequestMessage requestMessage)
            : base(geoStatContext, requestMessage)
        {
            _geoStatContext = geoStatContext;
        }

        public IEnumerable<GroupModel> GetGroupsOfUser(string userId)
        {
            var user = _geoStatContext.GeoStatUsers
                .Where(u => u.UserId == userId)
                .FirstOrDefault();

            var groupList = new List<GroupModel>();
            
            foreach (var group in user.Groups)
            {
                var groupId = group.Id;

                groupList.Add(
                    new GroupModel
                    {
                        Id = groupId,
                        Label = group.Label,
                        CreatorId = group.CreatorId,
                        CreatorName = group.Creator.Email,
                        Users = GetUsersOfGroup(groupId)
                    });
            }

            return groupList;
        }

        public IEnumerable<GeoStatUser> GetUsersOfGroup(string groupId)
        {
            return _geoStatContext.GroupUsers
                .Where(g => g.GroupId == groupId)
                .Select(u => u.User);
        }
    }

    public class GroupModel
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public IEnumerable<GeoStatUser> Users { get; set; }
    }
}
