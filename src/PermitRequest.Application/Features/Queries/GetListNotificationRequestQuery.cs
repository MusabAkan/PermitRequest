using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermissionRequestApp.Application.Common.Dtos;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Specifications;

namespace PermitRequest.Application.Features.Queries
{
    public record GetListNotificationRequestQuery(int skip, int take) : IQuery<Result<IEnumerable<NotificationDto>>>;

    public class GetListNotificationRequestQueryHandler(IRepository<Notification> _repository, IMapper _mapper) : IQueryHandler<GetListNotificationRequestQuery, Result<IEnumerable<NotificationDto>>>
    {
        public async Task<Result<IEnumerable<NotificationDto>>> Handle(GetListNotificationRequestQuery request, CancellationToken cancellationToken)
        {

            var filterSpec = new NotificationFilterPaginatedSpec(request.skip, request.take);

            var data = await _repository.ListAsync(filterSpec);

            if (data == null || data.Count == 0)
                throw new ExceptionMessage("Veri yok!!");

            var notifications = _mapper.Map<List<NotificationDto>>(data);

            return  notifications;
        }
    }
}