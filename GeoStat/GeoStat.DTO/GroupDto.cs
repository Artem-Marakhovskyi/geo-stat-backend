using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations;

namespace GeoStat.DTO
{
    public class GroupDto : EntityData
    {
        [Required]
        public string Label { get; set; }

        [Required]
        public string CreatorId { get; set; }
    }
}
