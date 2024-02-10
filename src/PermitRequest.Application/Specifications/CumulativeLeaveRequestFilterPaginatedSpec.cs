using Ardalis.Specification;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;

namespace PermitRequest.Application.Specifications
{
    public class CumulativeLeaveRequestFilterPaginatedSpec : Specification<CumulativeLeaveRequest>
    {
        public CumulativeLeaveRequestFilterPaginatedSpec(int skip, int take, Guid userId)
        {
            Query.
                Include(i => i.User).
                Where(i => i.UserId == userId).
                OrderByDescending(i => i.Year).
                Skip(skip).Take(take);
        }

        public CumulativeLeaveRequestFilterPaginatedSpec(int skip, int take, Guid userId, int? year)
        {
            Query.
                Include(i => i.User).
                Where(i => i.UserId == userId && i.Year == year).
                OrderByDescending(i => i.Year).
                Skip(skip).Take(take);
        }

        public CumulativeLeaveRequestFilterPaginatedSpec(int skip, int take, Guid userId, LeaveType? leaveType)
        {
            Query.
              Include(i => i.User).
              Where(i => i.UserId == userId && i.LeaveType == leaveType).
              OrderByDescending(i => i.Year).
              Skip(skip).Take(take);
        }

        public CumulativeLeaveRequestFilterPaginatedSpec(int skip, int take, Guid userId, LeaveType? leaveType, int? year)
        {
            Query.
                Include(i => i.User).
                Where(i => i.UserId == userId && i.LeaveType == leaveType && i.Year == year).
                OrderByDescending(i => i.Year).
                Skip(skip).Take(take);

        }


    }
}
