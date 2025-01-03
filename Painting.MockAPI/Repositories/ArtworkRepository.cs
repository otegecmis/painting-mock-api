using Microsoft.EntityFrameworkCore;
using Painting.MockAPI.Data;
using Painting.MockAPI.Dtos.Artwork;
using Painting.MockAPI.Entities;
using Painting.MockAPI.Interfaces;
using Painting.MockAPI.Mapping;

namespace Painting.MockAPI.Repositories;

public class ArtworkRepository(ApplicationDbContext context) : IArtworkRepository
{
    public async Task<List<ArtworkDto>> GetAll()
    {
        return await context.Artworks
            .Include(artwork => artwork.Artist)
            .Include(artwork => artwork.Museum)
            .AsNoTracking()
            .Select(artwork => artwork.ToArtworkDetailDto())
            .ToListAsync();
    }

    public async Task<Artwork?> GetById(int id)
    {
        return await context.Artworks.Include(artwork => artwork.Artist)
            .Include(artwork => artwork.Museum)
            .FirstOrDefaultAsync(artwork => artwork.Id == id);
    }

    public async Task<Artwork> Create(Artwork createdArtwork)
    {
        var artist = await context.Artists.FindAsync(createdArtwork.ArtistId);

        if (artist is null) throw new InvalidOperationException("Artist not found.");

        var museum = await context.Museums.FindAsync(createdArtwork.MuseumId);

        if (museum is null) throw new InvalidOperationException("Museum not found.");

        context.Artworks.Add(createdArtwork);
        await context.SaveChangesAsync();

        return await context.Artworks
            .Include(artwork => artwork.Artist)
            .Include(artwork => artwork.Museum)
            .FirstAsync(artwork => artwork.Id == createdArtwork.Id);
    }

    public async Task<Artwork?> UpdateById(int id, Artwork updatedArtwork)
    {
        var existingArtwork = await context.Artworks
            .Include(artwork => artwork.Artist)
            .Include(artwork => artwork.Museum)
            .FirstOrDefaultAsync(artwork => artwork.Id == id);

        if (existingArtwork is null) return null;

        existingArtwork.Name = updatedArtwork.Name;
        existingArtwork.ArtistId = updatedArtwork.ArtistId;
        existingArtwork.MuseumId = updatedArtwork.MuseumId;

        await context.SaveChangesAsync();

        return existingArtwork;
    }

    public async Task<Artwork?> DeleteById(int id)
    {
        var existingArtwork = await context.Artworks.FindAsync(id);

        if (existingArtwork is null) return null;

        context.Artworks.Remove(existingArtwork);
        await context.SaveChangesAsync();

        return existingArtwork;
    }
}
