using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.ModelsConfig
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookId);
            builder.Property(b => b.Title).HasMaxLength(300).IsRequired();
            builder.Property(b => b.ISBN).HasMaxLength(20);
            builder.Property(b => b.Edition).HasMaxLength(50);
            builder.Property(b => b.Language).HasMaxLength(50);
            builder.Property(b => b.CoverImagePath).HasMaxLength(500);
            builder.Property(b => b.Status).HasMaxLength(20).HasDefaultValue("Available");

            builder.HasOne(b => b.Publisher)
                   .WithMany(p => p.Books)
                   .HasForeignKey(b => b.PublisherId);
        }
    }
}
