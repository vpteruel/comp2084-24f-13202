using ContactsManagerApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsManagerApp.DbSetConfig
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.ModifiedAt)
                .IsRequired();

            // seed data
            var categories = new[]
            {
                new CategoryEntity { Id = 1, Name = "Friend" },
                new CategoryEntity { Id = 2, Name = "Work" },
                new CategoryEntity { Id = 3, Name = "Family" }
            };

            builder.HasData(categories);
        }
    }

}
