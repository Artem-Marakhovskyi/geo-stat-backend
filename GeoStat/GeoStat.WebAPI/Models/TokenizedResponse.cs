using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoStat.WebAPI.Models
{
    public class TokenizedResponse
    {
        public JObject CreateTokenizedResponse(string email, string id)
        {
            var tokenGenerator = new TokenGenerator();
            var token = tokenGenerator.GenerateToken(email, id);

            var jsonGenerator = new JsonGenerator();
            var json = jsonGenerator.GenerateJson(token);

            return json;
        }
    }
}