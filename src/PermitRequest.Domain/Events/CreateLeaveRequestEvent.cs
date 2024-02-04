using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Events
{
    public class CreateLeaveRequestEvent(LeaveRequest leaveRequest ) : DomainEventBase
    {
        public LeaveRequest LeaveRequest = leaveRequest;
    } 
     
}
