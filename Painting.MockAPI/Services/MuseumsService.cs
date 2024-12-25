using Microsoft.EntityFrameworkCore;
using Painting.MockAPI.Mapping;
using Painting.MockAPI.Data;
using Painting.MockAPI.Dtos;
using Painting.MockAPI.Entities;

namespace Painting.MockAPI.Services;

public class MuseumsService : IMuseumsService
{
    private readonly ApplicationDbContext _context;

    public MuseumsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MuseumDto>> GetMuseums()
    {
        var museums = await _context.Museums.Include(museum => museum.Paintings)
            .ThenInclude(painting => painting.Artist)
            .AsNoTracking()
            .Select(museum => museum.ToMuseumDetailDto())
            .ToListAsync();

        return museums;
    }

    public async Task<MuseumDto?> GetMuseumById(int id)
    {
        var museum = await _context.Museums.Include(museum => museum.Paintings)
            .ThenInclude(painting => painting.Artist)
            .FirstOrDefaultAsync(museum => museum.Id == id);

        return museum?.ToMuseumDetailDto();
    }

    public async Task<Museum> CreateMuseum(CreateMuseumDto createdMuseum)
    {
        var museum = createdMuseum.ToEntity();
        _context.Museums.Add(museum);
        await _context.SaveChangesAsync();

        return museum;
    }

    public async Task<bool> UpdateMuseumById(int id, UpdateMuseumDto updatedMuseum)
    {
        var existingMuseum = await _context.Museums.FindAsync(id);

        if (existingMuseum is null)
        {
            return false;
        }

        _context.Entry(existingMuseum).CurrentValues.SetValues(updatedMuseum.ToEntity(id));
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteMuseumById(int id)
    {
        var existingMuseum = await _context.Museums.FindAsync(id);

        if (existingMuseum is null)
        {
            return false;
        }

        var countPaintings = _context.Paintings.Count(m => m.Museum.Id == id);

        if (countPaintings > 0)
        {
            throw new InvalidOperationException("Cannot delete museum.");
        }

        await _context.Museums.Where(museum => museum.Id == id).ExecuteDeleteAsync();

        return true;
    }
}