namespace PCStore.Application.Features.CQRSDesignPattern.Results
{
    public class TaskListResult<T>
    {
        public bool IsSucceeded { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public List<T>? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static TaskListResult<T> Success(string message = "", List<T>? data = null)
        {
            return new TaskListResult<T>
            {
                IsSucceeded = true,
                StatusCode = 200,
                Message = message,
                Data = data
            };
        }

        public static TaskListResult<T> NotFound(string message = "", List<T>? data = null)
        {
            return new TaskListResult<T>
            {
                IsSucceeded = false,
                StatusCode = 404,
                Message = message,
                Data = data
            };
        }

        public static TaskListResult<T> Fail(string message = "", int statusCode = 400, List<string>? errors = null)
        {
            return new TaskListResult<T>
            {
                IsSucceeded = false,
                StatusCode = statusCode,
                Message = message,
                Errors = errors ?? [message]
            };
        }
    }

}
