using Ardalis.Specification.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Services;
using PermitRequest.Infrastructure.Contexts;

namespace PermitRequest.Infrastructure.EntityFramework.Repositories
{
    public class EfNotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public EfNotificationRepository(PermitRequestContext dbContext) : base(dbContext) { }
    }
}
