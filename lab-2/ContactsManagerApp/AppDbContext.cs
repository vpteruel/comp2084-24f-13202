using ContactsManagerApp.DbSetConfig;
using ContactsManagerApp.Entities;
using ContactsManagerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsManagerApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfiguration(new CategoryConfiguration())
                .ApplyConfiguration(new ContactConfiguration());
        }
    }
}

