namespace E_Commerce_Website_Persentation.Errors
{
    public class ApiExceptionResponse : APIResponse
    {
        public string ? Details {  get; set; }
        public ApiExceptionResponse(int statusCode, string? errorMessage = null, string? details = null) : base(statusCode, errorMessage)
        {
            Details = details;
        }

    }
}
