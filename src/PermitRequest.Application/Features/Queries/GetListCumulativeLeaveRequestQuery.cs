using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.DTOs;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Services;

namespace PermitRequest.Application.Features.Queries
{
    public record GetListCumulativeLeaveRequestQuery(int skip, int take) : IQuery<Result<IEnumerable<CumulativeLeaveRequestDto>>>;

    public class GetListCumulativeLeaveRequestQueryHandler(ICumulativeLeaveRequestRepository _repository, IMapper _mapper) : IQueryHandler<GetListCumulativeLeaveRequestQuery, Result<IEnumerable<CumulativeLeaveRequestDto>>>
    {
        public async Task<Result<IEnumerable<CumulativeLeaveRequestDto>>> Handle(GetListCumulativeLeaveRequestQuery request, CancellationToken cancellationToken)
        {
            var filterSpec = new CumulativeLeaveRequestFilterPaginatedSpec(request.skip, request.take);

            var data = await _repository.ListAsync(filterSpec);          

            if (data == null || data.Count == 0)
                throw new ExceptionMessage("Veri yok!!");

            var cumulativeLeaves = _mapper.Map<List<CumulativeLeaveRequestDto>>(data);

            return cumulativeLeaves;
        }
    }
}
