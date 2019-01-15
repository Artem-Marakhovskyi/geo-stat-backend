using Microsoft.Azure.Mobile.Server;
using System.Collections.Generic;

namespace GeoStat.Entities
{
    public class GeoStatUser : EntityData
    {
        public string Email { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
