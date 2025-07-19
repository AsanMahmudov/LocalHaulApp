using Data.Configurations;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data
{
    public class LocalHaulDbContext : IdentityDbContext<A>
    {
        protected LocalHaulDbContext(DbContextOptions<LocalHaulDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

            // --- Fixed GUIDs for Roles ---
            // Using fixed GUIDs allows us to reference them when seeding users and their roles.
            // These IDs must be unique and consistent across migrations.
            string adminRoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210"; // Example fixed GUID for Admin Role
            string userRoleId = "2c5e174e-3b0e-446f-86af-483d56fd7211";  // Example fixed GUID for User Role

            // Seed Identity Roles using HasData
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            // --- Seed Users and assign them to Roles using HasData ---

            // Create a password hasher instance
            var hasher = new PasswordHasher<ApplicationUser>();

            // Admin User
            string adminUserId = "8e445865-a24d-4543-a6c6-9443d048cdb0"; // Example fixed GUID for Admin User
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@olxclone.com",
                NormalizedUserName = "ADMIN@OLXCLONE.COM",
                Email = "admin@olxclone.com",
                NormalizedEmail = "ADMIN@OLXCLONE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "AdminPass123!"), // Hash the password
                SecurityStamp = Guid.NewGuid().ToString(), // Generate a new security stamp
                ConcurrencyStamp = Guid.NewGuid().ToString(), // Generate a new concurrency stamp
                FirstName = "System",
                LastName = "Administrator",
                City = "Sofia"
            };

            // Regular User
            string regularUserId = "9e224968-3b4f-4f80-9b6e-4493d048cdb0"; // Example fixed GUID for Regular User
            var regularUser = new ApplicationUser
            {
                Id = regularUserId,
                UserName = "user@olxclone.com",
                NormalizedUserName = "USER@OLXCLONE.COM",
                Email = "user@olxclone.com",
                NormalizedEmail = "USER@OLXCLONE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "UserPass123!"), // Hash the password
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                FirstName = "Regular",
                LastName = "User",
                City = "Plovdiv"
            };

            // Add users to the database
            builder.Entity<ApplicationUser>().HasData(adminUser, regularUser);

            // Link users to roles in AspNetUserRoles table
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = regularUserId,
                    RoleId = userRoleId
                }
            );

            // Optional: Seed Categories (if you want them seeded via HasData as well)
            // if you want them seeded via HasData as well, add them here.
            // Otherwise, you might still use a separate SeedData.cs for categories
            // if they are more dynamic or complex.
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Vehicles" },
                new Category { Id = 3, Name = "Real Estate" },
                new Category { Id = 4, Name = "Home & Garden" },
                new Category { Id = 5, Name = "Fashion" },
                new Category { Id = 6, Name = "Sports & Hobbies" },
                new Category { Id = 7, Name = "Services" }
            );


        }
    }
}
