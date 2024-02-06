using Ardalis.Specification;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Application.Specifications
{
    public class LeaveRequestFilterPaginatedSpec : Specification<LeaveRequest>
    {
        public LeaveRequestFilterPaginatedSpec(int skip, int take)
        {
            Query.
                Include(i => i.CreatedBy).
                Skip(skip).Take(take);

        }
        public LeaveRequestFilterPaginatedSpec(int skip, int take, Guid userId)
        {
            Query.
              Include(i => i.CreatedBy).
              Skip(skip).Take(take).
              Where(i => i.CreatedById == userId);
        }
    }
}
