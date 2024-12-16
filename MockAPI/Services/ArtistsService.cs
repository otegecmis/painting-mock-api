using MockAPI.Data;
using MockAPI.DTOs;
using MockAPI.Mapping;
using Microsoft.EntityFrameworkCore;
using MockAPI.Entities;

namespace MockAPI.Services;

public class ArtistsService : IArtistsService
{
    private readonly PaintingContext _dbContext;

    public ArtistsService(PaintingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ArtistDTO>> GetAllArtistsAsync()
    {
        var artists = await _dbContext.Artists.Include(m => m.Paintings).ThenInclude(p => p.Museum).AsNoTracking().Select(a => a.ToArtistDetailDTO()).ToListAsync();

        return artists;
    }

    public async Task<ArtistDTO?> GetArtistByIdAsync(int Id)
    {
        var artist = await _dbContext.Artists.Include(a => a.Paintings).ThenInclude(p => p.Museum).FirstOrDefaultAsync(a => a.Id == Id);

        return artist?.ToArtistDetailDTO();
    }

    public async Task<Artist> CreateArtistAsync(CreateArtistDTO createdArtist)
    {
        Artist artist = createdArtist.ToEntity();
        _dbContext.Artists.Add(artist);

        await _dbContext.SaveChangesAsync();

        return artist;
    }

    public async Task<bool> UpdateArtistAsync(int Id, UpdateArtistDTO updatedArtist)
    {
        var existingArtist = await _dbContext.Artists.FindAsync(Id);

        if (existingArtist is null)
        {
            return false;
        }

        _dbContext.Entry(existingArtist).CurrentValues.SetValues(updatedArtist.ToEntity(Id));
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteArtistAsync(int Id)
    {
        var existingArtist = await _dbContext.Artists.FindAsync(Id);

        if (existingArtist is null)
        {
            return false;
        }

        var countPaintings = await _dbContext.Paintings.Where(p => p.Artist.Id == Id).CountAsync();

        if (countPaintings > 0)
        {
            throw new InvalidOperationException("Cannot delete artist with paintings.");
        }

        _dbContext.Artists.Remove(existingArtist);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
