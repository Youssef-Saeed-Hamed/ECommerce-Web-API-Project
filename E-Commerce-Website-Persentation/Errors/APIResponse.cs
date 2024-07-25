
namespace E_Commerce_Website_Persentation.Errors
{
    public class APIResponse
    {
        public APIResponse(int statusCode, string? errorMessage = null)
        {
            _statusCode = statusCode;
            _errorMessage = errorMessage ?? GetErrorMessageByStatusCode(_statusCode);
        }

        private string? GetErrorMessageByStatusCode(int StatusCode)
         => StatusCode switch
         {
             500 => "Internet Server Error",
             404 => "Not Found",
             401 => "An Autorized",
             400 => "Bad Request",
             _ => ""
         };
            
        

        public int _statusCode { get; set; }
        public string? _errorMessage { get; set; }

    }
}
