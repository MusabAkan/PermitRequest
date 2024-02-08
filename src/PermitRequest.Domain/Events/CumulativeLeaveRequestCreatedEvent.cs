using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Events
{
    public class CumulativeLeaveRequestCreatedEvent(LeaveRequest leaveRequest) : DomainEventBase
    {
        public LeaveRequest LeaveRequest = leaveRequest;
    }

}
