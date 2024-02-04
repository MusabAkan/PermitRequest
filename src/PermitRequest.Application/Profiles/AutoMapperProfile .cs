using AutoMapper;
using PermitRequest.Application.Commons;
using PermitRequest.Application.DTOs;
using PermitRequest.Domain.Enums;

namespace PermitRequest.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RequestRecordDto, CreateRequestRecordCommand>()
                .ConstructUsing(src => new CreateRequestRecordCommand(src.UserId, DateTime.Parse(src.StartDate), DateTime.Parse(src.EndDate), (LeaveType)src.LeaveType, src.reason));
            //CreateMap<CreateRequestRecordCommand, RequestRecordDto>().ReverseMap();
        }
    }
}
