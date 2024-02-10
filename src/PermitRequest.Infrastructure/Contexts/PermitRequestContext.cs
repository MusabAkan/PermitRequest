using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Common;
using PermitRequest.Domain.Entities;
using System.Reflection;
namespace PermitRequest.Infrastructure.Contexts
{
    public class PermitRequestContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;
        public PermitRequestContext(DbContextOptions<PermitRequestContext> options, IDomainEventDispatcher dispatcher) : base(options)
        {
            _dispatcher = dispatcher;
        }
        public DbSet<AdUser> AdUsers { get; set; }
        public DbSet<CumulativeLeaveRequest> CumulativeLeaveRequests { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await SetTimestamps();

            int result = await base.SaveChangesAsync(cancellationToken);           

            var entities = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any()).ToList();

            if(entities.Any())
                await _dispatcher.DispatchAndClearEvents(entities);

            return result;
        }

        private async Task SetTimestamps()
        {
            var entities = ChangeTracker.Entries()
             .Where(x => x.Entity is LeaveRequest && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.Now;

                if (entity.State == EntityState.Added)
                    ((LeaveRequest)entity.Entity).CreatedAt = now;

                ((LeaveRequest)entity.Entity).LastModifiedAt = now;
            }
        }
    }

}
