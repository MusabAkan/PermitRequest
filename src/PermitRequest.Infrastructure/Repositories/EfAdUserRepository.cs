using Ardalis.Specification.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Infrastructure.Contexts;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Infrastructure.Repositories
{
    public class EfAdUserRepository : RepositoryBase<AdUser>, IAdUserRepository
    {
        public EfAdUserRepository(PermitRequestContext dbContext) : base(dbContext) { }
    }
}
