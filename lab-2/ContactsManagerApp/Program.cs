using Microsoft.EntityFrameworkCore;
using System;

namespace ContactsManagerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuring InMemory database for EF Core
            builder.Services.AddDbContext<AppDbContext>(options =>
                options
                    .UseInMemoryDatabase("ContactManagerInMemoryDb")
                    .EnableSensitiveDataLogging());

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Create the database (this ensures seed data is added)
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
