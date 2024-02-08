using PermitRequest.WebApi.Extensions;

namespace PermitRequest.Application.Extensions
{
    public static class ExceptionMidlewareExtension
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app) => app.UseMiddleware<ExceptionMidleware>();
    }
}
