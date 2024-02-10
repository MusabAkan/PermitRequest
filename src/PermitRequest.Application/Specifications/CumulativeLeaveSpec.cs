using Ardalis.Specification;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Application.Specifications
{
    public class CumulativeLeaveSpec : SingleResultSpecification<CumulativeLeaveRequest>
    {
        public CumulativeLeaveSpec(LeaveRequest leaveRequest)
        {
            Query.Where(i => i.UserId == leaveRequest.CreatedById && i.LeaveType == leaveRequest.LeaveType && leaveRequest.BetweenDates.StartDate.Year == i.Year);
        }
    }
}
