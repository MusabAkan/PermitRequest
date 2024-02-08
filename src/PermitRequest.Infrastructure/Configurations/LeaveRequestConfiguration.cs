using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.ValueObjets;

namespace PermitRequest.Infrastructure.Configurations
{
    internal class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasKey(e => e.Id);

            var withBetweenDates = builder.OwnsOne(e => e.BetweenDates);
            withBetweenDates.Property(e => e.StartDate).HasColumnName("StartDate");
            withBetweenDates.Property(e => e.EndDate).HasColumnName("EndDate");

            var withReasonExplanation = builder.OwnsOne(e => e.ReasonExplanation);
            withReasonExplanation.Property(e => e.Reason).HasColumnName("Reason");

            builder.Property(e => e.FormNumber)
             .ValueGeneratedOnAdd();

            builder.Property(lr => lr.RequestNumber)
           .HasComputedColumnSql("CONCAT('LRF-', FORMAT(FormNumber, 'D6'))");

        }
    }
}
