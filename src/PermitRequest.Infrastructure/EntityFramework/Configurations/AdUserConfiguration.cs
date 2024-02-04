using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermitRequest.Domain.Entities;

namespace PermitRequest.Infrastructure.EntityFramework.Configurations
{
    public class AdUserConfiguration : IEntityTypeConfiguration<AdUser>
    {
        public void Configure(EntityTypeBuilder<AdUser> builder)
        {
            builder.HasKey(e => e.Id);

        }
    }
}
