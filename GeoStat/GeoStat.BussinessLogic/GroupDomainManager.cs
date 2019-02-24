using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using AutoMapper;
using GeoStat.BussinessLogic.Interfaces;

namespace GeoStat.BussinessLogic
{
    public class GroupDomainManager : 
        BaseDomainManager<GroupDto, Group>,
        IGroupDomainManager
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
                .Include("Groups")
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var groupList = new List<GroupModel>();
            
            foreach (var group in user.Groups)
            {
                groupList.Add(
                    new GroupModel
                    {
                        Id = group.Id,
                        Label = group.Label,
                        CreatorId = group.CreatorId,
                        CreatorName = group.Creator.Email,
                        Users = GetUsersOfGroup(group.Id)
                    });
            }

            return groupList;
        }

        public IEnumerable<GeoStatUserDto> GetUsersOfGroup(string groupId)
        {
            var users = _geoStatContext.GroupUsers
                .Where(g => g.GroupId == groupId)
                .Select(u => u.User);

            return Mapper.Map<GeoStatUserDto[]>(users);
        }
    }

    public class GroupModel
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public IEnumerable<GeoStatUserDto> Users { get; set; }
    }
}
