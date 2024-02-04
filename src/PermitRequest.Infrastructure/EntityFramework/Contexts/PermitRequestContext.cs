using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using System.Reflection;

namespace PermitRequest.Infrastructure.EntityFramework.Contexts
{
    public class PermitRequestContext : DbContext
    {
        
        public PermitRequestContext(DbContextOptions<PermitRequestContext> options) : base(options)
        {

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

    }


}
