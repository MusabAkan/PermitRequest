using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Infrastructure.EntityFramework.Services;

namespace PermitRequest.Infrastructure.EntityFramework.Repositories
{
    public class EfNotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public EfNotificationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
