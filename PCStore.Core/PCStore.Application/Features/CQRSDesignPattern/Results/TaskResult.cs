namespace PCStore.Application.Features.CQRSDesignPattern.Results
{
    public class TaskResult<T>
    {
        public bool IsSucceeded { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static TaskResult<T> Success(string message = "", T? data = default)
        {
            return new TaskResult<T>
            {
                IsSucceeded = true,
                StatusCode = 200,
                Message = message,
                Data = data
            };
        }

        public static TaskResult<T> NotFound(string message = "", T? data = default)
        {
            return new TaskResult<T>
            {
                IsSucceeded = false,
                StatusCode = 404,
                Message = message,
                Data = data
            };
        }

        public static TaskResult<T> Fail(string message = "", int statusCode = 400, List<string>? errors = null)
        {
            return new TaskResult<T>
            {
                IsSucceeded = false,
                StatusCode = statusCode,
                Message = message,
                Errors = errors ?? [message]
            };
        }
    }

}
