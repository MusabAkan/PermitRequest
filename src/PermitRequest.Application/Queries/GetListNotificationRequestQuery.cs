using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermissionRequestApp.Domain.Common.Dtos;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Services;

namespace PermitRequest.Application.Queries
{
    public record GetListNotificationRequestQuery(int skip, int take) : IQuery<Result<IEnumerable<NotificationResponseDto>>>;

    public class GetListNotificationRequestQueryHandler(INotificationRepository _repository, IMapper _mapper) : IQueryHandler<GetListNotificationRequestQuery, Result<IEnumerable<NotificationResponseDto>>>
    {
        public async Task<Result<IEnumerable<NotificationResponseDto>>> Handle(GetListNotificationRequestQuery request, CancellationToken cancellationToken)
        {

            var filterSpec = new NotificationFilterPaginatedSpec(request.skip, request.take);

            var data = await _repository.ListAsync(filterSpec);

            if (data == null || data.Count == default)
                throw new ExceptionMessage("Veri yok!!");

            var notifications = _mapper.Map<List<NotificationResponseDto>>(data);

            return notifications;
        }
    }
}