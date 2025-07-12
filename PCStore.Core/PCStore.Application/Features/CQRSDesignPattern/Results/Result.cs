namespace PCStore.Application.Features.CQRSDesignPattern.Results
{
    public class Result
    {
        public bool IsSucceeded { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }

        public static Result Success(string message = "Removed successfully!")
        {
            return new Result
            {
                IsSucceeded = true,
                StatusCode = 200,
                Message = message,
            };
        }

        public static Result Fail(string message = "", int statusCode = 400)
        {
            return new Result
            {
                IsSucceeded = false,
                StatusCode = statusCode,
                Message = message
            };
        }
        public static Result NotFound(string message = "Not found!", int statusCode = 404)
        {
            return new Result
            {
                IsSucceeded = false,
                StatusCode = statusCode,
                Message = message
            };
        }
    }
}
