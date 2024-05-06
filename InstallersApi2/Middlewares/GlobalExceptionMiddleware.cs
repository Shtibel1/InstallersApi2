using BLL.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace InstallersApi.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch (EntityNotFoundException ex)
            {
                await HandleGlobalExceptionAsync(httpContext, StatusCodes.Status404NotFound, ex);
            }

            catch (Exception ex)
            {
                _logger.LogError("################ EXCEPTION ##################" + ex);
                await HandleGlobalExceptionAsync(httpContext, StatusCodes.Status500InternalServerError, ex);
            }
        }

        private Task HandleGlobalExceptionAsync(HttpContext context, int statusCode, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.ToString(),
            }.ToString());
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public static class StatusCodeMessageResolver
    {
        private static readonly Dictionary<int, string> Messages = new Dictionary<int, string>
    {
        { 404, "Not Found" },
        { 500, "Internal Server Error" },
        { 400, "Bad Request" },
        { 401, "Unauthorized" },
        { 403, "Forbidden" },
        // Add other status codes and messages as needed
    };

        public static string GetMessage(int statusCode)
        {
            if (Messages.TryGetValue(statusCode, out string message))
            {
                return message;
            }

            // Return a default message or throw an exception if the status code is not found
            return "Unknown status code";
        }
    }


}
