using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Azure.Mobile.Server.Swagger;
using Swashbuckle.Application;

namespace GeoStat.WebAPI
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Services.Replace(
                typeof(IApiExplorer),
                new MobileAppApiExplorer(configuration));

            configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "GeoStat.WebAPI");
                    })
                .EnableSwaggerUi(c => { });
        }
    }
}
