using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using PermissionRequestApp.Domain.Common.Dtos;
using PermitRequest.Application.Specifications;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Exceptions;

namespace PermitRequest.Application.Queries
{
    public record GetListNotificationRequestQuery(int skip, int take) : IQuery<Result<List<NotificationResponseDto>>>;

    public class GetListNotificationRequestQueryHandler(IRepository<Notification> _repository, IMapper _mapper) : IQueryHandler<GetListNotificationRequestQuery, Result<List<NotificationResponseDto>>>
    {
        public async Task<Result<List<NotificationResponseDto>>> Handle(GetListNotificationRequestQuery request, CancellationToken cancellationToken)
        {

            var filterSpec = new NotificationFilterPaginatedSpec(request.skip, request.take);

            var data = await _repository.ListAsync(filterSpec);

            if (data == null || data.Count == default)
                throw new ExceptionMessage("Veri yok!!");

            var notifications = _mapper.Map<List<NotificationResponseDto>>(data);            

            var count = "Total Data : " + notifications.Count;

            return Result.Success(notifications, count);             
        }
    }
}