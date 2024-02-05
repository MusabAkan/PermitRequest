using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermitRequest.Application.Constants;
using PermitRequest.Application.DTOs;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Specifications;

namespace PermitRequest.Application.Features.Queries
{
    public record GetListLeaveRequestQuery(int skip, int take) : IQuery<Result<IEnumerable<LeaveRequestDto>>>;
    public class GetListLeaveRequestQueryHandler(IRepository<LeaveRequest> _repository, IMapper _mapper) : IQueryHandler<GetListLeaveRequestQuery, Result<IEnumerable<LeaveRequestDto>>>
    {
        public async Task<Result<IEnumerable<LeaveRequestDto>>> Handle(GetListLeaveRequestQuery request, CancellationToken cancellationToken)
        {

            var filterSpec = new LeaveRequestFilterPaginatedSpec(request.skip, request.take);

            var data = await _repository.ListAsync(filterSpec);

            if (data == null || data.Count == 0)
                return Result.Error(Message.NoData);

            var leaveRequests = _mapper.Map<List<LeaveRequestDto>>(data);

            return leaveRequests;
        }
    }
}

