using Ardalis.Specification;
using PermitRequest.Domain.Entities;

namespace PermissionRequestApp.Domain.Entities.CumulativeLeaveRequestAggregate.Specifications
{
    public class CumulativeLeaveRequestFilterPaginatedSpec : Specification<CumulativeLeaveRequest>
    {
        public CumulativeLeaveRequestFilterPaginatedSpec(int skip, int take)
        {
            Query.
                Include(i => i.User).
                Skip(skip).Take(take);

        }
    }
}
