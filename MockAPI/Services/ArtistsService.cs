using Microsoft.EntityFrameworkCore;
using MockAPI.Data;
using MockAPI.Entities;
using MockAPI.DTOs;
using MockAPI.Mapping;

namespace MockAPI.Services;

public class ArtistsService : IArtistsService
{
    private readonly PaintingContext _context;

    public ArtistsService(PaintingContext context)
    {
        _context = context;
    }

    public async Task<List<ArtistDTO>> GetArtists()
    {
        var artists = await _context.Artists.Include(artist => artist.Paintings)
                                            .ThenInclude(painting => painting.Museum)
                                            .AsNoTracking()
                                            .Select(artist => artist.ToArtistDetailDTO())
                                            .ToListAsync();

        return artists;
    }

    public async Task<ArtistDTO?> GetArtistById(int Id)
    {
        var artist = await _context.Artists.Include(artist => artist.Paintings)
                                           .ThenInclude(painting => painting.Museum)
                                           .FirstOrDefaultAsync(artist => artist.Id == Id);

        return artist?.ToArtistDetailDTO();
    }

    public async Task<Artist> CreateArtist(CreateArtistDTO createdArtist)
    {
        Artist artist = createdArtist.ToEntity();
        _context.Artists.Add(artist);
        await _context.SaveChangesAsync();

        return artist;
    }

    public async Task<bool> UpdateArtist(int Id, UpdateArtistDTO updatedArtist)
    {
        var existingArtist = await _context.Artists.FindAsync(Id);

        if (existingArtist is null)
        {
            return false;
        }

        _context.Entry(existingArtist).CurrentValues.SetValues(updatedArtist.ToEntity(Id));
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteArtist(int Id)
    {
        var existingArtist = await _context.Artists.FindAsync(Id);

        if (existingArtist is null)
        {
            return false;
        }

        var countPaintings = await _context.Paintings.Where(painting => painting.Artist.Id == Id).CountAsync();

        if (countPaintings > 0)
        {
            throw new InvalidOperationException("Cannot delete artist.");
        }

        _context.Artists.Remove(existingArtist);
        await _context.SaveChangesAsync();

        return true;
    }
}
