using AutoMapper;
using PermissionRequestApp.Domain.Common.Dtos;
using PermitRequest.Application.Features.Commands;
using PermitRequest.Application.Features.Queries;
using PermitRequest.Domain.DTOs;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;

namespace PermitRequest.Application.Features.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RequestRecordDto, CreateRequestRecordCommand>()
                .ConstructUsing(src => new CreateRequestRecordCommand(src.UserId, DateTime.Parse(src.StartDate), DateTime.Parse(src.EndDate), (LeaveType)src.LeaveType, src.reason));

            CreateMap<GetListDto, GetListLeaveRequestQuery>()
                .ConstructUsing(src => new GetListLeaveRequestQuery(src.skip, src.take));

            CreateMap<GetListDto, GetListNotificationRequestQuery>()
               .ConstructUsing(src => new GetListNotificationRequestQuery(src.skip, src.take));

            CreateMap<GetListDto, GetListCumulativeLeaveRequestQuery>()
              .ConstructUsing(src => new GetListCumulativeLeaveRequestQuery(src.skip, src.take));

            CreateMap<GetByIdDto, GetByIdLeaveRequestQuery>()
                .ConstructUsing(src => new GetByIdLeaveRequestQuery(src.skip, src.take, src.userId));

            CreateMap<CumulativeLeaveRequest, CumulativeLeaveRequestDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveTypeId))
                .ForMember(dest => dest.TotalHour, opt => opt.MapFrom(src => src.TotalHours));

            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDateStr))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));

            CreateMap<LeaveRequest, LeaveRequestDto>()
              .ForMember(dest => dest.ReqFormNumber, opt => opt.MapFrom(src => src.RequestNumber))
              .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.CreatedBy.FullName))
              .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType))
              .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreatedAtStr))
              .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDateStr))
              .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDateStr))
              .ForMember(dest => dest.TotalHour, opt => opt.MapFrom(src => src.TotalWorkHours))
              .ForMember(dest => dest.Workflow, opt => opt.MapFrom(src => src.WorkflowStatus));
        }
    }
}
