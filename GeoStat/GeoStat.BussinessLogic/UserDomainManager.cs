using GeoStat.DataAccess;
using GeoStat.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoStat.BussinessLogic
{
    public class UserDomainManager : UserManager<User>
    {
        public UserDomainManager(IUserStore<User> store)
                : base(store)
        {
        }
    }
}