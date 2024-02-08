using Ardalis.Specification.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Infrastructure.Contexts;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Infrastructure.EntityFramework.Repositories
{
    public class EfCumulativeLeaveRequestRepository : RepositoryBase<CumulativeLeaveRequest>, ICumulativeLeaveRequestRepository
    {
        public EfCumulativeLeaveRequestRepository(PermitRequestContext dbContext) : base(dbContext)
        {
        }
    }
}
