using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermissionRequestApp.Domain.Entities.CumulativeLeaveRequestAggregate.Specifications;
using PermitRequest.Application.Constants;
using PermitRequest.Application.DTOs;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Application.Features.Queries
{
    public record GetListCumulativeLeaveRequestQuery(int skip, int take) : IQuery<Result<IEnumerable<CumulativeLeaveRequestDto>>>;

    public class GetListCumulativeLeaveRequestQueryHandler(IRepository<CumulativeLeaveRequest> _repository, IMapper _mapper) : IQueryHandler<GetListCumulativeLeaveRequestQuery, Result<IEnumerable<CumulativeLeaveRequestDto>>>
    {
        public async Task<Result<IEnumerable<CumulativeLeaveRequestDto>>> Handle(GetListCumulativeLeaveRequestQuery request, CancellationToken cancellationToken)
        {
            var filterSpec = new CumulativeLeaveRequestFilterPaginatedSpec(request.skip, request.take);

            var data = await _repository.ListAsync(filterSpec);          

            if (data == null || data.Count == 0)
                return Result.Error(Message.NoData);

            var cumulativeLeaves = _mapper.Map<List<CumulativeLeaveRequestDto>>(data);

            return cumulativeLeaves;
        }
    }
}
