using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Infrastructure.EntityFramework.Repositories
{
    public class EfAdUserRepository : RepositoryBase<AdUser>, IAdUserRepository
    {
        public EfAdUserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
