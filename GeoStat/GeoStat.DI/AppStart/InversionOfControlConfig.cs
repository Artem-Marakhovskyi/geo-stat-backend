using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using GeoStat.IoC;
using Owin;

namespace GeoStat.DI.AppStart
{
    public class InversionOfControlConfig
    {
        public static void Initialize(
            IAppBuilder app,
            HttpConfiguration config,
            Assembly assemblyWithControllers)
        {
            var container = GetContainer(assemblyWithControllers, config);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);

            // Make sure the Autofac lifetime scope is passed to Web API.
            app.UseAutofacWebApi(config);
        }

        private static IContainer GetContainer(
            Assembly assemblyWithControllers,
            HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(assemblyWithControllers);
            builder.RegisterHttpRequestMessage(config);

            new ServicesRegistrator().Register(builder);

            return builder.Build();
        }
    }
}
