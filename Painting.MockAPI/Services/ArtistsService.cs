using Microsoft.EntityFrameworkCore;
using Painting.MockAPI.Mapping;
using Painting.MockAPI.Data;
using Painting.MockAPI.Dtos;
using Painting.MockAPI.Entities;

namespace Painting.MockAPI.Services;

public class ArtistsService : IArtistsService
{
    private readonly ApplicationDbContext _context;

    public ArtistsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ArtistDto>> GetArtists()
    {
        var artists = await _context.Artists.Include(artist => artist.Paintings)
            .ThenInclude(painting => painting.Museum)
            .AsNoTracking()
            .Select(artist => artist.ToArtistDetailDto())
            .ToListAsync();

        return artists;
    }

    public async Task<ArtistDto?> GetArtistById(int id)
    {
        var artist = await _context.Artists.Include(artist => artist.Paintings)
            .ThenInclude(painting => painting.Museum)
            .FirstOrDefaultAsync(artist => artist.Id == id);

        return artist?.ToArtistDetailDto();
    }

    public async Task<Artist> CreateArtist(CreateArtistDto createdArtist)
    {
        var artist = createdArtist.ToEntity();
        _context.Artists.Add(artist);
        await _context.SaveChangesAsync();

        return artist;
    }

    public async Task<bool> UpdateArtistById(int id, UpdateArtistDto updatedArtist)
    {
        var existingArtist = await _context.Artists.FindAsync(id);

        if (existingArtist is null)
        {
            return false;
        }

        _context.Entry(existingArtist).CurrentValues.SetValues(updatedArtist.ToEntity(id));
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteArtistById(int id)
    {
        var existingArtist = await _context.Artists.FindAsync(id);

        if (existingArtist is null)
        {
            return false;
        }

        var countPaintings = await _context.Paintings.Where(painting => painting.Artist.Id == id).CountAsync();

        if (countPaintings > 0)
        {
            throw new InvalidOperationException("Cannot delete artist.");
        }

        _context.Artists.Remove(existingArtist);
        await _context.SaveChangesAsync();

        return true;
    }
}