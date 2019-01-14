using Microsoft.Azure.Mobile.Server;

namespace GeoStat.Entities
{
    public class GroupUser : EntityData
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }
    }
}
