using PermitRequest.Domain.Enums;
using Ardalis.SharedKernel;
using PermitRequest.Domain.Events;
using PermitRequest.Domain.ValueObjets;
using PermitRequest.Domain.Entities.Base;
using PermitRequest.Domain.Extensions;

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
        private LeaveRequest(Guid userId, LeaveType leaveType, string reason,  DateTime start, DateTime end, UserType userType)
        {
            var result = CreateWorkflow(leaveType, userType);      
            
            LeaveType = leaveType;
            WorkflowStatus = result.Item1;
            ReasonExplanation = new  ReasonExplanation(reason);
            BetweenDates = new BetweenDate(start, end);    
            

            static (Workflow, string?) CreateWorkflow(LeaveType leaveType, UserType userType)
            {
                switch (userType)
                {
                    case UserType.WhiteCollarEmployee:
                        return (Workflow.Pending, "employeee managerin bilgisi");

                    case UserType.BlueCollarEmployee when leaveType == LeaveType.AnnualLeave:
                        return (Workflow.Pending, "Manager’ın manager bilgisi (Kemal Sunal için Münir Özkul olmalı)");

                    case UserType.BlueCollarEmployee when leaveType == LeaveType.ExcusedAbsence:
                        return (Workflow.Pending, "Manager bilgisi");

                    case UserType.Manager:
                        return (Workflow.None, null);

                    default:
                        throw new ExceptionMessage("UserType  yada LeaveType bulunamadı");
                }
            }
        }
        public static LeaveRequest CreateFactory(Guid userId, DateTime startDate, DateTime endDate, LeaveType leaveType, string reason, UserType userType)
        {

            LeaveRequest leaveRequest = new(userId, leaveType, reason, startDate, endDate, userType);         

            leaveRequest.RegisterDomainEvent(new CumulativeLeaveRequestCreatedEvent(leaveRequest));

            return leaveRequest;
        }
       
    }
}
