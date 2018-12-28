using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeoStat.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public User User { get; set; }
    }
}
