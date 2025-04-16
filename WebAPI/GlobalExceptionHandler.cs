namespace WebAPI
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        //public async Task InvokeAsync(HttpContext httpContext)
        //{
        //    try
        //    {
        //        await _next(httpContext);  
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Unhandled Exception: {ex.Message}");
        //        await HandleExceptionAsync(httpContext, ex);
        //    }
        //}

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        //private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    var response = new
        //    {
        //        StatusCode = (int)HttpStatusCode.InternalServerError,
        //        Message = "An unexpected error occurred.",
        //        Detailed = exception.Message
        //    };

        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = response.StatusCode;

        //    var result = JsonSerializer.Serialize(response);
        //    return context.Response.WriteAsync(result);
        //}

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Log.Error(exception, "An unexpected error occurred.");

            var response = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "An unexpected error occurred.",
                Detailed = exception.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            var result = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(result);
        }

    }

}
