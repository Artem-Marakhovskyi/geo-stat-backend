using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations;

namespace GeoStat.DTO
{
    public class GeoStatUserDto : EntityData
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
