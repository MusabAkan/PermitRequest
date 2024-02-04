using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Infrastructure.EntityFramework.Configurations
{
    public class CumulativeLeaveRequestConfiguration : IEntityTypeConfiguration<CumulativeLeaveRequest>
    {
        public void Configure(EntityTypeBuilder<CumulativeLeaveRequest> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(c => c.Notifications)
               .WithOne(c => c.CumulativeLeaveRequest)
               .HasForeignKey(c => c.CumulativeLeaveRequestId)
               .OnDelete(DeleteBehavior.Cascade);
               

        }
    }
}
