namespace Talabat.API.Errors
{
    public class ApiExceptionErrorResponse : ApiResponse
    {
        public string Details { get; set; }
        public ApiExceptionErrorResponse(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }


    }
}
