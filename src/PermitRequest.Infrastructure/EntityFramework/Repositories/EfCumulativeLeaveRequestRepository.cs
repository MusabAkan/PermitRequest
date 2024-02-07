using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Infrastructure.EntityFramework.Contexts;
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
