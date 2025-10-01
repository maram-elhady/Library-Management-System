using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.ModelsConfig
{
    public class UserActivityLogConfiguration : IEntityTypeConfiguration<UserActivityLog>
    {
        public void Configure(EntityTypeBuilder<UserActivityLog> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Action).HasMaxLength(100).IsRequired();
            builder.Property(l => l.EntityType).HasMaxLength(100);
            builder.Property(l => l.EntityId).HasMaxLength(100);
        }
    }
}
