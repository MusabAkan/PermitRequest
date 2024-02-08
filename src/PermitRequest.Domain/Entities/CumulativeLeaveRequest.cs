using Ardalis.SharedKernel;
using PermitRequest.Domain.Enums;

namespace PermitRequest.Domain.Entities
{
    public class CumulativeLeaveRequest : BaseEntity, IAggregateRoot
    {
        public LeaveType LeaveTypeId { get; set; }
        public virtual AdUser User { get; set; }
        public Guid UserId { get; set; }
        public int TotalHours { get; set; }
        public int Year { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }    
    }
}
