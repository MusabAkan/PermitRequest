using Ardalis.Specification;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Specifications
{
    public class AdUserSpec : Specification<AdUser>
    {
        public AdUserSpec(string userId)
        {
            Query.Where(i => i.Id == Guid.Parse(userId));          

        }
    }
}
