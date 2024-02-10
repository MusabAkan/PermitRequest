using Ardalis.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using PermitRequest.Application.Profiles;
using PermitRequest.Domain.Services;
using PermitRequest.Infrastructure.EntityFramework.Repositories;
using PermitRequest.Infrastructure.EntityFramework.Services;
using PermitRequest.Infrastructure.Repositories;
namespace PermitRequest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddScoped<IAdUserRepository, EfAdUserRepository>();
            services.AddScoped<ICumulativeLeaveRequestRepository, EfCumulativeLeaveRequestRepository>();
            services.AddScoped<ILeaveRequestRepository, EfLeaveRequestRepository>();
            services.AddScoped<INotificationRepository, EfNotificationRepository>();  
            services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>(); 

            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }
    }
}
