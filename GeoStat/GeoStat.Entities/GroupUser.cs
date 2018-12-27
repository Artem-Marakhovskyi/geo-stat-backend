using System;
using System.Collections.Generic;
using System.Text;

namespace GeoStat.Entities
{
    public class GroupUser
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
