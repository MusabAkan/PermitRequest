using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PermissionRequestApp.Domain.Common.Dtos;
using PermitRequest.Application.Features.Commands;
using PermitRequest.Application.Features.EventHandlers;
using PermitRequest.Application.Features.Events;
using PermitRequest.Application.Features.Profiles;
using PermitRequest.Application.Features.Queries;
using PermitRequest.Domain.DTOs;
using PermitRequest.Infrastructure.EntityFramework.Repositories;
using PermitRequest.Infrastructure.EntityFramework.Services;


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

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IRequestHandler<CreateRequestRecordCommand, Result<Guid>>, CreateRequestRecordCommandHandler>();

            services.AddScoped<IRequestHandler<GetListLeaveRequestQuery, Result<IEnumerable<LeaveRequestDto>>>, GetListLeaveRequestQueryHandler>();
            services.AddScoped<IRequestHandler<GetByIdLeaveRequestQuery, Result<IEnumerable<LeaveRequestDto>>>, GetByIdLeaveRequestQueryHandler>();
            services.AddScoped<IRequestHandler<GetListCumulativeLeaveRequestQuery, Result<IEnumerable<CumulativeLeaveRequestDto>>>, GetListCumulativeLeaveRequestQueryHandler>();
            services.AddScoped<IRequestHandler<GetListNotificationRequestQuery, Result<IEnumerable<NotificationDto>>>, GetListNotificationRequestQueryHandler>();

            services.AddScoped<INotificationHandler<CreateCumulativeEvent>, CreateCumulativeEventHandler>();
            services.AddScoped<INotificationHandler<CreateNotificationEvent>, CreateNotificationEventHandler>();

            return services;
        }
    }
}
