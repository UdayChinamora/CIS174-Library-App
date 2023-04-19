using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = "1", Name = "Fiction" },
                new Genre { GenreId = "2", Name = "Non-Fiction" },
                new Genre { GenreId = "3", Name = "Children" },
                new Genre { GenreId = "4", Name = "Other" }
               
            );
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "available", Name = "Available" },
                new Status { StatusId = "checked", Name = "Checked" },
                 new Status { StatusId = "returned", Name = "Returned" }
                

            );
        }
    }
}
