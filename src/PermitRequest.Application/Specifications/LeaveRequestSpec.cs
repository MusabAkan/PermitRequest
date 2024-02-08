using Ardalis.Specification;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Application.Specifications
{
    internal class LeaveRequestSpec : Specification<LeaveRequest>
    {
        public LeaveRequestSpec(Guid Id)
        {
            Query.
             Include(i => i.CreatedBy).              
             Where(i => i.Id == Id);
        }
    }
}
