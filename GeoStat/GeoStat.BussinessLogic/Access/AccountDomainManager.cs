using System;
using GeoStat.BussinessLogic.Interfaces;
using GeoStat.DTO;
using GeoStat.Entities;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;
using GeoStat.CrossCutting.Logger;

namespace GeoStat.BussinessLogic.Access
{
    public class AccountDomainManager : IAccountDomainManager
    {
        private readonly IGeoStatUserDomainManager _geostatUserDomainManager;
        private readonly UserIdentityDomainManager _userIdentityDomainManager;
        private readonly ITokenManager _tokenManager;
        private readonly IGeoStatLogger _logger;

        public AccountDomainManager(
            IGeoStatUserDomainManager geostatUserDomainManager,
            UserIdentityDomainManager userIdentityDomainManager,
            ITokenManager tokenManager,
            IGeoStatLogger logger)
        {
            _geostatUserDomainManager = geostatUserDomainManager;
            _userIdentityDomainManager = userIdentityDomainManager;
            _tokenManager = tokenManager;
            _logger = logger;
        }

        public async Task<AuthResult> Authorise(AuthorisationUserDTO userDTO)
        {
            var user = await _userIdentityDomainManager.FindByNameAsync(userDTO.Email);

            if (user == null)
            {
                const string noUsersWithEmail = "No users found with specified email";
                _logger.LogWarn(noUsersWithEmail);

                throw new InvalidOperationException(noUsersWithEmail);
            }

            if (!await _userIdentityDomainManager.CheckPasswordAsync(user, userDTO.Password))
            {
                const string passwordIsIncorrect = "Password is incorrect";
                _logger.LogWarn(passwordIsIncorrect);

                throw new InvalidOperationException(passwordIsIncorrect);
            }

            return CreateAuthResult(user.Id, user.GeoStatUser_Id, user.Email);
        }
    
        public async Task<AuthResult> Register(AuthorisationUserDTO userDTO)
        {
            var customUser = new User
            {
                UserName = userDTO.Email,
                Email = userDTO.Email
            };

            var userResult = await _userIdentityDomainManager.CreateAsync(customUser, userDTO.Password);

            if (userResult.Errors.Any())
            {
                var errors = string.Join(", ", userResult.Errors);
                _logger.LogWarn($"Errors while creating user: {errors}");

                throw new InvalidOperationException(errors);
            }

            var user = _userIdentityDomainManager.FindByName(userDTO.Email);

            var geostatUserDto = new GeoStatUserDto
            {
                Email = userDTO.Email,
                UserId = user.Id
            };

            var geostatUserResult = await _geostatUserDomainManager.InsertAsync(geostatUserDto);
            user.GeoStatUser_Id = geostatUserResult.Id;
            await _userIdentityDomainManager.UpdateAsync(user);

            return CreateAuthResult(user.Id, user.GeoStatUser_Id, user.Email);
        }

        private AuthResult CreateAuthResult(
            string userId,
            string userGeoStatId,
            string userEmail)
            => new AuthResult()
                {
                    UserEmail = userEmail,
                    UserId = userGeoStatId,
                    Token = _tokenManager.GenerateToken(
                        userEmail,
                        userId,
                        DateTime.UtcNow,
                        DateTime.UtcNow.AddDays(1))
                };
        
    }
}
