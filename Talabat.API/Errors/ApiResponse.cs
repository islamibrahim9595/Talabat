using System;

namespace Talabat.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }


        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultStatusCodeMessage(statusCode);
        }

        private string GetDefaultStatusCodeMessage(int statusCode)
            => statusCode switch
            {
                400 => "A Bad Request, You Are Not",
                401 => "Authorized, You Are Not",
                404 => "Resource was not found",
                500 => "Errors are the path to the dark side, Errors leads to anger, Anger leads to hate",
                _ => null
            };
        
    }
}
