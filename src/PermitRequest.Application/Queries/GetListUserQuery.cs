using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.DTOs;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Exceptions;

namespace PermitRequest.Application.Queries
{
    public record GetListUserQuery(int skip, int take) : IQuery<Result<List<UserDto>>>;

    public class GetListUserQueryHandler(IRepository<AdUser> _repository, IMapper _mapper) : IQueryHandler<GetListUserQuery, Result<List<UserDto>>>
    {
        public async Task<Result<List<UserDto>>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            var filterSpec = new UsertFilterPaginatedSpec(request.skip, request.take);

            var data = await _repository.ListAsync(filterSpec);

            if (data == null || data.Count == default)
                throw new ExceptionMessage("Veri yok!!");

            var users = _mapper.Map<List<UserDto>>(data);

            var count = "Total Data : " + users.Count;

            return Result.Success(users, count);
        }
    }
}
