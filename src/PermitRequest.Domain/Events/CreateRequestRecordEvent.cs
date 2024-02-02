using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Events
{
    public class CreateRequestRecordEvent(LeaveRequest LeaveRequest) : DomainEventBase
    {
        public LeaveRequest LeaveRequest = LeaveRequest;
    } 
     
}
