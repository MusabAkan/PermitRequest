using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.DTOs;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Services;

namespace PermitRequest.Application.Features.Queries
{
    public record GetByIdLeaveRequestQuery(int skip, int take, Guid userid) : IQuery<Result<IEnumerable<LeaveRequestDto>>>;

    public class GetByIdLeaveRequestQueryHandler(ILeaveRequestRepository _repository, IMapper _mapper) : IQueryHandler<GetByIdLeaveRequestQuery, Result<IEnumerable<LeaveRequestDto>>>
    {
        
        public async Task<Result<IEnumerable<LeaveRequestDto>>> Handle(GetByIdLeaveRequestQuery request, CancellationToken cancellationToken)
        {

            var filterSpec = new LeaveRequestFilterPaginatedSpec(request.skip, request.take, request.userid);

            var data = await _repository.ListAsync(filterSpec);

            if (data == null || data.Count == 0)
               throw new ExceptionMessage("Veri yok!!");

            var leaveRequests = _mapper.Map<List<LeaveRequestDto>>(data);

            return leaveRequests;
        }

    }

}
