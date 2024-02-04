using Ardalis.Specification;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Domain.Specifications
{
    public class AdUserSpec : Specification<AdUser>
    {
        public AdUserSpec(Guid userId)
        {
            Query.Where(i => i.Id == userId);          

        }
    }
}
