namespace E_Commerce_Website_Persentation.Errors
{
    public class ApiVaidationErrorResponse : APIResponse
    {
        public ApiVaidationErrorResponse() : base(400)
        {
            Errors = new List<string>();
        }
        public IEnumerable<string> Errors { get; set; }

    }
}
