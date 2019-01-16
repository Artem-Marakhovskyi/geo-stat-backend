using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoStat.DTO
{
    public class GroupDto : EntityData
    {
        public string Label { get; set; }

        public string CreatorId { get; set; }
    }
}
