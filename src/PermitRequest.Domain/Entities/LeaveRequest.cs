 
using PermitRequest.Domain.Enums;
using PermitRequest.Core.Commons;

namespace PermitRequest.Domain.Entities
{

    public class LeaveRequest : BaseEntity
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
    }
}
