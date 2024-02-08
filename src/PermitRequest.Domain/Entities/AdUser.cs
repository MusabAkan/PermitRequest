
using Ardalis.SharedKernel;
using PermitRequest.Domain.Enums;

namespace PermitRequest.Domain.Entities
{
    public class AdUser : BaseEntity, IAggregateRoot
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public UserType UserType { get; set; }
        public Guid? ManagerId { get; set; }
        public IEnumerable<LeaveRequest> LeaveRequests { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
        public IEnumerable<CumulativeLeaveRequest> CumulativeLeaveRequests { get; set; }
        /****************   Ingore  *****************/
        public string FullName { get { return FirstName + " " + LastName; } }
        /****************   Ingore  *****************/

    }
}
