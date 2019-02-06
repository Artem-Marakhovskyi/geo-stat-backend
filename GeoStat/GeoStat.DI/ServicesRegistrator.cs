using System.Configuration;
using System.Data.Entity;
using Autofac;
using AutoMapper;
using GeoStat.BussinessLogic;
using GeoStat.BussinessLogic.Interfaces;
using GeoStat.CrossCutting.Logger;
using GeoStat.DataAccess;
using GeoStat.DI;
using GeoStat.DTO;
using GeoStat.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Azure.Mobile.Server.Tables;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace GeoStat.IoC
{
    public class ServicesRegistrator
    {
        public void Register(ContainerBuilder builder)
        {
            RegisterLogger(builder);
            MapperConfig.InitializeMapper();
            RegisterContext(builder);
            RegisterDomainManagers(builder);
        }
        private void RegisterDomainManagers(ContainerBuilder builder)
        {
            builder.RegisterType<LocationDomainManager>().As<IDomainManager<LocationDto>>();
            builder.RegisterType<GeoStatUserDomainManager>().As<IGeoStatUserDomainManager>();
            builder.RegisterType<GroupUserDomainManager>().As<IDomainManager<GroupUserDto>>();
            builder.RegisterType<GroupDomainManager>().As<IDomainManager<GroupDto>>();
            builder.RegisterType<AccountDomainManager>().As<IAccountDomainManager>();
            builder.RegisterType<UserDomainManager>().As<UserDomainManager>();
            builder.RegisterType<CustomUserStore>().As<IUserStore<User>>();
        }

        private void RegisterContext(ContainerBuilder builder)
        {
            var connectionString 
                = ConfigurationManager.ConnectionStrings["MS_TableConnectionString"].ConnectionString;

            builder
                .RegisterType<GeoStatContext>()
                .AsSelf()
                .WithParameter("connectionString", connectionString)
                .InstancePerRequest();
        }

        private void RegisterLogger(ContainerBuilder builder)
        {
            NLog.LogManager.LoadConfiguration("nlog.config");
            var logger = new NLogLoggerFactory().CreateLogger("GeoStat");
            builder.RegisterInstance(logger).As<ILogger>();
            builder.RegisterType<GeoStatLogger>().As<IGeoStatLogger>().InstancePerRequest();
        }
    }
}
