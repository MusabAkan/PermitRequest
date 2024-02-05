using Ardalis.Result;
using Ardalis.SharedKernel;
using PermissionRequestApp.Domain.Entities.CumulativeLeaveRequestAggregate.Specifications;
using PermitRequest.Application.DTOs;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Application.Features.Queries
{
    public record GetListCumulativeLeaveRequestQuery(int skip, int take) : IQuery<Result<IEnumerable<CumulativeLeaveRequestDto>>>;

    public class GetListCumulativeLeaveRequestQueryHandler(IRepository<CumulativeLeaveRequest> _repository, IRepository<AdUser> _repositoryAdUser) : IQueryHandler<GetListCumulativeLeaveRequestQuery, Result<IEnumerable<CumulativeLeaveRequestDto>>>
    {
        public async Task<Result<IEnumerable<CumulativeLeaveRequestDto>>> Handle(GetListCumulativeLeaveRequestQuery request, CancellationToken cancellationToken)
        {
            var filterSpec = new CumulativeLeaveRequestFilterPaginatedSpec(request.skip, request.take);

            var data = await _repository.ListAsync(filterSpec);

            List<CumulativeLeaveRequestDto> cumlatives = new();

            if (data.Count == 0)
                return Result.Error("Veri yok!!");


            foreach (var item in data)
            {
                cumlatives.Add(item: new CumulativeLeaveRequestDto(

                    item.User.FullName,
                    item.LeaveTypeId.ToString(),
                    item.Year.ToString(),
                    item.TotalHours.ToString())
                );
            }
            return cumlatives;
        }
    }
}
