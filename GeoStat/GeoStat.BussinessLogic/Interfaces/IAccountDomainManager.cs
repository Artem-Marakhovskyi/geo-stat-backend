using GeoStat.BussinessLogic.Helpers;
using GeoStat.DTO;
using GeoStat.Entities;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static GeoStat.BussinessLogic.Helpers.Response;

namespace GeoStat.BussinessLogic.Interfaces
{
    public interface IAccountDomainManager
    { 
        Task<Response> Register(AuthorisationUserDTO userDTO);

        Task<Response> Authorise(AuthorisationUserDTO userDTO);

        Task<string> FindUserId(AuthorisationUserDTO userDTO);
    }
}
