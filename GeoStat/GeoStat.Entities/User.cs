using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeoStat.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } 
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Location> Locations { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<Group> CreatedGroups { get; set; }
    }
}
