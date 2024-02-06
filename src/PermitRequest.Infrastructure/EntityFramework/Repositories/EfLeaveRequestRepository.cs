using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Infrastructure.EntityFramework.Repositories
{
    public class EfLeaveRequestRepository : RepositoryBase<LeaveRequest>, ILeaveRequestRepository
    {
        public EfLeaveRequestRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
