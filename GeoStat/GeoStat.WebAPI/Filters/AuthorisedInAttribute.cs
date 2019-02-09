using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using GeoStat.BussinessLogic.Access;

namespace GeoStat.WebAPI.Filters
{
    public class AuthorisedInAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var hasHeaders = actionContext.Request.Headers.TryGetValues("GEOSTAT_AUTH", out var list);
            if (!hasHeaders || list.Count() != 1)
            {
                return false;
            }

            var tokenManager
                = actionContext
                    .ControllerContext
                    .Configuration
                    .DependencyResolver
                    .GetService(typeof(ITokenManager)) as ITokenManager;

            var token = list.First();

            return tokenManager.Validate(token);
        }
    }
}
