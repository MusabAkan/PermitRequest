using Ardalis.SharedKernel;
using PermitRequest.Domain.Common;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Events;

namespace PermitRequest.Domain.Entities
{
    public class CumulativeLeaveRequest : BaseEntity, IAggregateRoot
    {
        public LeaveType LeaveType { get; set; }
        public virtual AdUser User { get; set; }
        public Guid UserId { get; set; }
        public int TotalHours { get; set; }
        public int Year { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
        public static CumulativeLeaveRequest CreateFactory(CumulativeLeaveRequest? oldCumlativeEntity, LeaveRequest leaveEntity)
        {
            CumulativeLeaveRequest newCumlativeEntity;

            var total = leaveEntity.BetweenDates.TotalWorkHours;
            var userId = leaveEntity.CreatedById;
            var leaveType = leaveEntity.LeaveType;
            var year = leaveEntity.BetweenDates.Year;

            if (oldCumlativeEntity == null)
                newCumlativeEntity = new()
                {
                    LeaveType = leaveType,
                    UserId = userId,
                    Year = year,
                    TotalHours = total
                };
            else
            {
                newCumlativeEntity = oldCumlativeEntity;
                newCumlativeEntity.SetTotalHours(total);
            }

            newCumlativeEntity.RegisterDomainEvent(new NotificationCreatedEvent(leaveEntity));

            return newCumlativeEntity;
        }
        public void SetTotalHours(int total, string value = "+") => TotalHours += (value.Contains('+') ? total : -total);
    }
}
