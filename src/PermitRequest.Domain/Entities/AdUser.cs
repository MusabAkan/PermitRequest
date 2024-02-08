
using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities.Base;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.ValueObjets;

namespace PermitRequest.Domain.Entities
{
    public class AdUser : BaseEntity, IAggregateRoot
    {
        public FullName FullName { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public Guid? ManagerId { get; set; }
        public IEnumerable<LeaveRequest> LeaveRequests { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
        public IEnumerable<CumulativeLeaveRequest> CumulativeLeaveRequests { get; set; }
    }
}
