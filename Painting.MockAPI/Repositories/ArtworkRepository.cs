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
        context.Artworks.Add(createdArtwork);
        await context.SaveChangesAsync();

        return createdArtwork;
    }

    public async Task<Artwork?> UpdateById(int id, UpdateArtworkDto updatedArtwork)
    {
        var existingArtwork = await context.Artworks.FindAsync(id);

        if (existingArtwork is null) return null;

        context.Entry(existingArtwork).CurrentValues.SetValues(updatedArtwork.ToEntity(id));
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
