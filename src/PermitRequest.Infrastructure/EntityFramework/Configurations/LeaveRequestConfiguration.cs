using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Infrastructure.EntityFramework.Configurations
{
    internal class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasKey(e => e.Id);

        }
    }
