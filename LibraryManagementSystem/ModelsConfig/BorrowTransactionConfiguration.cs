using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.ModelsConfig
{
    public class BorrowTransactionConfiguration : IEntityTypeConfiguration<BorrowTransaction>
    {
        public void Configure(EntityTypeBuilder<BorrowTransaction> builder)
        {
            builder.HasKey(bt => bt.BorrowTransactionId);
            builder.Property(bt => bt.Status).HasMaxLength(20).HasDefaultValue("Borrowed");
        }
    }
}
