using Ardalis.SharedKernel;
using PermitRequest.Domain.Entities.Base;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Events;

namespace PermitRequest.Domain.Entities
{
    public class CumulativeLeaveRequest : BaseEntity, IAggregateRoot
    {
        public LeaveType LeaveTypeId { get; set; }
        public virtual AdUser User { get; set; }
        public Guid UserId { get; set; }
        public int TotalHours { get; set; }
        public int Year { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }

        private CumulativeLeaveRequest(LeaveType leaveTypeId, Guid userId, int totalHours, int year)
        {
            LeaveTypeId = leaveTypeId;
            UserId = userId;
            TotalHours = totalHours;
            Year = year;
        }      

        public static CumulativeLeaveRequest CreateFactory(CumulativeLeaveRequest? oldEntity, Guid userId, LeaveType LeaveTypeId, int total, int year, Guid leaveRequestId)
        {
            CumulativeLeaveRequest entity;

            if (oldEntity is null)
                entity = new(LeaveTypeId, userId, total, year);
            else
            {
                entity = oldEntity;
                entity.TotalHours += total;
            }
            entity.RegisterDomainEvent(new NotificationCreatedEvent(entity, leaveRequestId));

            return entity;
        }
    }
}
