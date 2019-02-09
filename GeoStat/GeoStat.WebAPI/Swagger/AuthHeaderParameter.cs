using GeoStat.WebAPI.Filters;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace GeoStat.WebAPI.Swagger
{
    public class AuthHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            var requiresAuth = apiDescription.GetControllerAndActionAttributes<AuthorisedInAttribute>().Any();

            if (requiresAuth)
            {
                operation.parameters.Add(new Parameter
                {
                    name = "GEOSTAT_AUTH",
                    @in = "header",
                    type = "string",
                    required = true
                });
            }
        }
    }
}