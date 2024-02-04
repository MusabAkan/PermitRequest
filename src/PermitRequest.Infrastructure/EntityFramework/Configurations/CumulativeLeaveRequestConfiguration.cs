using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Infrastructure.EntityFramework.Configurations
{
    internal class CumulativeLeaveRequestConfiguration : IEntityTypeConfiguration<CumulativeLeaveRequest>
    {
        public void Configure(EntityTypeBuilder<CumulativeLeaveRequest> builder)
        {
            builder.HasKey(e => e.Id);

        }
    }
}
