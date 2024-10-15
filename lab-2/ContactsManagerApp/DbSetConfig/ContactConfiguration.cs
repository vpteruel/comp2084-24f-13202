using ContactsManagerApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsManagerApp.DbSetConfig
{
    public class ContactConfiguration : IEntityTypeConfiguration<ContactEntity>
    {
        public void Configure(EntityTypeBuilder<ContactEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            // define relationship between ContactEntity and CategoryEntity (foreign key)
            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // seed data
            var contacts = new[]
            {
                new ContactEntity { Id = 1, FirstName = "Daniela", LastName = "Grippo Oliveira", Phone = "123-123-1233", Email = "daniela@fakemail.com", CategoryId = 1 },
                new ContactEntity { Id = 2, FirstName = "Pamela", LastName = "Alves Musialak", Phone = "456-456-4566", Email = "pamela@fakemail.com", CategoryId = 2 },
                new ContactEntity { Id = 3, FirstName = "Vinicius", LastName = "Picossi Teruel", Phone = "789-789-7899", Email = "vinicius@fakemail.com", CategoryId = 3 }
            };

            builder.HasData(contacts);
        }
    }
}
