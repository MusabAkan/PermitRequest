using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Infrastructure.Configurations
{
    internal class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasKey(e => e.Id);

            builder.OwnsOne(e => e.BetweenDates);



            builder.Property(e => e.BetweenDates.StartDate).HasColumnName("StartDate");
            builder.Property(e => e.BetweenDates.EndDate).HasColumnName("EndDate");

            //builder.Ignore(e => e.BetweenDates.TotalWorkHours);
            //builder.Ignore(e => e.BetweenDates.Year);
            ////builder.Ignore(e => e.CreatedAtStr);
            // builder.Ignore(e => e.BetweenDates.StartDateStr);
            //builder.Ignore(e => e.BetweenDates.EndDateStr);

            builder.Property(e => e.FormNumber)
             .ValueGeneratedOnAdd();

            builder.Property(lr => lr.RequestNumber)
           .HasComputedColumnSql("CONCAT('LRF-', FORMAT(FormNumber, 'D6'))");

        }
    }
}
