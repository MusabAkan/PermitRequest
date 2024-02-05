using Ardalis.Result;
using Ardalis.SharedKernel;
using PermissionRequestApp.Application.Common.Dtos;
using PermitRequest.Application.Extensions;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Specifications;

namespace PermitRequest.Application.Features.Queries
{
    public record GetListNotificationRequestQuery(int skip, int take) : IQuery<Result<IEnumerable<NotificationDto>>>;

    public class GetListNotificationRequestQueryHandler(IRepository<Notification> _repository) : IQueryHandler<GetListNotificationRequestQuery, Result<IEnumerable<NotificationDto>>>
    {
        public async Task<Result<IEnumerable<NotificationDto>>> Handle(GetListNotificationRequestQuery request, CancellationToken cancellationToken)
        {

            var filterSpec = new NotificationFilterPaginatedSpec(request.skip, request.take);

            var data = await _repository.ListAsync(filterSpec);

            if (data == null)
                return Result.Error("Veri yok!!");

            List<NotificationDto> notification = new();

            foreach (var item in data)
            {

                notification.Add(new NotificationDto(
                     item.CreateDate.DateTimeToString(),
                     item.Message,
                     item.CreateDate.DateTimeYearToString(),
                     item.User.FullName
                ));
            }
            return notification;
        }
    }
}