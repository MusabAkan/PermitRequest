using PermitRequest.Domain.Commons;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Events
{
    public class CreateCumulativeEvent(LeaveRequest leaveRequest) : DomainEvent
    {
        public LeaveRequest LeaveRequest = leaveRequest;
    }

}
