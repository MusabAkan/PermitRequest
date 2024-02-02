using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;
using PermitRequest.Infrastructure.EntityFramework.Contexts;

namespace PermitRequest.Infrastructure.EntityFramework.Repositories
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(PermitRequestContext dbContext) : base(dbContext)
        {
        }
    }
}
