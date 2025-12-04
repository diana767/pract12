using Microsoft.EntityFrameworkCore;
using System;

namespace pract12.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Pract12;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Student>()
                .HasOne(s => s.UserProfile)
                .WithOne(up => up.Student)
                .HasForeignKey<UserProfile>(up => up.StudentId)
                .IsRequired(false)  
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserProfiles)
                .WithOne(up => up.Role)
                .HasForeignKey(up => up.RoleId)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Title = "Пользователь" },
                new Role { Id = 2, Title = "Менеджер" },
                new Role { Id = 3, Title = "Администратор" }
            );

            modelBuilder.Entity<UserProfile>()
                .Property(u => u.RoleId)
                .HasDefaultValue(1);

            modelBuilder.Entity<UserProfile>()
                .Property(u => u.Birthday)
                .IsRequired(false);
        }
    }
}