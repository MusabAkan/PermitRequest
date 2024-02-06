using Ardalis.Specification;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;

namespace PermitRequest.Application.Specifications
{
    public class CumulativeLeaveSpec : Specification<CumulativeLeaveRequest>
    {

        public CumulativeLeaveSpec(Guid userId, LeaveType leaveType, int year)
        {
            Query.Where(i => i.UserId == userId && i.LeaveTypeId == leaveType && year == i.Year);
        }
    }
}
