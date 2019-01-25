using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Mobile.Server;

namespace GeoStat.DTO
{
    public class UserDTO : EntityData
    {
        //[Required]
        //[Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }
    }
}
