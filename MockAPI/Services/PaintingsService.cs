using Microsoft.EntityFrameworkCore;
using MockAPI.Data;
using MockAPI.Entities;
using MockAPI.DTOs;
using MockAPI.Mapping;

namespace MockAPI.Services;

public class PaintingsService : IPaintingsService
{
    private readonly PaintingContext _context;

    public PaintingsService(PaintingContext context)
    {
        _context = context;
    }

    public async Task<List<PaintingDTO>> GetPaintings()
    {
        var paintings = await _context.Paintings.Include(painting => painting.Artist)
                                                .Include(painting => painting.Museum)
                                                .AsNoTracking()
                                                .Select(painting => painting.ToPaintingDetailDTO())
                                                .ToListAsync();

        return paintings;
    }

    public async Task<PaintingDTO?> GetPaintingById(int id)
    {
        var painting = await _context.Paintings.Include(painting => painting.Artist)
                                               .Include(painting => painting.Museum)
                                               .FirstOrDefaultAsync(painting => painting.Id == id);

        return painting?.ToPaintingDetailDTO();
    }

    public async Task<Painting> CreatePainting(CreatePaintingDTO createdPainting)
    {
        Painting painting = createdPainting.ToEntity();
        _context.Paintings.Add(painting);
        await _context.SaveChangesAsync();

        return painting;
    }

    public async Task<bool> UpdatePainting(int Id, UpdatePaintingDTO updatedPainting)
    {
        var existingPainting = await _context.Paintings.FindAsync(Id);

        if (existingPainting is null)
        {
            return false;
        }

        _context.Entry(existingPainting).CurrentValues.SetValues(updatedPainting.ToEntity(Id));
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePainting(int Id)
    {
        var painting = await _context.Paintings.FindAsync(Id);

        if (painting is null)
        {
            return false;
        }

        _context.Paintings.Remove(painting);
        await _context.SaveChangesAsync();

        return true;
    }
}
