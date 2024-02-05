using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PermissionRequestApp.Application.Common.Dtos;
using PermitRequest.Application.DTOs;
using PermitRequest.Application.Features.Commands;
using PermitRequest.Application.Features.EventHandlers;
using PermitRequest.Application.Features.Queries;
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
            services.AddScoped<IRepository<CumulativeLeaveRequest>, EfRepository<CumulativeLeaveRequest>>();
            services.AddScoped<IRepository<Notification>, EfRepository<Notification>>();


            services.AddTransient<IRequestHandler<CreateRequestRecordCommand, Result<bool>>, CreateRequestRecordCommandHandler>();

            services.AddTransient<IRequestHandler<GetListLeaveRequestQuery, Result<IEnumerable<LeaveRequestDto>>>, GetListLeaveRequestQueryHandler>();
            services.AddTransient<IRequestHandler<GetByIdLeaveRequestQuery, Result<IEnumerable<LeaveRequestDto>>>, GetByIdLeaveRequestQueryHandler>();
            services.AddTransient<IRequestHandler<GetListCumulativeLeaveRequestQuery, Result<IEnumerable<CumulativeLeaveRequestDto>>>, GetListCumulativeLeaveRequestQueryHandler>();
            services.AddTransient<IRequestHandler<GetListNotificationRequestQuery, Result<IEnumerable<NotificationDto>>>, GetListNotificationRequestQueryHandler>();


            services.AddTransient<INotificationHandler<CreateCumulativeEvent>, CreateCumulativeEventHandler>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }
    }
}
