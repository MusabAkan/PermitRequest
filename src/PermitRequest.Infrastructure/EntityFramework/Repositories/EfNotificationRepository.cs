using Ardalis.Specification.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Infrastructure.EntityFramework.Contexts;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Infrastructure.EntityFramework.Repositories
{
    public class EfNotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public EfNotificationRepository(PermitRequestContext dbContext) : base(dbContext)
        {
        }
    }
}
