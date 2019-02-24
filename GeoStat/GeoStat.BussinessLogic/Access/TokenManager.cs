using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using SecurityAlgorithms = System.IdentityModel.Tokens.SecurityAlgorithms;

namespace GeoStat.BussinessLogic.Access
{
    public class TokenManager : ITokenManager
    {
        private const string communicationKey = "GQDstc21ewfffffffffffFiwDffVvVBrk";

        private readonly SigningCredentials _signingCredentials
            = new SigningCredentials(
                new InMemorySymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(communicationKey)),
                    SecurityAlgorithms.HmacSha256Signature,
                    SecurityAlgorithms.Sha256Digest);

        private readonly JwtSecurityTokenHandler _tokenHandler
            = new JwtSecurityTokenHandler();

        public string GenerateToken(
            string userName,
            string userId,
            DateTime? notBefore = null,
            DateTime? expires = null)
        {
            var jwt = new JwtSecurityToken(
                    claims: GetClaims(userName, userId),
                    notBefore: notBefore ?? DateTime.UtcNow,
                    expires: expires ?? DateTime.UtcNow.AddDays(10),
                    signingCredentials: _signingCredentials
                );

            return _tokenHandler.WriteToken(jwt);
        }

        public bool Validate(string token)
        {
            try
            {
                var tokenSecure = _tokenHandler.ReadToken(token) as JwtSecurityToken;

                var userName = tokenSecure.Claims.First().Value;
                var userId = tokenSecure.Claims.Skip(1).First().Value;

                return token == GenerateToken(
                    userName,
                    userId,
                    tokenSecure.ValidFrom,
                    tokenSecure.ValidTo);
            }
            catch (Exception)
            {
                return false;
            }

        }

        private IEnumerable<Claim> GetClaims(
            string userName,
            string userId)
        {
            return new List<Claim>()
            {
                new Claim("userName", userName),
                new Claim("userId", userId)
            };
        }
    }
}