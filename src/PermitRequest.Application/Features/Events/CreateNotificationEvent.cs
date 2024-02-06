using PermitRequest.Domain.Commons;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Application.Features.Events
{
    public class CreateNotificationEvent(CumulativeLeaveRequest cumulativeLeaveRequest) : DomainEvent
    {
        public CumulativeLeaveRequest CumulativeLeaveRequest = cumulativeLeaveRequest;
    }
}
