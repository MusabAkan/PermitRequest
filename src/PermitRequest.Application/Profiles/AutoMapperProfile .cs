using AutoMapper;
using PermitRequest.Application.Commons;
using PermitRequest.Application.Dtos;

namespace PermitRequest.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RequestRecordDto, CreateRequestRecordCommand>()
                .ConstructUsing(src => new CreateRequestRecordCommand(src.UserId, src.StartTime, src.EndTime, src.LeaveType, src.reason));
            //CreateMap<CreateRequestRecordCommand, RequestRecordDto>().ReverseMap();
        }
    }
}
