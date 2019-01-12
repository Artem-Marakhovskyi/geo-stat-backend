using System.Collections.Generic;
using Microsoft.Azure.Mobile.Server;

namespace GeoStat.Entities
{
    public class Group : EntityData
    {
        public string Label { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; }
    }
}
