using System;
using Microsoft.Azure.Mobile.Server;

namespace GeoStat.Entities
{
    public class Location : EntityData
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public string UserId { get; set; }

        public GeoStatUser User { get; set; }
    }
}
