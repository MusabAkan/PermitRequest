using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PermitRequest.Application.Commons;
using PermitRequest.Application.EventHandlers;
using PermitRequest.Application.Profiles;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Events;
using PermitRequest.Infrastructure.EntityFramework.Repositories;

namespace PermitRequest.Infrastructure.EntityFramework
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddScoped<IRepository<AdUser>, EfRepository<AdUser>>();
            services.AddScoped<IRepository<LeaveRequest>, EfRepository<LeaveRequest>>();
            
            services.AddTransient<IRequestHandler<CreateRequestRecordCommand, Result<bool>>, CreateRequestRecordCommandHandler>();

            services.AddTransient<INotificationHandler<CreateLeaveRequestEvent>, CreateLeaveRequestEventHandlers>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }
    }
}
