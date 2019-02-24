using System.Net.Http;
using System.Threading.Tasks;
using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using System.Linq.Expressions;
using System;

namespace GeoStat.BussinessLogic
{
    public class LocationDomainManager : 
        BaseDomainManager<LocationDto, Location>,
        ILocationDomainManager
    {
        private readonly GeoStatContext _geoStatContext;

        public LocationDomainManager(
            GeoStatContext geoStatContext,
            HttpRequestMessage requestMessage) 
            : base(geoStatContext, requestMessage)
        {
            _geoStatContext = geoStatContext;
        }

        public IEnumerable<LocationDto> GetLocationsByUserId(string tokenUserId, string id)
        {
            if (tokenUserId == id || CheckPermissionsForGroupMember(tokenUserId, id))
            {
                var locationsOfUser = _geoStatContext
                    .Locations
                    .Where(l => l.UserId == id)
                    .ToList();
                return Mapper.Map<LocationDto[]>(locationsOfUser);
            }

            return null;
        }
        //Mapper.EF
        public IEnumerable<LocationDto> GetLocationsByGroupId(string userId, string groupId)
        {
            //if (IsUserInGroup(userId, groupId))
            //{
                var groupMembersId = _geoStatContext.GroupUsers
                    .Where(u => u.GroupId == groupId)
                    .Select(u => u.UserId);

                var locationsOfGroupMembers = _geoStatContext.Locations
                    .Where(l => groupMembersId.Contains(l.UserId))
                    .ToList();

                return Mapper.Map<LocationDto[]>(locationsOfGroupMembers);
            //}

           // return null;
        }

        private Expression<Func<string, bool>> IsUserInGroup(string userId)
        {
            return (groupId) => _geoStatContext.GroupUsers
                .Where(u => u.GroupId == groupId)
                .Select(u => u.UserId)
                .Contains(userId);
        }

        private bool CheckPermissionsForGroupMember(string tokenUserId, string groupMemberId)
            => _geoStatContext
                .GroupUsers
                .Where(u => u.UserId == groupMemberId)
                .Select(u => u.GroupId)
                .Any(IsUserInGroup(tokenUserId));
    } 
}
