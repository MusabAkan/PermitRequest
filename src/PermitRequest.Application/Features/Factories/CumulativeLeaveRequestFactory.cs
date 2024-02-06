using PermitRequest.Application.Features.Events;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;

namespace PermitRequest.Application.Features.Factories
{
    public class CumulativeLeaveRequestFactory
    {
        public static CumulativeLeaveRequest CreateCumulativeLeaveRequest(CumulativeLeaveRequest? oldtEntity, Guid userId, LeaveType LeaveTypeId, int total, int year)
        {
            CumulativeLeaveRequest entity;

            if (oldtEntity == null)
                entity = new()
                {
                    LeaveTypeId = LeaveTypeId,
                    UserId = userId,
                    Year = year,
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
