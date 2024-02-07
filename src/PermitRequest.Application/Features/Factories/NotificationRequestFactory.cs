using PermitRequest.Domain.Entities;

namespace PermitRequest.Application.Features.Factories
{
    public class NotificationRequestFactory
    {
        public static Notification Create(Guid cumulativeLeaveRequestId, Guid userId)
        {
            Notification entity = new()
            {
                CreateDate = DateTime.Now,
                CumulativeLeaveRequestId = cumulativeLeaveRequestId,
                UserId = userId,
            };
            return entity;
        }
    }
}
