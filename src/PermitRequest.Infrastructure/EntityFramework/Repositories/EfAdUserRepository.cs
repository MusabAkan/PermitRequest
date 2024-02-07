using Ardalis.Specification.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Infrastructure.EntityFramework.Contexts;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Infrastructure.EntityFramework.Repositories
{
    public class EfAdUserRepository : RepositoryBase<AdUser>, IAdUserRepository
    {
        public EfAdUserRepository(PermitRequestContext dbContext) : base(dbContext)
        {
        }
    }
}
