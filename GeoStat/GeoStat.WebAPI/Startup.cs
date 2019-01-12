using GeoStat.IoC.AppStart;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Tables.Config;
using Microsoft.Owin;
using Owin;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(GeoStat.WebAPI.Startup))]

namespace GeoStat.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            SwaggerConfig.Register(config);
            InversionOfControlConfig.Initialize(appBuilder, config, Assembly.GetExecutingAssembly());
            ConfigureMobileApp(appBuilder, config);
        }

        public virtual void ConfigureMobileApp(IAppBuilder appBuilder, HttpConfiguration config)
        {
            new MobileAppConfiguration()
                .MapApiControllers()
                .AddTables(
                    new MobileAppTableConfiguration()
                        .MapTableControllers()
                        .AddEntityFramework()
                )
                .ApplyTo(config);

            appBuilder.UseWebApi(config);
            config.MapHttpAttributeRoutes();
        }
    }
}