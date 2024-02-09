using Ardalis.Specification.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Services;
using PermitRequest.Infrastructure.Contexts;

namespace PermitRequest.Infrastructure.EntityFramework.Repositories
{
    public class EfCumulativeLeaveRequestRepository : RepositoryBase<CumulativeLeaveRequest>, ICumulativeLeaveRequestRepository
    {
        public EfCumulativeLeaveRequestRepository(PermitRequestContext dbContext) : base(dbContext)
        {
        }
    }
}
