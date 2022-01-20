using Cleverbit.CodingTask.Data.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Host.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> _logger)
        {
            logger = _logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var now = DateTime.UtcNow;
            logger.LogError($"{now.ToString("HH:mm:ss")} : {ex}");
            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResult()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            }));
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}