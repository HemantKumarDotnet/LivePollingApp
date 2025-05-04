namespace LivePollingApp.Models
{
    public class CommonResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }

        public CommonResponse() { }

        public CommonResponse(T data, string message = "Success", int statusCode = 200, bool success = true)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
            Success = success;
        }

        public CommonResponse(string message, int statusCode = 500, bool success = false)
        {
            Message = message;
            StatusCode = statusCode;
            Success = success;
        }
    }

}
