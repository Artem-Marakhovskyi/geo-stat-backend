using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Mobile.Server;

namespace GeoStat.DTO
{
    public class UserDTO : AuthorisationUserDTO
    {
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
    }
}
