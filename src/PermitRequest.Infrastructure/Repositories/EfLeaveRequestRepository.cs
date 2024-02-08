using Ardalis.Specification.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Infrastructure.Contexts;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Infrastructure.EntityFramework.Repositories
{
    public class EfLeaveRequestRepository : RepositoryBase<LeaveRequest>, ILeaveRequestRepository
    {
        public EfLeaveRequestRepository(PermitRequestContext dbContext) : base(dbContext)
        {
        }
    }
}
