using Ardalis.SharedKernel;
using PermitRequest.Domain.Commons;
using PermitRequest.Domain.Factories;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Events;
using PermitRequest.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using PermitRequest.Domain.Extensions;
using PermitRequest.Domain.Specifications;
using System.Threading;

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

        /****************   Ingore  *****************/
        public string StartDateStr => StartDate.ToString("dd.MM.yyyy");
        public string EndDateStr => EndDate.ToString("dd.MM.yyyy");
        public string CreatedAtStr => CreatedAt.ToString("dd.MM.yyyy");
        public int Year => StartDate.Year;
        public int TotalWorkHours
        {
            get
            {
                int totalWorkHours = 0;
                const int workHoursPerDay = 8;
                DateTime currentDate = StartDate;

                while (currentDate <= EndDate)
                {
                    if (currentDate.DayOfWeek != DayOfWeek.Sunday)
                        totalWorkHours += workHoursPerDay;
                    currentDate = currentDate.AddDays(1);
                }

                return totalWorkHours;
            }
        }
        /****************   Ingore  *****************/

        public static LeaveRequest CreateLeaveRequestFactory(AdUser user, DateTime startDate, DateTime endDate, LeaveType leaveType, string reason, Guid? managerId, Guid? managerOfManagerId)
        {

            if (startDate.Date >= endDate.Date)
                throw new ExceptionMessage("Başlangıç tarih bitiş tarihden büyük yada eşit olmamalıdır..");

            if (reason.Length > 150)
                throw new ExceptionMessage("Sebep açıklamsında karakter sayısı 150'den fazla olmamalıdır");

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
                WorkflowStatus = result.Item1,
                CreatedBy = user,
                LeaveType = leaveType,
            };

            switch (result.Item2)
            {
                case "ManagerOfManagerCase":
                    entity.AssignedUserId = managerOfManagerId;
                    break;
                case "ManagerCase":
                    entity.AssignedUserId = managerId;
                    break;
                case "EmployeeManagerCase":
                    entity.AssignedUserId = user.Id;
                    break;
                default:
                    entity.AssignedUserId = null;
                    break;
            }

            entity.RaiseDomainEvent(new CreateCumulativeEvent(entity));

            return entity;

        }


    }









}
