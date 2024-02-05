using Ardalis.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Commons;
using PermitRequest.Domain.Entities;
using System.Reflection;

namespace PermitRequest.Infrastructure.EntityFramework.Contexts
{
    public class PermitRequestContext : DbContext
    {
        private readonly IPublisher publisher;
        public PermitRequestContext(DbContextOptions<PermitRequestContext> options, IPublisher publisher) : base(options)
        {
            this.publisher = publisher;
        }

        public DbSet<AdUser> AdUsers { get; set; }
        public DbSet<CumulativeLeaveRequest> CumulativeLeaveRequests { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }
        public DbSet<Notification> Notification { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEventsAsync();       
            return result;
        }

        async Task PublishDomainEventsAsync()
        {
            var domainEntities = ChangeTracker.Entries<BaseEntity>()
                 .Where(x => x.Entity.GetDomainEvents().Any() && x.Entity.GetDomainEvents() != null);

            var domainEvents = domainEntities
              .SelectMany(x => x.Entity.GetDomainEvents())
              .ToList();

            domainEntities.ToList()
                .ForEach(x => x.Entity.ClearDomainEvents());

            foreach(var domainEvent in domainEvents)
                await publisher.Publish(domainEvent);
        }
    }

}
