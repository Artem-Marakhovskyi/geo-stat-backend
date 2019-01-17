using System;
using Microsoft.Azure.Mobile.Server;

namespace GeoStat.Entities
{
    public class Location : EntityData
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public string UserId { get; set; }

        public GeoStatUser User { get; set; }
    }
}
