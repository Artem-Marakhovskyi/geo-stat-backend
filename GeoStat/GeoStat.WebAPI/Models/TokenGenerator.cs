using GeoStat.BussinessLogic;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GeoStat.WebAPI.Models
{
    public class TokenGenerator
    { 
        private const string communicationKey = "GQDstc21ewfffffffffffFiwDffVvVBrk";

        readonly System.IdentityModel.Tokens.SecurityKey signingKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(communicationKey));

        public string GenerateToken(string userName, string userId)
        {
            var signingKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(communicationKey));
            var signingCredentials = new System.IdentityModel.Tokens.SigningCredentials(signingKey,
               System.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature, System.IdentityModel.Tokens.SecurityAlgorithms.Sha256Digest);

            var claims = new List<Claim>()
            {
                new Claim("userName", userName),
                new Claim("userId", userId)
            };

            var claimsIdentity = new ClaimsIdentity(claims);

            var jwt = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: signingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            var signedAndEncodedToken = tokenHandler.WriteToken(jwt);

            return signedAndEncodedToken;
        }

        public string GenerateToken(string userName, string userId, DateTime notBefore, DateTime expires)
        {
            var signingKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(communicationKey));
            var signingCredentials = new System.IdentityModel.Tokens.SigningCredentials(signingKey,
               System.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature, System.IdentityModel.Tokens.SecurityAlgorithms.Sha256Digest);

            var claims = new List<Claim>()
            {
                new Claim("userName", userName),
                new Claim("userId", userId)
            };

            var claimsIdentity = new ClaimsIdentity(claims);

            var jwt = new JwtSecurityToken(
                    claims: claims,
                    notBefore: notBefore,
                    expires: expires,
                    signingCredentials: signingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            var signedAndEncodedToken = tokenHandler.WriteToken(jwt);

            return signedAndEncodedToken;
        }
    }
}