using System.Configuration;
using Autofac;
using GeoStat.BussinessLogic;
using GeoStat.CrossCutting.Logger;
using GeoStat.DataAccess;
using GeoStat.DTO;
using Microsoft.Azure.Mobile.Server.Tables;
using Microsoft.Extensions.Logging;

namespace GeoStat.IoC
{
    public class ServicesRegistrator
    {
        public void Register(ContainerBuilder builder)
        {
            RegisterLogger(builder);
            RegisterContext(builder);
            RegisterDomainManagers(builder);
        }

        private void RegisterDomainManagers(ContainerBuilder builder)
        {
            builder.RegisterType<LocationDomainManager>().As<IDomainManager<LocationDto>>();
        }

        private void RegisterContext(ContainerBuilder builder)
        {
            var connectionString 
                = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            builder
                .RegisterType<GeoStatContext>()
                .AsSelf()
                .WithParameter("connectionString", connectionString)
                .InstancePerRequest();
        }

        private void RegisterLogger(ContainerBuilder builder)
        {
            var logger = GeoStatLogger.Factory.CreateLogger("GeoStat");
            builder.RegisterInstance(logger).As<ILogger>();
            builder.RegisterType<GeoStatLogger>().As<IGeoStatLogger>().InstancePerRequest();
        }
    }
}
