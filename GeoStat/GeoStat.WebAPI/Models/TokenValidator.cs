using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;

namespace GeoStat.WebAPI.Models
{
    public class TokenValidator
    {
        public bool ValidateToken(string token)
        {
            var tokenGenerator = new TokenGenerator();
            var handler = new JwtSecurityTokenHandler();
            var tokenSecure = handler.ReadToken(token) as JwtSecurityToken;

            var userName = tokenSecure.Claims.First().Value;
            var userId = tokenSecure.Claims.Skip(1).First().Value;

            return token != tokenGenerator.GenerateToken(userName, userId, tokenSecure.ValidFrom, tokenSecure.ValidTo);
        }
    }
}