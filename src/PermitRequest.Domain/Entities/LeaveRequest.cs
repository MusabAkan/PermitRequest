
using PermitRequest.Domain.Enums;
using Ardalis.SharedKernel;
using PermitRequest.Domain.Events;
using PermitRequest.Domain.ValueObjets;

namespace PermitRequest.Domain.Entities
{

    public class LeaveRequest : BaseEntity, IAggregateRoot
    {
        public long FormNumber { get; set; }
        public string RequestNumber { get; set; }
        public LeaveType LeaveType { get; set; }
        public ReasonExplanation? ReasonExplanation { get; set; }
        public BetweenDate BetweenDates { get; set; }
        public Workflow WorkflowStatus { get; set; }
        public Guid? AssignedUserId { get; set; }
        public virtual AdUser CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtStr => CreatedAt.ToString("dd.MM.yyyy");
        public Guid? LastModifiedById { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        public static LeaveRequest CreateFactory(Guid userId, DateTime startDate, DateTime endDate, LeaveType leaveType, string reason)
        {
            LeaveRequest leaveRequest = new()
            {
                CreatedById = userId,
                LeaveType = leaveType,
                ReasonExplanation = new ReasonExplanation(reason),  
                BetweenDates = new BetweenDate(startDate, endDate),
            };

            leaveRequest.RegisterDomainEvent(new CumulativeLeaveRequestCreatedEvent(leaveRequest));

            return leaveRequest;
        }
    }
}
