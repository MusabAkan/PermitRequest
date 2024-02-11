using Ardalis.Specification;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Application.Specifications
{
    internal class UsertFilterPaginatedSpec : Specification<AdUser>
    {
        public UsertFilterPaginatedSpec(int skip, int take)
        {
            Query.                
                OrderByDescending(i => i.Id).
                Skip(skip).Take(take);
        }
    }
}
