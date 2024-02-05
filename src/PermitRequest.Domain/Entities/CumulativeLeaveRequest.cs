using Ardalis.SharedKernel;
using PermitRequest.Domain.Commons;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.Events;
using PermitRequest.Domain.Extensions;

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

        public static CumulativeLeaveRequest CreateCumulativeLeaveRequestFactory(CumulativeLeaveRequest? oldtEntity, Guid userId, LeaveType LeaveTypeId, DateTime startDate, DateTime endDate)
        {
            CumulativeLeaveRequest entity;

            var total = new object().TotalWorkHourCalculate(startDate, endDate);

            if (oldtEntity == null)
                entity = new()
                {
                    LeaveTypeId = LeaveTypeId,
                    UserId = userId,
                    Year = startDate.Year,
                    TotalHours = total
                };
            else
            {
                entity = oldtEntity;
                entity.TotalHours += total;  
            }

            entity.RaiseDomainEvent(new CreateNotificationEvent(entity));

            return entity;
        }

    }
}
