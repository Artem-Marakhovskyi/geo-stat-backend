using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace GeoStat.WebAPI.Swagger
{
    public class AddODataParameters : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                return;

            var supportsOData = false;
            var returnType = apiDescription.ActionDescriptor.ReturnType;

            if (returnType == null)
            {
                return;
            }

            if (returnType.IsGenericType)
            {
                supportsOData = returnType.GetGenericTypeDefinition() == typeof(System.Linq.IQueryable<>) ||
                                returnType.GetGenericTypeDefinition() == typeof(IEnumerable<>);
            }

            if (supportsOData)
            {
                AddParameter(operation, "$expand", "query", "Expands related entities inline.", "string", false);
                AddParameter(operation, "$filter", "query", "Filters the results, based on a Boolean condition.",
                    "string", false);
                AddParameter(operation, "$select", "query", "Selects which properties to include in the response.",
                    "string", false);
                AddParameter(operation, "$orderby", "query", "Sorts the results.", "string", false);
                AddParameter(operation, "$top", "query", "Returns only the first n results.", "integer", false,
                    "int32");
                AddParameter(operation, "$skip", "query", "Skips the first n results.", "integer", false, "int32");
                AddParameter(operation, "$count", "query", "Includes a count of the matching results in the response.",
                    "boolean", false);
            }
        }

        private static void AddParameter(Operation operation, string name, string kind, string description, string type,
            bool required, string format = null)
        {
            operation.parameters.Add(new Parameter
            {
                name = name,
                @in = kind,
                description = description,
                type = type,
                format = format,
                required = required
            });
        }

    }
}