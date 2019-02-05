using GeoStat.DTO;
using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace GeoStat.BussinessLogic.Interfaces
{
    public interface IGeoStatUserDomainManager : IDomainManager<GeoStatUserDto> 
    {
        SingleResult<GeoStatUserDto> FindByName(string userName);
    }
}
