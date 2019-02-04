using GeoStat.BussinessLogic.Interfaces;
using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using Microsoft.AspNet.Identity;
using System.Linq;
using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.OData.Query;
using System.Web.Http;

namespace GeoStat.BussinessLogic
{
    public class AccountDomainManager : IAccountDomainManager
    {
        private readonly GeoStatUserDomainManager _geostatUserDomainManager;
        private readonly UserDomainManager _userDomainManager;

        public AccountDomainManager(GeoStatUserDomainManager geostatUserDomainManager, UserDomainManager userDomainManager)
        {
            _geostatUserDomainManager = geostatUserDomainManager;
            _userDomainManager = userDomainManager;
        } 

        public async Task<string> FindUserId(AuthorisationUserDTO userDTO)
        {
            var userResult = await _userDomainManager.FindByNameAsync(userDTO.Email);
            return userResult.Id;
        }

        public async Task<string> Authorise(AuthorisationUserDTO userDTO)
        {
            var userResult = await _userDomainManager.FindByNameAsync(userDTO.Email);

            if (userResult == null)
            {
                return "user not found";
            }

            if (_userDomainManager.CheckPassword(userResult, userDTO.Password))
            {
                return "ok";
            }
            else
            {
                return "invalid password";
            }
        }

        public async Task<string> Register(AuthorisationUserDTO userDTO)
        {
            var userResult = await _userDomainManager.CreateAsync(new User { UserName = userDTO.Email,
                                                                    Email = userDTO.Email }, userDTO.Password);
            if(userResult.Errors.Count() != 0)
            {
                string response = String.Empty;
                foreach (var item in userResult.Errors)
                {
                    response += item + " ";
                }
                return response;
            }
            
            var user =  _userDomainManager.FindByName(userDTO.Email);

            var geostatUserResult = await _geostatUserDomainManager.InsertAsync(new GeoStatUserDto { Email = userDTO.Email, UserId = user.Id });
            var geostatUserId = _geostatUserDomainManager.FindByName(userDTO.Email).Queryable.First().Id;

            user.GeoStatUser_Id = geostatUserId;

            var userres = await _userDomainManager.UpdateAsync(user);

            return "ok";
        }
    }
}
