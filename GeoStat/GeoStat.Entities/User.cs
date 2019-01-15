using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GeoStat.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Location> Locations { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<Group> CreatedGroups { get; set; }
    }
}
