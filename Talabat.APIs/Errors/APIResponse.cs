namespace Talabat.APIs.Errors
{
    public class APIResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public APIResponse(int statusCode, string? message = null)
        {
            
            StatusCode = statusCode;
            ErrorMessage = message ?? GetDefaultMessageforStatusCode(statusCode);
        }

        private string GetDefaultMessageforStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "UnAuthorized",
                404 => "Not Found",
                500 => "Error path to the dark side , Errors Leads to anger . Anger Leads to hate . Hate Leads to career Change"

            };
        }
    }
}
