﻿using Autofac;
using GeoStat.BussinessLogic;
using GeoStat.BussinessLogic.Access;
using GeoStat.BussinessLogic.Interfaces;
using GeoStat.DTO;
using GeoStat.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.Mobile.Server.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoStat.DI
{
    static class DomainManagersRegistrator
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<LocationDomainManager>().As<IDomainManager<LocationDto>>();
            builder.RegisterType<GeoStatUserDomainManager>().As<IGeoStatUserDomainManager>();
            builder.RegisterType<GroupUserDomainManager>().As<IDomainManager<GroupUserDto>>();
            builder.RegisterType<GroupDomainManager>().As<IDomainManager<GroupDto>>();
            builder.RegisterType<AccountDomainManager>().As<IAccountDomainManager>();
            builder.RegisterType<CustomUserStore>().As<IUserStore<User>>();
            builder.RegisterType<UserIdentityDomainManager>().As<UserIdentityDomainManager>();
            builder.RegisterType<TokenManager>().As<ITokenManager>();
        }
    }
}
