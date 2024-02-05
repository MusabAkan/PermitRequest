using AutoMapper;
using PermissionRequestApp.Application.Common.Dtos;
using PermitRequest.Application.DTOs;
using PermitRequest.Application.Features.Commands;
using PermitRequest.Application.Features.Queries;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Extensions;

namespace PermitRequest.Application.Profiles
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
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.CreateDate.Year))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));
                


            CreateMap<LeaveRequest, LeaveRequestDto>()
              .ForMember(dest => dest.ReqFormNumber, opt => opt.MapFrom(src => src.RequestNumber))
              .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.CreatedBy.FullName))
              .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType.ToString()))
              .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreatedAt.DateTimeToString()))
              .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.DateTimeToString()))
              .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.DateTimeToString()))
              .ForMember(dest => dest.TotalHour, opt => opt.MapFrom(src => src.TotalWorkHourCalculate(src.StartDate, src.EndDate)))
              .ForMember(dest => dest.Workflow, opt => opt.MapFrom(src => src.WorkflowStatus.ToString()));
        }
    }
}
