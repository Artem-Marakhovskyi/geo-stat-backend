using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoStat.WebAPI.Models
{
    public class TokenGenerationResponses
    {
        public Responses CustomResponse { get; set; }

        public string ResponseString { get; set; }

        public enum Responses
        {
            AccessDenied,
            Success
        }

        public TokenGenerationResponses(Responses responses, string responseString)
        {
            CustomResponse = responses;
            ResponseString = responseString;
        }
    }
}