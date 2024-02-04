using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PermitRequest.Infrastructure.EntityFramework.Contexts;
using System.Reflection;

namespace PermitRequest.Infrastructure.EntityFramework
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, Type type)
        {
            var assemblies = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assemblies);
            services.AddMediatR(type.);

            services.AddDbContext<PermitRequestContext>(options =>
                 options.UseSqlServer(connectionString));

            return services;
        }
    }
}
