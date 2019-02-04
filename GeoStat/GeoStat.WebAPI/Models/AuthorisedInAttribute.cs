using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Web;

namespace GeoStat.WebAPI.Models
{
    public class AuthorisedInAttribute : ValidationAttribute 
    {
        public AuthorisedInAttribute()
        {

        }
        public override bool IsValid(object value)
        {
            if(value != null)
            {
                var token = value as string;
                string secret = "GQDstc21ewfffffffffffFiwDffVvVBrk";
                var key = Encoding.ASCII.GetBytes(secret);
                var handler = new JwtSecurityTokenHandler();
                var tokenSecure = handler.ReadToken(token) as SecurityToken;
                var validations = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new InMemorySymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var claims = handler.ValidateToken(token, validations, out tokenSecure);
                var a = claims.Identities;
                return true;
            }
            return false;
        }
    }
}