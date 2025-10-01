using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.ModelsConfig
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FullName).HasMaxLength(200);

            builder.HasMany(u => u.ActivityLogs)
                   .WithOne(l => l.User)
                   .HasForeignKey(l => l.UserId);

            builder.HasMany(u => u.BorrowTransactions)
                   .WithOne(t => t.User)
                   .HasForeignKey(t => t.UserId);
        }
    }
}
