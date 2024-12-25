using Microsoft.EntityFrameworkCore;
using Painting.MockAPI.Entities;

namespace Painting.MockAPI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Entities.Painting> Paintings => Set<Entities.Painting>();
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

        modelBuilder.Entity<Entities.Painting>().HasData(
            new Entities.Painting { Id = 1, Name = "Mona Lisa", ArtistId = 1, MuseumId = 1 },
            new Entities.Painting { Id = 2, Name = "Starry Night", ArtistId = 2, MuseumId = 2 }
        );
    }
}
