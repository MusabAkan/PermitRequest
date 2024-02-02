using AutoMapper;
using PermitRequest.Application.Commons;
using PermitRequest.Application.Dtos;

namespace PermitRequest.Application.Profiles
{
    public class RequestRecordProifle : Profile
    {
        public RequestRecordProifle()  
        {
            CreateMap<RequestRecordDto, CreateRequestRecordCommand>();
        }
    }
}
