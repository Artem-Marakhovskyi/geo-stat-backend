using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoStat.WebAPI.Models
{
    public class JsonGenerator
    {
        public JObject GenerateJson (string accessToken, string refreshToken)
        {
            JObject json = new JObject(
                    new JProperty("accessToken", accessToken),
                    new JProperty("refreshToken", refreshToken));
            return json;
        }
    }
}