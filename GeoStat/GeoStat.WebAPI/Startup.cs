using GeoStat.DI.AppStart;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Tables.Config;
using Microsoft.Owin;
using Owin;
using System.Reflection;
using System.Web.Http;
using GeoStat.WebAPI;
using GeoStat.DataAccess;
using GeoStat.WebAPI.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(GeoStat.WebAPI.Startup))]

namespace GeoStat.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

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