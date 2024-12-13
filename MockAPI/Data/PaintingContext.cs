using Microsoft.EntityFrameworkCore;
using MockAPI.Entities;

namespace MockAPI.Data;

public class PaintingContext(DbContextOptions<PaintingContext> options) : DbContext(options)
{
    public DbSet<Painting> Paintings => Set<Painting>();
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Museum> Museums => Set<Museum>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>().HasData(
            new Artist { Id = 1, Name = "Leonardo da Vinci" },
            new Artist { Id = 2, Name = "Vincent van Gogh" }
        );

        modelBuilder.Entity<Museum>().HasData(
            new Museum { Id = 1, Name = "Louvre" },
            new Museum { Id = 2, Name = "Van Gogh Museum" }
        );

        modelBuilder.Entity<Painting>().HasData(
            new Painting { Id = 1, Name = "Mona Lisa", ArtistId = 1, MuseumId = 1 },
            new Painting { Id = 2, Name = "Starry Night", ArtistId = 2, MuseumId = 2 }
        );
    }
}
