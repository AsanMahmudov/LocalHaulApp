using Asan_CSharp_Web_Project.Data;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Asan_CSharp_Web_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            {
                var builder = WebApplication.CreateBuilder(args);

                string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

                // Configure your SINGLE DbContext for both application data and Identity
                builder.Services
                    .AddDbContext<LocalHaulDbContext>(options =>
                    {
                        options.UseSqlServer(connectionString);
                    });
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                // Configure ASP.NET Identity to use LocalHaulDbContext for its stores
                builder.Services
                    .AddDefaultIdentity<ApplicationUser>(options =>
                    {
                        options.SignIn.RequireConfirmedEmail = false;
                        options.SignIn.RequireConfirmedAccount = false;
                        options.SignIn.RequireConfirmedPhoneNumber = false;

                        options.Password.RequiredLength = 6;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredUniqueChars = 0;
                    })
                    .AddRoles<IdentityRole>() // Use your custom ApplicationRole here
                    .AddEntityFrameworkStores<LocalHaulDbContext>(); // Correct: Identity uses LocalHaulDbContext

                builder.Services.AddControllersWithViews();
                builder.Services.AddRazorPages();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                // Add Area mapping for MVC Areas
                app.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.MapRazorPages();

                // Database Migration and Seeding Block
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {

                        var context = services.GetRequiredService<LocalHaulDbContext>();
                        // Ensure database is migrated to the latest version on startup
                        context.Database.Migrate();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred during database migration/seeding: {ex.Message}");
                    }
                }

                app.Run();


            }
        }
}
