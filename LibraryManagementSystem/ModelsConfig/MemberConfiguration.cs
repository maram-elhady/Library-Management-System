using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.ModelsConfig
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(m => m.MemberId);
            builder.Property(m => m.FullName).HasMaxLength(200).IsRequired();
            builder.Property(m => m.Email).HasMaxLength(200);
            builder.HasIndex(m => m.Email).IsUnique();
            builder.Property(m => m.Phone).HasMaxLength(50);
            builder.Property(m => m.Address).HasMaxLength(500);
        }
    }
}
