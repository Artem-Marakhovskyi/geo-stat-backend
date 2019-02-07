using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoStat.WebAPI.Models
{
    public class JsonGenerator
    {
        public JObject GenerateJson (string token, string userId)
        {
            JObject json = new JObject(
                    new JProperty("token", token),
                    new JProperty("userId", userId));
            return json;
        }
    }
}