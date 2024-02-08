using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Events
{
    public class NotificationCreatedEvent(CumulativeLeaveRequest cumulativeLeaveRequest) : DomainEventBase
    {
        public CumulativeLeaveRequest CumulativeLeaveRequest = cumulativeLeaveRequest;
    }
}
