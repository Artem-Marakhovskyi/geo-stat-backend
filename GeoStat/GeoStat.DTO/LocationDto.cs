using System;
using Microsoft.Azure.Mobile.Server;
using System.ComponentModel.DataAnnotations;

namespace GeoStat.DTO
{
    public class LocationDto : EntityData
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public DateTimeOffset DateTime { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
