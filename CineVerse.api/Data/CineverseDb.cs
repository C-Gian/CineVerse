using Microsoft.EntityFrameworkCore;
using CineVerse.api.Models; 

namespace CineVerse.api.Data;

public class CineverseDb : DbContext
{
    public CineverseDb(DbContextOptions<CineverseDb> opts) : base(opts) { }

    public DbSet<GenreEntity> Genres => Set<GenreEntity>();

    #region ModelCreating

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        mb.Entity<GenreEntity>().HasData(
            new GenreEntity { Id = 28, Name = "Action" },
            new GenreEntity { Id = 12, Name = "Adventure" },
            new GenreEntity { Id = 16, Name = "Animation" },
            new GenreEntity { Id = 35, Name = "Comedy" },
            new GenreEntity { Id = 80, Name = "Crime" },
            new GenreEntity { Id = 99, Name = "Documentary" },
            new GenreEntity { Id = 18, Name = "Drama" },
            new GenreEntity { Id = 10751, Name = "Family" },
            new GenreEntity { Id = 14, Name = "Fantasy" },
            new GenreEntity { Id = 36, Name = "History" },
            new GenreEntity { Id = 27, Name = "Horror" },
            new GenreEntity { Id = 10402, Name = "Music" },
            new GenreEntity { Id = 9648, Name = "Mystery" },
            new GenreEntity { Id = 10749, Name = "Romance" },
            new GenreEntity { Id = 878, Name = "Science Fiction" },
            new GenreEntity { Id = 10770, Name = "TV Movie" },
            new GenreEntity { Id = 53, Name = "Thriller" },
            new GenreEntity { Id = 10752, Name = "War" },
            new GenreEntity { Id = 37, Name = "Western" }
        );
    }

    #endregion
}
