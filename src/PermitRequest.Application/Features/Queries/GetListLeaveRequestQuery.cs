using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.DTOs;
using PermitRequest.Domain.Extensions;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Application.Features.Queries
{
    public record GetListLeaveRequestQuery(int skip, int take) : IQuery<Result<IEnumerable<LeaveRequestDto>>>;
    public class GetListLeaveRequestQueryHandler(ILeaveRequestRepository _repository, IMapper _mapper) : IQueryHandler<GetListLeaveRequestQuery, Result<IEnumerable<LeaveRequestDto>>>
    {
        public async Task<Result<IEnumerable<LeaveRequestDto>>> Handle(GetListLeaveRequestQuery request, CancellationToken cancellationToken)
        {

            var filterSpec = new LeaveRequestFilterPaginatedSpec(request.skip, request.take);

            var data = await _repository.ListAsync(filterSpec);

            if (data == null || data.Count == 0)
                throw new ExceptionMessage("Veri yok!!");

            var leaveRequests = _mapper.Map<List<LeaveRequestDto>>(data);

            return leaveRequests;
        }
    }
}

