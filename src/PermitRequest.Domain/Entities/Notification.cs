﻿
using Ardalis.SharedKernel;
using PermitRequest.Domain.Common.Base;

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
    }
}
