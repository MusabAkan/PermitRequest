using AutoMapper;
using PermissionRequestApp.Domain.Common.Dtos;
using PermitRequest.Application.Commands;
using PermitRequest.Application.Queries;
using PermitRequest.Domain.DTOs;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;

namespace PermitRequest.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RecordRequestDto, CreateRequestRecordCommand>()
                .ConstructUsing(src => new CreateRequestRecordCommand(Guid.Parse(src.userId), DateTime.Parse(src.startDate), DateTime.Parse(src.endDate), (LeaveType)src.leaveType, src.reason));

            CreateMap<GetListRequestDto, GetListLeaveRequestQuery>()
                .ConstructUsing(src => new GetListLeaveRequestQuery(src.skip, src.take));

            CreateMap<GetListRequestDto, GetListUserQuery>()
                .ConstructUsing(src => new GetListUserQuery(src.skip, src.take));

            CreateMap<GetListRequestDto, GetListNotificationRequestQuery>()
               .ConstructUsing(src => new GetListNotificationRequestQuery(src.skip, src.take));

            CreateMap<GetCumulativeLeaveRequestDto, GetListCumulativeLeaveRequestQuery>()
              .ConstructUsing(src => new GetListCumulativeLeaveRequestQuery(src.skip, src.take, Guid.Parse(src.userId),  src.year, (LeaveType)src.leaveType ));

            CreateMap<GetByIdRequestDto, GetByIdLeaveRequestQuery>()
                .ConstructUsing(src => new GetByIdLeaveRequestQuery(src.skip, src.take, src.userId));

            CreateMap<AdUser, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName.ToString()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<CumulativeLeaveRequest, CumulativeResponseDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName.ToString()))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType))
                .ForMember(dest => dest.TotalHour, opt => opt.MapFrom(src => src.TotalHours));

            CreateMap<Notification, NotificationResponseDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName.ToString()))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDateStr))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));

            CreateMap<LeaveRequest, LeaveResponsetDto>()
              .ForMember(dest => dest.ReqFormNumber, opt => opt.MapFrom(src => src.RequestNumber))
              .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.CreatedBy.FullName.ToString()))
              .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType))
              .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreatedAtStr))
              .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.BetweenDates.StartDateStr))
              .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.BetweenDates.EndDateStr))
              .ForMember(dest => dest.TotalHour, opt => opt.MapFrom(src => src.BetweenDates.TotalWorkHours))
              .ForMember(dest => dest.Workflow, opt => opt.MapFrom(src => src.WorkflowStatus));
        }
    }
}
