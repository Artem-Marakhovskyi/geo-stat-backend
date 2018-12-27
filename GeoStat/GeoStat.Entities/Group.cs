using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeoStat.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Label { get; set; }
        public User Creator { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
    }
}
