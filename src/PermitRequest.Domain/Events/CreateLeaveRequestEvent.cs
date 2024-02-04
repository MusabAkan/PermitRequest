using Ardalis.SharedKernel;
using PermitRequest.Domain.Common;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Events
{
    public class CreateLeaveRequestEvent(LeaveRequest leaveRequest) : DomainEvent
    {

        public LeaveRequest LeaveRequest = leaveRequest;
    }

}
