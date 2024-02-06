using Microsoft.AspNetCore.Builder;

namespace PermitRequest.Domain.Extensions
{
    public static class ExceptionMidlewareExtension
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMidleware>();
        }
    }
}
