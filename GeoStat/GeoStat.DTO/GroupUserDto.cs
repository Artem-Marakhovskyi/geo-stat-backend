using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoStat.DTO
{
    public class GroupUserDto : EntityData
    {
        public string UserId { get; set; }

        public string GroupId { get; set; }
    }
}
