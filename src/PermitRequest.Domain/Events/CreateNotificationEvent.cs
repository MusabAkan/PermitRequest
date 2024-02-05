using PermitRequest.Domain.Commons;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Events
{
    public class CreateNotificationEvent(CumulativeLeaveRequest cumulativeLeaveRequest) : DomainEvent
    {
        public CumulativeLeaveRequest CumulativeLeaveRequest = cumulativeLeaveRequest;
    }

}
