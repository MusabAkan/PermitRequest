using AutoMapper;
using PermitRequest.Application.DTOs;
using PermitRequest.Application.Extensions;
using PermitRequest.Application.Features.Commands;
using PermitRequest.Application.Features.Queries;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;

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

            CreateMap<List<LeaveRequest>, List<LeaveRequestDto>>()
              .ConstructUsing(src => (List<LeaveRequestDto>)src.Select(i => new LeaveRequestDto(
                    i.RequestNumber,
                    i.CreatedBy.FullName,
                    i.LeaveType.ToString(),
                    i.CreatedAt.DateTimeToString(),
                    i.StartDate.DateTimeToString(),
                    i.EndDate.DateTimeToString(),
                    i.TotalWorkHourCalculate(i.StartDate, i.EndDate),
                    i.WorkflowStatus.ToString())));




        }
    }
}
