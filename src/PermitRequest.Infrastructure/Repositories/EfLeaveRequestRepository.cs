using Ardalis.Specification.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Services;
using PermitRequest.Infrastructure.Contexts;

namespace PermitRequest.Infrastructure.EntityFramework.Repositories
{
    public class EfLeaveRequestRepository : RepositoryBase<LeaveRequest>, ILeaveRequestRepository
    {
        public EfLeaveRequestRepository(PermitRequestContext dbContext) : base(dbContext)
        {
        }
    }
}
