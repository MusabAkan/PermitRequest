using MediatR;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Events
{
    public class CreateNotificationEvent(CumulativeLeaveRequest cumulativeLeaveRequest) : INotification
    {
        public CumulativeLeaveRequest CumulativeLeaveRequest = cumulativeLeaveRequest;
    }
}
