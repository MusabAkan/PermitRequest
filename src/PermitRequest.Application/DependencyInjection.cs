using Ardalis.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using PermitRequest.Application.Profiles;
using PermitRequest.Infrastructure.Repositories;

namespace PermitRequest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

            services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }
    }
}
