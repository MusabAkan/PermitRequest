
using Ardalis.SharedKernel;
using PermitRequest.Domain.Commons;
using PermitRequest.Domain.Enums;

namespace PermitRequest.Domain.Entities
{

    public class Notification : BaseEntity, IAggregateRoot
    {

        public AdUser User { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual CumulativeLeaveRequest CumulativeLeaveRequest { get; set; }
        public Guid CumulativeLeaveRequestId { get; set; }

        public static Notification CreateNotificationRequestFactory(Guid cumulativeLeaveRequestId, Guid userId)
        {
            Notification entity = new()
            {
                CreateDate = DateTime.Now,
                CumulativeLeaveRequestId = cumulativeLeaveRequestId,
                UserId = userId,                
            };

            return entity;
        }
    }
}
