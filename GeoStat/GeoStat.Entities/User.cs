using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoStat.Entities
{
    public class User : IdentityUser
    {
        public string GeoStatUser_Id { get; set; }
    }
}
