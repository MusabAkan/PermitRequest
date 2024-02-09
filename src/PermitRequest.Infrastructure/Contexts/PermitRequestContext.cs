using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Entities.Base;
using System.Reflection;
namespace PermitRequest.Infrastructure.Contexts
{
    public class PermitRequestContext : DbContext
    {
        private readonly IDomainEventDispatcher? _dispatcher;
        public PermitRequestContext(DbContextOptions<PermitRequestContext> options, IDomainEventDispatcher? dispatcher) : base(options)
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
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            if (_dispatcher == null) return result;

            var entities = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any()).ToList();

            await _dispatcher.DispatchAndClearEvents(entities);

            return result;
        }

    }

}
