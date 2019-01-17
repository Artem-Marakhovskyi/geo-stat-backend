using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations;

namespace GeoStat.DTO
{
    public class GeoStatUserDto : EntityData
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
