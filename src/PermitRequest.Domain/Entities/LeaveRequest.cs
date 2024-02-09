using PermitRequest.Domain.Enums;
using Ardalis.SharedKernel;
using PermitRequest.Domain.Events;
using PermitRequest.Domain.ValueObjets;
using PermitRequest.Domain.Entities.Base;
using PermitRequest.Domain.Services;

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

        public static LeaveRequest CreateFactory(AdUser user, DateTime startDate, DateTime endDate, LeaveType leaveType, string reason)
        {
            LeaveRequest leaveRequest = new()
            {
                LeaveType = leaveType,
                ReasonExplanation = new ReasonExplanation(reason),
                BetweenDates = new BetweenDate(startDate, endDate),
                CreatedById = user.Id
            };

            leaveRequest.RegisterDomainEvent(new LeaveRequestCreatedEvent(user));

            return leaveRequest;
        }

        public void SetAssignedUserId(Guid? userId) => AssignedUserId = userId;
        public void SetWorkflowStatus(Workflow type) => WorkflowStatus = type;
    }


}
