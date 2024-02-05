using Ardalis.SharedKernel;
using PermitRequest.Domain.Commons;
using PermitRequest.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermitRequest.Domain.Entities
{
    public class AdUser : BaseEntity, IAggregateRoot
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
        public UserType UserType { get; set; }
        public Guid? ManagerId { get; set; }
        public IEnumerable<LeaveRequest> LeaveRequests { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
        public IEnumerable<CumulativeLeaveRequest> CumulativeLeaveRequests { get; set; }


    }
}
