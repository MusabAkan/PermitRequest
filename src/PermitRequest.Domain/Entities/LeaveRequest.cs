using Ardalis.SharedKernel;
using PermitRequest.Domain.Commons;
using PermitRequest.Domain.Factories;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Events;
using PermitRequest.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermitRequest.Domain.Entities
{

    public class LeaveRequest : BaseEntity, IAggregateRoot
    {

        public long FormNumber { get; set; }
        public string RequestNumber { get; set; }
        public LeaveType LeaveType { get; set; }
        public string? Reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Workflow WorkflowStatus { get; set; }
        //public virtual AdUser? AssignedUser { get; set; }
        public Guid? AssignedUserId { get; set; }
        public virtual AdUser CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        //public virtual AdUser? LastModifiedBy { get; set; }
        public Guid? LastModifiedById { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        [NotMapped]
        public string AssignedUserStr { get; set; }

        public static LeaveRequest CreateLeaveRequestFactory(AdUser user, DateTime startDate, DateTime endDate, LeaveType leaveType, string reason)
        { 
            static IWorkflowFactory CreateWorkflowFactory(UserType userType, LeaveType leaveType)
            {
                switch (userType)
                {
                    case UserType.WhiteCollarEmployee:
                        return new WhiteCollarEmployeeWorkflowFactory();
                    case UserType.BlueCollarEmployee:
                        if (leaveType == LeaveType.AnnualLeave)
                            return new BlueCollarEmployeeAnnualLeaveWorkflowFactory();
                        else if (leaveType == LeaveType.ExcusedAbsence)
                            return new BlueCollarEmployeeExcusedAbsenceWorkflowFactory();
                        else
                            throw new ArgumentOutOfRangeException();
                    case UserType.Manager:
                        return new ManagerWorkflowFactory();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            var result = CreateWorkflowFactory(user.UserType, leaveType).CreateWorkflow();

            LeaveRequest entity = new()
            {
                StartDate = startDate,
                EndDate = endDate,
                Reason = reason,
                LeaveType = leaveType,
                WorkflowStatus = result.Item1,
                AssignedUserStr = result.Item2,
                CreatedBy = user,
            };

            entity.RaiseDomainEvent(new CreateCumulativeEvent(entity));          

            return entity;

        }


    }









}
