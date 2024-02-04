using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Infrastructure.EntityFramework.Contexts
{
    public class PermitRequestContext : DbContext
    {
        public PermitRequestContext()
        {

        }
        public PermitRequestContext(DbContextOptions<PermitRequestContext> options) : base(options)
        {

        }
        public DbSet<AdUser> AdUsers { get; set; }
        public DbSet<CumulativeLeaveRequest> CumulativeLeaveRequests { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }
        public DbSet<Notification> Notification { get; set; }

    }


}
