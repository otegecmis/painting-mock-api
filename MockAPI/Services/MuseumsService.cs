using MockAPI.Data;
using MockAPI.DTOs;
using MockAPI.Entities;
using Microsoft.EntityFrameworkCore;
using MockAPI.Mapping;

namespace MockAPI.Services;

public class MuseumsService : IMuseumsService
{
    private readonly PaintingContext _dbContext;

    public MuseumsService(PaintingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MuseumDTO>> GetAllMuseumsAsync()
    {
        var museums = await _dbContext.Museums.Include(m => m.Paintings).ThenInclude(p => p.Artist).AsNoTracking().Select(m => m.ToMuseumDetailDTO()).ToListAsync();

        return museums;
    }

    public async Task<MuseumDTO?> GetMuseumByIdAsync(int Id)
    {
        var museum = await _dbContext.Museums.Include(m => m.Paintings).ThenInclude(m => m.Artist).FirstOrDefaultAsync(m => m.Id == Id);

        return museum?.ToMuseumDetailDTO();
    }

    public async Task<Museum> CreateMuseumAsync(CreateMuseumDTO createdMuseum)
    {
        Museum museum = createdMuseum.ToEntity();
        _dbContext.Museums.Add(museum);

        await _dbContext.SaveChangesAsync();

        return museum;
    }

    public async Task<bool> UpdateMuseumAsync(int Id, UpdateMuseumDTO updatedMuseum)
    {
        var existingMuseum = await _dbContext.Museums.FindAsync(Id);

        if (existingMuseum is null)
        {
            return false;
        }

        _dbContext.Entry(existingMuseum).CurrentValues.SetValues(updatedMuseum.ToEntity(Id));
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteMuseumAsync(int Id)
    {
        var existingMuseum = await _dbContext.Museums.FindAsync(Id);

        if (existingMuseum is null)
        {
            return false;
        }

        var countPaintings = _dbContext.Paintings.Where(m => m.Museum.Id == Id).Count();

        if (countPaintings > 0)
        {
            throw new InvalidOperationException("Cannot delete museum with paintings.");
        }

        await _dbContext.Museums.Where(museum => museum.Id == Id).ExecuteDeleteAsync();

        return true;
    }
}
