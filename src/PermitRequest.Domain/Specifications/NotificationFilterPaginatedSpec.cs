using Ardalis.Specification;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Specifications
{
    public class NotificationFilterPaginatedSpec : Specification<Notification>
    {
        public NotificationFilterPaginatedSpec(int skip, int take)
        {
            Query.
                Include(i => i.User).
                Skip(skip).Take(take);

        }
    }
}
