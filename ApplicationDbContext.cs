using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using project4.Models;

namespace project4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                // Hashed password
                entity.Property(u => u.PasswordHash)
                      .IsRequired();

                // User status
                entity.Property(u => u.Status)
                      .IsRequired()
                      .HasConversion<string>(); // Convert enum to string for storage
            });
        }
    }
}




