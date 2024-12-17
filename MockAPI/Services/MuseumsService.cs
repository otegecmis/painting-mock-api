using Microsoft.EntityFrameworkCore;
using MockAPI.Data;
using MockAPI.Entities;
using MockAPI.DTOs;
using MockAPI.Mapping;

namespace MockAPI.Services;

public class MuseumsService : IMuseumsService
{
    private readonly PaintingContext _context;

    public MuseumsService(PaintingContext context)
    {
        _context = context;
    }

    public async Task<List<MuseumDTO>> GetMuseums()
    {
        var museums = await _context.Museums.Include(museum => museum.Paintings)
                                            .ThenInclude(painting => painting.Artist)
                                            .AsNoTracking()
                                            .Select(museum => museum.ToMuseumDetailDTO())
                                            .ToListAsync();

        return museums;
    }

    public async Task<MuseumDTO?> GetMuseumById(int Id)
    {
        var museum = await _context.Museums.Include(museum => museum.Paintings)
                                           .ThenInclude(painting => painting.Artist)
                                           .FirstOrDefaultAsync(museum => museum.Id == Id);

        return museum?.ToMuseumDetailDTO();
    }

    public async Task<Museum> CreateMuseum(CreateMuseumDTO createdMuseum)
    {
        Museum museum = createdMuseum.ToEntity();
        _context.Museums.Add(museum);
        await _context.SaveChangesAsync();

        return museum;
    }

    public async Task<bool> UpdateMuseum(int Id, UpdateMuseumDTO updatedMuseum)
    {
        var existingMuseum = await _context.Museums.FindAsync(Id);

        if (existingMuseum is null)
        {
            return false;
        }

        _context.Entry(existingMuseum).CurrentValues.SetValues(updatedMuseum.ToEntity(Id));
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteMuseum(int Id)
    {
        var existingMuseum = await _context.Museums.FindAsync(Id);

        if (existingMuseum is null)
        {
            return false;
        }

        var countPaintings = _context.Paintings.Where(m => m.Museum.Id == Id).Count();

        if (countPaintings > 0)
        {
            throw new InvalidOperationException("Cannot delete museum.");
        }

        await _context.Museums.Where(museum => museum.Id == Id).ExecuteDeleteAsync();

        return true;
    }
}
