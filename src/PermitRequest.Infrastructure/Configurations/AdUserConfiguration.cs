using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Infrastructure.Configurations
{
    public class AdUserConfiguration : IEntityTypeConfiguration<AdUser>
    {
        public void Configure(EntityTypeBuilder<AdUser> builder)
        {
            builder.HasKey(e => e.Id);        

            var withEntity = builder.OwnsOne(e => e.FullName);

            withEntity.Property(e => e.FirstName).HasColumnName("FirstName");
            withEntity.Property(e => e.LastName).HasColumnName("LastName");

            builder.HasMany(c => c.LeaveRequests)
                .WithOne(c => c.CreatedBy)
                .HasForeignKey(c => c.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.Notifications)
              .WithOne(c => c.User)
              .HasForeignKey(c => c.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.CumulativeLeaveRequests)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
