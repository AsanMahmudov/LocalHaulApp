using Data.Configurations;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data
{
    public class LocalHaulDbContext : IdentityDbContext<ApplicationUser>
    {
        public LocalHaulDbContext(DbContextOptions<LocalHaulDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Apply all configurations from the Configurations folder
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new MessageConfiguration());


            string adminRoleId = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            string userRoleId = "c08d2460-593b-453b-8219-00bd9344e575";

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

            // 2. Prepare Password Hasher
            var hasher = new PasswordHasher<ApplicationUser>();

            // 3. Define Admin User
            string adminUserId = "8e445865-a24d-4543-a6c6-9443d048cdb0";
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@localHaul.com",
                NormalizedUserName = "ADMIN@LOCALHAUL.COM",
                Email = "admin@localHaul.com",
                NormalizedEmail = "ADMIN@LOCALHAUL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "AdminPass123!"); // Password hash generation

            // 4. Define Regular User
            string regularUserId = "9e224968-3b4f-4f80-9b6e-4493d048cdb0";
            var regularUser = new ApplicationUser
            {
                Id = regularUserId,
                UserName = "user@localHaul.com",
                NormalizedUserName = "USER@LOCALHAUL.COM",
                Email = "user@localHaul.com",
                NormalizedEmail = "USER@LOCALHAUL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            regularUser.PasswordHash = hasher.HashPassword(regularUser, "UserPass123!"); // Password hash generation

            // 5. Seed Users
            builder.Entity<ApplicationUser>().HasData(adminUser, regularUser);

            // 6. Assign Users to Roles
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>().HasData(
                new Microsoft.AspNetCore.Identity.IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new Microsoft.AspNetCore.Identity.IdentityUserRole<string>
                {
                    UserId = regularUserId,
                    RoleId = userRoleId
                }
            );



            builder.Entity<Category>().HasData(
                new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Electronics" },
                new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Vehicles" },
                new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Real Estate" },
                new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Home & Garden" },
                new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Fashion" },
                new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "Sports & Hobbies" },
                new Category { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "Services" }
            );


        }
    }
}
