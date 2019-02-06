using GeoStat.BussinessLogic;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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

        public string GenerateAccessToken(string userName, string userId)
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

        public string GenerateRefreshToken(string accessToken)
        {
            var signingKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(communicationKey));
            var signingCredentials = new System.IdentityModel.Tokens.SigningCredentials(signingKey,
               System.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature, System.IdentityModel.Tokens.SecurityAlgorithms.Sha256Digest);

            var claims = new List<Claim>()
            {
                new Claim("accessToken" , accessToken)
            };

            var jwt = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMonths(1),
                    signingCredentials: signingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            var signedAndEncodedToken = tokenHandler.WriteToken(jwt);

            return signedAndEncodedToken;
        }

        public TokenGenerationResponses RefreshAccessToken(string expiredAccessToken, string refreshToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var accessTokenSecure = handler.ReadToken(expiredAccessToken) as JwtSecurityToken;
            var refreshTokenSecure = handler.ReadToken(refreshToken) as JwtSecurityToken;
            if(expiredAccessToken == refreshTokenSecure.Claims.First().Value)
            {
                var newAccessToken = GenerateAccessToken(accessTokenSecure.Claims.First().Value, accessTokenSecure.Claims.Skip(1).First().Value);
                return new TokenGenerationResponses(TokenGenerationResponses.Responses.Success, newAccessToken);
            }
            return new TokenGenerationResponses(TokenGenerationResponses.Responses.AccessDenied, "not allowed");
        }
    }
}