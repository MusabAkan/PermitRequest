
using Ardalis.SharedKernel;
using PermitRequest.Core.Commons;

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

        /****************   Ingore  *****************/
        public string CreateDateStr => CreateDate.ToString("dd.MM.yyyy HH:ss");
        public int Year => CreateDate.Year;
        /****************   Ingore  *****************/       
    }
}
