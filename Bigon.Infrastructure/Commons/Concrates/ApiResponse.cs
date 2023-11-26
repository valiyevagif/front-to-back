using System.Net;

namespace Bigon.Infrastructure.Commons.Concrates
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }


        public static ApiResponse Success(string message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponse
            {
                StatusCode = statusCode,
                Message = message,
                Error = false
            };
        }

        public static ApiResponse<T> Success<T>(T data, string message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
            where T : class
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Error = false,
                Data = data
            };
        }

        public static ApiResponse Fail(string message = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new ApiResponse
            {
                StatusCode = statusCode,
                Message = message,
                Error = true
            };
        }

        public static ApiResponse Fail(IDictionary<string, IEnumerable<string>> errors, string message = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponse
            {
                StatusCode = statusCode,
                Message = message,
                Error = true,
                Errors = errors
            };
        }
    }

    public class ApiResponse<T> : ApiResponse
        where T : class
    {
        public T Data { get; set; }
    }
}
