using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Events
{
    public class LeaveRequestCreatedEvent(AdUser adUser) : DomainEventBase
    { 
        public AdUser AdUser = adUser;
    }
}
