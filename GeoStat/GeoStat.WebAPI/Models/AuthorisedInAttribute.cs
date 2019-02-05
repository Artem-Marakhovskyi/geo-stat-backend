using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GeoStat.WebAPI.Models
{
    public class AuthorisedInAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.Count != 0)
            {
                var token = actionContext.ActionArguments.First().Value.ToString();
                if (token != null)
                {
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
                    if (DateTime.Now < tokenSecure.ValidTo)
                    {
                        ///redirecttoaction 
                    }
                }
                //return 
            }
            OnActionExecuting(actionContext);
        }
    }
}