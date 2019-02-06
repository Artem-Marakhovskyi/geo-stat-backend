using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GeoStat.WebAPI.Models
{
    public class TokenChecker
    {
        public bool CheckToken(string accessToken, string refreshToken)
        {
            var tokenGenerator = new TokenGenerator();
            var handler = new JwtSecurityTokenHandler();
            var tokenSecure = handler.ReadToken(accessToken) as JwtSecurityToken;
            if (DateTime.Now > tokenSecure.ValidTo)
            {
                return false;
            }
            return true;
        }
    }
}