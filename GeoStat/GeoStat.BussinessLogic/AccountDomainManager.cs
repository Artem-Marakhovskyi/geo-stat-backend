using GeoStat.BussinessLogic.Helpers;
using GeoStat.BussinessLogic.Interfaces;
using GeoStat.DTO;
using GeoStat.Entities;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GeoStat.BussinessLogic.Helpers.Response;

namespace GeoStat.BussinessLogic
{
    public class AccountDomainManager : IAccountDomainManager
    {
        private readonly IGeoStatUserDomainManager _geostatUserDomainManager;
        private readonly UserDomainManager _userDomainManager;

        public AccountDomainManager(IGeoStatUserDomainManager geostatUserDomainManager, UserDomainManager userDomainManager)
        {
            _geostatUserDomainManager = geostatUserDomainManager;
            _userDomainManager = userDomainManager;
        }

        public async Task<string> FindUserId(AuthorisationUserDTO userDTO)
        {
            var userResult = await _userDomainManager.FindByNameAsync(userDTO.Email);
            return userResult.Id;
        }

        public async Task<Response> Authorise(AuthorisationUserDTO userDTO)
        {
            var userResult = await _userDomainManager.FindByNameAsync(userDTO.Email);

            if (userResult == null)
            {
                return new Response(Responses.UserNotFound, "user not found");
            }

            if (_userDomainManager.CheckPassword(userResult, userDTO.Password))
            {
                return new Response(Responses.Success, string.Empty);
            }
            else
            {
                return new Response(Responses.InvalidPassword, "invalid password");
            }
        }

        public async Task<Response> Register(AuthorisationUserDTO userDTO)
        {
            var customUser = new User
            {
                UserName = userDTO.Email,
                Email = userDTO.Email
            };

            var userResult = await _userDomainManager.CreateAsync(customUser, userDTO.Password);

            if (userResult.Errors.Any())
            {
                var errors = string.Join(" ", userResult.Errors);
                return new Response(Responses.ModelErrors, errors);
            }

            var user = _userDomainManager.FindByName(userDTO.Email);

            var geostatUserDto = new GeoStatUserDto
            {
                Email = userDTO.Email,
                UserId = user.Id
            };

            var geostatUserResult = await _geostatUserDomainManager.InsertAsync(geostatUserDto);
            var geostatUserId = _geostatUserDomainManager.FindByName(userDTO.Email).Queryable.First().Id;

            user.GeoStatUser_Id = geostatUserId;

            await _userDomainManager.UpdateAsync(user);

            return new Response(Responses.Success, string.Empty);
        }
    }
}
