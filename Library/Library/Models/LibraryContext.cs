using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;
using System;

namespace Library.Models
{
    public class LibraryContext : IdentityDbContext<User>
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Status> Statuses { get; set; }

        internal class SeedGenres : IEntityTypeConfiguration<Genre>
        {
            public void Configure(EntityTypeBuilder<Genre> entity)
            {
                entity.HasData(
                new Genre { GenreId = "1", Name = "Fiction" },
                new Genre { GenreId = "2", Name = "Non-Fiction" },
                new Genre { GenreId = "3", Name = "Children" },
                new Genre { GenreId = "4", Name = "Other" }
                );
            }
        }
        internal class SeedStatuses : IEntityTypeConfiguration<Status>
        {
            public void Configure(EntityTypeBuilder<Status> entity)
            {
                entity.HasData(
                        new Status { StatusId = "available", Name = "Available" },
                        new Status { StatusId = "checked", Name = "Checked" },
                        new Status { StatusId = "returned", Name = "Returned" }
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed initial data
            modelBuilder.ApplyConfiguration(new SeedGenres());
            modelBuilder.ApplyConfiguration(new SeedStatuses());
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>(); 
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string username = "admin"; 
            string password = "Password123!"; 
            string roleName = "Admin";
            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username }; 
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
