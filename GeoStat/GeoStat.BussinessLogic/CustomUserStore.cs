using GeoStat.DataAccess;
using GeoStat.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoStat.BussinessLogic
{
    public class CustomUserStore : UserStore<User>
    {
        public CustomUserStore(GeoStatContext geostatContext) : base (geostatContext)
        {
       
        }
    }
}
