using System.Net;
using System.Text.Json;

namespace BridgeRTU
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public class ApiException
        {
            public ApiException(int statusCode, string message, string details)
            {
                StatusCode = statusCode;
                Message = message;
                Details = details;
            }

            public int StatusCode { get; set; }
            public string Message { get; set; } = null!;
            public string Details { get; set; } = null!;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
             {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = ex switch
                {
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    BadHttpRequestException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,// unhandled error
                };
                var response = new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()!);
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}