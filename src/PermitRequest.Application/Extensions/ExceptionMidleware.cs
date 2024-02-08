using System.Net;
using Ardalis.Result;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace PermitRequest.Application.Extensions
{
    public class ExceptionMidleware
    {
        readonly RequestDelegate _next;

        public ExceptionMidleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }
        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = JsonSerializer.Serialize(Result.Error(e.Message));
            return httpContext.Response.WriteAsync(result);
        }
    }
}
