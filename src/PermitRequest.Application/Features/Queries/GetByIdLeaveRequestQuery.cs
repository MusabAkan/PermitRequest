using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermitRequest.Application.DTOs;
using PermitRequest.Application.Extensions;
using PermitRequest.Domain.Entities;
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

            if (data == null)
                return Result.Error("Veri yok!!");

            _mapper.Map<LeaveRequestDto>(data);

            List<LeaveRequestDto> leaves = new();
            
            return leaves;
        }

    }

}
 