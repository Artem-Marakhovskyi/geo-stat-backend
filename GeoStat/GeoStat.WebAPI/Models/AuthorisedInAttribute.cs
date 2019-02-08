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
                    var tokenGenerator = new TokenGenerator();
                    var handler = new JwtSecurityTokenHandler();
                    var tokenSecure = handler.ReadToken(token) as JwtSecurityToken;
                    if (DateTime.Now > tokenSecure.ValidTo)
                    {
                        throw new Exception("TOKEN EXPIRED");
                    }
                    else
                    {
                        var tokenValidator = new TokenValidator();
                        var result = tokenValidator.ValidateToken(token);
                        if(!result)
                        {
                            throw new Exception("TOKEN INVALID");
                        }
                    }
                }
            }
        }
    }
}