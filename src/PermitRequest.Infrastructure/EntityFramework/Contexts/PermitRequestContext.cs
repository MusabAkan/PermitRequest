using Ardalis.Specification;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Common;
using PermitRequest.Domain.Entities;
using System.Reflection;

namespace PermitRequest.Infrastructure.EntityFramework.Contexts
{
    public class PermitRequestContext : DbContext
    {
        private readonly IPublisher _publisher;
        public PermitRequestContext(DbContextOptions<PermitRequestContext> options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
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
            var domainEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e =>  e.GetDomainEvents());

            var result = await base.SaveChangesAsync(cancellationToken);          

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }


            return result;
        }

        //async Task PublishDomainEventsAsync()
        //{
        //    var events = ChangeTracker
        //        .Entries<BaseEntity>()
        //        .Select(e => e.Entity)
        //            .SelectMany(e =>
        //            {
        //                var domainEvents = e.DomainEvents;
        //                //e.ClearDomainEvents();
        //                return domainEvents;
        //            })
        //        .ToList();

        //    foreach (var @event in events)
        //    {               
        //        await publisher.Publish(@event);               
        //    }
        //}      
    }

}
