using Microsoft.EntityFrameworkCore;
using MockAPI.Data;
using MockAPI.Entities;
using MockAPI.Dtos;
using MockAPI.Mapping;

namespace MockAPI.Services;

public class PaintingsService : IPaintingsService
{
    private readonly PaintingContext _context;

    public PaintingsService(PaintingContext context)
    {
        _context = context;
    }

    public async Task<List<PaintingDto>> GetPaintings()
    {
        var paintings = await _context.Paintings.Include(painting => painting.Artist)
            .Include(painting => painting.Museum)
            .AsNoTracking()
            .Select(painting => painting.ToPaintingDetailDto())
            .ToListAsync();

        return paintings;
    }

    public async Task<PaintingDto?> GetPaintingById(int id)
    {
        var painting = await _context.Paintings.Include(painting => painting.Artist)
            .Include(painting => painting.Museum)
            .FirstOrDefaultAsync(painting => painting.Id == id);

        return painting?.ToPaintingDetailDto();
    }

    public async Task<Painting> CreatePainting(CreatePaintingDto createdPainting)
    {
        var painting = createdPainting.ToEntity();
        _context.Paintings.Add(painting);
        await _context.SaveChangesAsync();

        return painting;
    }

    public async Task<bool> UpdatePaintingById(int id, UpdatePaintingDto updatedPainting)
    {
        var existingPainting = await _context.Paintings.FindAsync(id);

        if (existingPainting is null)
        {
            return false;
        }

        _context.Entry(existingPainting).CurrentValues.SetValues(updatedPainting.ToEntity(id));
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePaintingById(int id)
    {
        var painting = await _context.Paintings.FindAsync(id);

        if (painting is null)
        {
            return false;
        }

        _context.Paintings.Remove(painting);
        await _context.SaveChangesAsync();

        return true;
    }
}
