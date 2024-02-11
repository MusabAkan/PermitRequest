using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.DTOs;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Services;

namespace PermitRequest.Application.Queries
{
    public record GetListCumulativeLeaveRequestQuery(int skip, int take, Guid userId, int? year,LeaveType? leaveType) : IQuery<Result<List<CumulativeResponseDto>>>;  

    public class GetListCumulativeLeaveRequestQueryHandler(ICumulativeLeaveRequestRepository _repository, IMapper _mapper) : IQueryHandler<GetListCumulativeLeaveRequestQuery, Result<List<CumulativeResponseDto>>>
    {
        public async Task<Result<List<CumulativeResponseDto>>> Handle(GetListCumulativeLeaveRequestQuery request, CancellationToken cancellationToken)
        {

            CumulativeLeaveRequestFilterPaginatedSpec filterSpec;

            if (request.leaveType != LeaveType.None && request.year == default)
                filterSpec = new CumulativeLeaveRequestFilterPaginatedSpec(request.skip, request.take, request.userId, request.leaveType);

            else if (request.leaveType == LeaveType.None && request.year != default)
                filterSpec = new CumulativeLeaveRequestFilterPaginatedSpec(request.skip, request.take, request.userId,  request.year);

            else if (request.leaveType != LeaveType.None && request.year != default)
                filterSpec = new CumulativeLeaveRequestFilterPaginatedSpec(request.skip, request.take, request.userId, request.leaveType, request.year);

            else
                filterSpec = new CumulativeLeaveRequestFilterPaginatedSpec(request.skip, request.take, request.userId);

            var data = await _repository.ListAsync(filterSpec);

            if (data == null || data.Count == default)
                throw new ExceptionMessage("Veri yok!!");

            var cumulativeLeaves = _mapper.Map<List<CumulativeResponseDto>>(data);           

            var count = "Total Data : " + cumulativeLeaves.Count;

            return Result.Success(cumulativeLeaves, count);
        }
    }
}
