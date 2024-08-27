using HouseUnitAPI.Helpers.ExceptionHandling;
using System.Text.Json;

namespace HouseUnitAPI.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(context, new ErrorResponse("An unexpected error occurred.", 500));
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, ErrorResponse errorResponse)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorResponse.StatusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
