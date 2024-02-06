using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PermissionRequestApp.Domain.Common.Dtos;
using PermitRequest.Domain.DTOs;
using PermitRequest.Infrastructure.EntityFramework.Repositories;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));          

            services.AddScoped<IAdUserRepository, EfAdUserRepository>();
            services.AddScoped<ICumulativeLeaveRequestRepository, EfCumulativeLeaveRequestRepository>();
            services.AddScoped<ILeaveRequestRepository, EfLeaveRequestRepository>();
            services.AddScoped<INotificationRepository, EfNotificationRepository>();     

            return services;
        }
    }
}
