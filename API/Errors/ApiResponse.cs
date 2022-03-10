using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public ApiResponse()
        {
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request you have made",
                401 => "Authrized You are not",
                404 => "Resorse Not Found",
                500 => "Errors",
                _ => null,
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

       
    }
}