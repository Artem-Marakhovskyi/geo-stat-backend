using System;
using System.Collections.Generic;
using System.Text;

namespace GeoStat.BussinessLogic.Helpers
{
    public class Response
    {
        public Responses CustomResponse { get; set; }

        public string ResponseString { get; set; }

        public enum Responses
        {
            InvalidPassword,
            UserNotFound,
            ModelErrors,
            Success
        }

        public Response(Responses responses, string responseString)
        {
            CustomResponse = responses;
            ResponseString = responseString;
        }
    }
}
