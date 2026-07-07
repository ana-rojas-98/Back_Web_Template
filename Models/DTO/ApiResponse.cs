namespace StrategicviewBack.Models.DTO
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public bool Warning { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(bool success, bool warning, string message, T data)
        {
            Success = success;
            Warning = warning;
            Message = message;
            Data = data;
        }

        public ApiResponse(bool success, bool warning,string message)
        {
            Success = success;
            Warning = warning;
            Message = message;
        }
    }
}

    
