using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations;

namespace GeoStat.DTO
{
    public class GroupUserDto : EntityData
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string GroupId { get; set; }
    }
}
