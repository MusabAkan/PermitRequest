using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Events
{
    public class NotificationCreatedEvent(CumulativeLeaveRequest cumulativeLeaveRequest, Guid leaveRequestId) : DomainEventBase
    {
        public CumulativeLeaveRequest CumulativeLeaveRequest = cumulativeLeaveRequest;
        public Guid leaveRequestId = leaveRequestId;
    }
}
