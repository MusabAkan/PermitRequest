using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.DTOs;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Services;

namespace PermitRequest.Application.Queries
{
    public record GetByIdLeaveRequestQuery(int skip, int take, Guid userid) : IQuery<Result<IEnumerable<LeaveResponsetDto>>>;

    public class GetByIdLeaveRequestQueryHandler(ILeaveRequestRepository _repository, IMapper _mapper) : IQueryHandler<GetByIdLeaveRequestQuery, Result<IEnumerable<LeaveResponsetDto>>>
    {

        public async Task<Result<IEnumerable<LeaveResponsetDto>>> Handle(GetByIdLeaveRequestQuery request, CancellationToken cancellationToken)
        {

            var filterSpec = new LeaveRequestFilterPaginatedSpec(request.skip, request.take, request.userid);

            var data = await _repository.ListAsync(filterSpec);

            if (data == null || data.Count == default)
                throw new ExceptionMessage("Veri yok!!");

            var leaveRequests = _mapper.Map<List<LeaveResponsetDto>>(data);

            return leaveRequests;
        }

    }

}
