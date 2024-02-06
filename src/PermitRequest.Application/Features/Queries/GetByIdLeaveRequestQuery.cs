using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermitRequest.Application.DTOs;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Specifications;

namespace PermitRequest.Application.Features.Queries
{
    public record GetByIdLeaveRequestQuery(int skip, int take, Guid userid) : IQuery<Result<IEnumerable<LeaveRequestDto>>>;

    public class GetByIdLeaveRequestQueryHandler(IRepository<LeaveRequest> _repository, IMapper _mapper) : IQueryHandler<GetByIdLeaveRequestQuery, Result<IEnumerable<LeaveRequestDto>>>
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
