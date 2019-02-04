using GeoStat.DTO;
using GeoStat.Entities;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoStat.BussinessLogic.Interfaces
{
    public interface IAccountDomainManager
    { 
        Task<string> Register(AuthorisationUserDTO userDTO);

        Task<string> Authorise(AuthorisationUserDTO userDTO);

        Task<string> FindUserId(AuthorisationUserDTO userDTO);
    }
}
