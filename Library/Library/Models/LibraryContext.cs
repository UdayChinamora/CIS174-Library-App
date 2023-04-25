using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Models
{
    public class LibraryContext : DbContext
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
    }
}
