namespace MyToDo.Shared
{
    public class ApiResponse
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public object Data { get; set; }

        public ApiResponse() : this(true) { }

        public ApiResponse(object data) : this(true, null, data) { }

        public ApiResponse(bool success) : this(success, null) { }

        public ApiResponse(bool success, string message) : this(success, message, null) { }

        public ApiResponse(bool success, string message, object data)
        {
            Success= success;
            Message= message;
            Data= data;
        }
    }

    public class ApiResponse<T> where T : class
    {
        public T Data { get; set; }

        public bool Success { get; set; }
        
        public string Message { get; set; }

    }
}
