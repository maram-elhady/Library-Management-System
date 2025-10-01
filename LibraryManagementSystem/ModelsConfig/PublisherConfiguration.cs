using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.ModelsConfig
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(p => p.PublisherId);
            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
        }
    }
}
