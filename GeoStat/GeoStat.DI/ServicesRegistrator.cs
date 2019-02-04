using System.Configuration;
using System.Data.Entity;
using Autofac;
using AutoMapper;
using GeoStat.BussinessLogic;
using GeoStat.BussinessLogic.Interfaces;
using GeoStat.CrossCutting.Logger;
using GeoStat.DataAccess;
using GeoStat.DTO;
using GeoStat.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Azure.Mobile.Server.Tables;
using Microsoft.Extensions.Logging;

namespace GeoStat.IoC
{
    public class ServicesRegistrator
    {
        public void Register(ContainerBuilder builder)
        {
            RegisterLogger(builder);
            ConfigureMapper();
            RegisterContext(builder);
            RegisterDomainManagers(builder);
        }

        private void ConfigureMapper()
        {
            Mapper.Initialize(
                c => 
                {
                    c.CreateMap<LocationDto, Location>().ReverseMap();
                    c.CreateMap<GeoStatUserDto, GeoStatUser>().ReverseMap();
                    c.CreateMap<GroupDto, Group>().ReverseMap();
                    c.CreateMap<GroupUserDto, GroupUser>().ReverseMap();
                });
        }

        private void RegisterDomainManagers(ContainerBuilder builder)
        {
            builder.RegisterType<LocationDomainManager>().As<IDomainManager<LocationDto>>();
            builder.RegisterType<GeoStatUserDomainManager>().As<GeoStatUserDomainManager>();
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
            var logger = GeoStatLogger.Factory.CreateLogger("GeoStat");
            builder.RegisterInstance(logger).As<ILogger>();
            builder.RegisterType<GeoStatLogger>().As<IGeoStatLogger>().InstancePerRequest();
        }
    }
}
