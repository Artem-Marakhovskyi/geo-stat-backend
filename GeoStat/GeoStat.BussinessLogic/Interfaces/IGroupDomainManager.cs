using GeoStat.DTO;
using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoStat.BussinessLogic.Interfaces
{
    public interface IGroupDomainManager : IDomainManager<GroupDto>
    {
        IEnumerable<GroupModel> GetGroupsOfUser(string userId);

        IEnumerable<GeoStatUserDto> GetUsersOfGroup(string groupId);
    }
}
