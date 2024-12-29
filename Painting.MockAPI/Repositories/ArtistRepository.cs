using Microsoft.EntityFrameworkCore;
using Painting.MockAPI.Data;
using Painting.MockAPI.Dtos.Artist;
using Painting.MockAPI.Entities;
using Painting.MockAPI.Interfaces;
using Painting.MockAPI.Mapping;

namespace Painting.MockAPI.Repositories;

public class ArtistRepository(ApplicationDbContext context) : IArtistRepository
{
    public async Task<List<ArtistDto>> GetAll()
    {
        return await context.Artists
            .Include(artist => artist.Artworks)
            .ThenInclude(painting => painting.Museum)
            .AsNoTracking()
            .Select(artist => artist.ToArtistDetailDto())
            .ToListAsync();
    }

    public async Task<Artist?> GetById(int id)
    {
        return await context.Artists.FindAsync(id);
    }

    public async Task<Artist> Create(Artist createdArtist)
    {
        await context.Artists.AddAsync(createdArtist);
        await context.SaveChangesAsync();

        return createdArtist;
    }

    public async Task<Artist?> UpdateById(int id, UpdateArtistDto updatedArtist)
    {
        var existingArtist = await context.Artists.FindAsync(id);

        if (existingArtist == null) return null;

        existingArtist.Name = updatedArtist.Name;
        await context.SaveChangesAsync();

        return existingArtist;
    }

    public async Task<Artist?> DeleteById(int id)
    {
        var existingArtist = await context.Artists.FindAsync(id);

        if (existingArtist == null) return null;

        context.Artists.Remove(existingArtist);
        await context.SaveChangesAsync();

        return existingArtist;
    }
}
