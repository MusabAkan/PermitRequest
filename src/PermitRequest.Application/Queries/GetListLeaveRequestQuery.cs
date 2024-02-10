using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.DTOs;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Services;

namespace PermitRequest.Application.Queries
{
    public record GetListLeaveRequestQuery(int skip, int take) : IQuery<Result<IEnumerable<LeaveResponsetDto>>>;

 
    public class GetListLeaveRequestQueryHandler(ILeaveRequestRepository _repository, IMapper _mapper) : IQueryHandler<GetListLeaveRequestQuery, Result<IEnumerable<LeaveResponsetDto>>>
    {
        public async Task<Result<IEnumerable<LeaveResponsetDto>>> Handle(GetListLeaveRequestQuery request, CancellationToken cancellationToken)
        {

            var filterSpec = new LeaveRequestFilterPaginatedSpec(request.skip, request.take);
            

            var data = await _repository.ListAsync(filterSpec);


            if (data == null || data.Count == default)
                throw new ExceptionMessage("Veri yok!!");

            var leaveRequests = _mapper.Map<List<LeaveResponsetDto>>(data);

            return  leaveRequests;
        }
    }
}

