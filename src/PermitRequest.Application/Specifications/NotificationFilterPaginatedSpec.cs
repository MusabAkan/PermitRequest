using Ardalis.Specification;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Application.Specifications
{
    public class NotificationFilterPaginatedSpec : Specification<Notification>
    {
        public NotificationFilterPaginatedSpec(int skip, int take)
        {
            Query.
                Include(i => i.User).
                OrderByDescending(i => i.CreateDate).
                Skip(skip).Take(take);
        }
    }
}
