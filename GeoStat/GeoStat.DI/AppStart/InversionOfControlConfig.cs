using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using GeoStat.DI.AppStart;
using Owin;

namespace GeoStat.IoC.AppStart
{
    public class InversionOfControlConfig
    {
        public static void Initialize(
            IAppBuilder app,
            HttpConfiguration config,
            Assembly assemblyWithControllers)
        {
            var container = GetContainer(assemblyWithControllers);

            config.DependencyResolver = new DependencyResolver(new AutofacWebApiDependencyResolver(container));

            app.UseAutofacMiddleware(container);

            // Make sure the Autofac lifetime scope is passed to Web API.
            app.UseAutofacWebApi(config);
        }

        private static IContainer GetContainer(
            Assembly assemblyWithControllers)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(assemblyWithControllers);

            new ServicesRegistrator().Register(builder);

            return builder.Build();
        }
    }
}
