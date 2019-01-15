using Microsoft.Azure.Mobile.Server;

namespace GeoStat.Entities
{
    public class GroupUser : EntityData
    {
        public string UserId { get; set; }

        public GeoStatUser User { get; set; }

        public string GroupId { get; set; }

        public Group Group { get; set; }
    }
}
