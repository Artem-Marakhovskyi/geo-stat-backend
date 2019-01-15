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

namespace GeoStat.WebAPI.Models
{
    public class GeoStatUserManager : UserManager<User>
    {
        public GeoStatUserManager(IUserStore<User> store)
                : base(store)
        {
        }
        public static GeoStatUserManager Create(IdentityFactoryOptions<GeoStat.WebAPI.Models.GeoStatUserManager> options,
                                                IOwinContext context)
        {
            GeoStatContext db = context.Get<GeoStatContext>();
            GeoStatUserManager manager = new GeoStatUserManager(new UserStore<User>(db));
            return manager;
        }
    }
}