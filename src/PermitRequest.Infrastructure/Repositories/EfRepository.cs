using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;
using PermitRequest.Infrastructure.Contexts;

namespace PermitRequest.Infrastructure.Repositories
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(PermitRequestContext dbContext) : base(dbContext) { }
    }
}
