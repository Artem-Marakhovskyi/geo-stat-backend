using GeoStat.DTO;
using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoStat.BussinessLogic
{
    public interface ILocationDomainManager : IDomainManager<LocationDto>
    {
        IEnumerable<LocationDto> GetLocationsByUserId(string tokenUserId, string id);

        IEnumerable<LocationDto> GetLocationsByGroupId(string userId, string groupId);
    }
}
