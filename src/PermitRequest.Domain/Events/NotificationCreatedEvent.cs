using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Events
{
    public class NotificationCreatedEvent(CumulativeLeaveRequest cumulativeLeaveRequest, LeaveRequest leaveRequest ) : DomainEventBase
    {
        public CumulativeLeaveRequest CumulativeLeaveRequest = cumulativeLeaveRequest;
        public LeaveRequest LeaveRequest = leaveRequest;
    }
}
