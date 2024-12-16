using Microsoft.EntityFrameworkCore;
using MockAPI.Data;
using MockAPI.DTOs;
using MockAPI.Entities;
using MockAPI.Mapping;

namespace MockAPI.Services;

public class PaintingService : IPaintingService
{
    private readonly PaintingContext _dbContext;

    public PaintingService(PaintingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PaintingDTO>> GetAllPaintingsAsync()
    {
        var paintings = await _dbContext.Paintings.Include(p => p.Artist).Include(p => p.Museum).AsNoTracking().Select(p => p.ToPaintingDetailDTO()).ToListAsync();

        return paintings;
    }

    public async Task<PaintingDTO?> GetPaintingByIdAsync(int id)
    {
        var painting = await _dbContext.Paintings.Include(p => p.Artist).Include(p => p.Museum).FirstOrDefaultAsync(p => p.Id == id);

        return painting?.ToPaintingDetailDTO();
    }

    public async Task<Painting> CreatePaintingAsync(CreatePaintingDTO createPainting)
    {
        Painting painting = createPainting.ToEntity();
        _dbContext.Paintings.Add(painting);

        await _dbContext.SaveChangesAsync();

        return painting;
    }

    public async Task<bool> UpdatePaintingAsync(int Id, UpdatePaintingDTO updatedPainting)
    {
        var existingPainting = await _dbContext.Paintings.FindAsync(Id);

        if (existingPainting is null)
        {
            return false;
        }

        _dbContext.Entry(existingPainting).CurrentValues.SetValues(updatedPainting.ToEntity(Id));
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePaintingAsync(int Id)
    {
        var existingPainting = await _dbContext.Paintings.FindAsync(Id);

        if (existingPainting is null)
        {
            return false;
        }

        _dbContext.Paintings.Remove(existingPainting);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
