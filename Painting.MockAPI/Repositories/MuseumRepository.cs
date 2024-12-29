using Microsoft.EntityFrameworkCore;
using Painting.MockAPI.Data;
using Painting.MockAPI.Dtos.Museum;
using Painting.MockAPI.Entities;
using Painting.MockAPI.Interfaces;
using Painting.MockAPI.Mapping;

namespace Painting.MockAPI.Repositories;

public class MuseumRepository(ApplicationDbContext context) : IMuseumRepository
{
    public async Task<List<MuseumDto>> GetAll()
    {
        return await context.Museums
            .Include(museum => museum.Artworks)
            .ThenInclude(artwork => artwork.Artist)
            .AsNoTracking()
            .Select(museum => museum.ToMuseumDetailDto())
            .ToListAsync();
    }

    public async Task<Museum?> GetById(int id)
    {
        return await context.Museums
            .Include(museum => museum.Artworks)
            .ThenInclude(artwork => artwork.Artist)
            .FirstOrDefaultAsync(museum => museum.Id == id);
    }

    public async Task<Museum> Create(Museum createdMuseum)
    {
        await context.Museums.AddAsync(createdMuseum);
        await context.SaveChangesAsync();

        return createdMuseum;
    }

    public async Task<Museum?> UpdateById(int id, UpdateMuseumDto updatedMuseum)
    {
        var existingMuseum = await context.Museums.FindAsync(id);

        if (existingMuseum == null) return null;

        existingMuseum.Name = updatedMuseum.Name;
        await context.SaveChangesAsync();

        return existingMuseum;
    }

    public async Task<Museum?> DeleteById(int id)
    {
        var existingMuseum = await context.Museums.FindAsync(id);

        if (existingMuseum == null) return null;

        context.Museums.Remove(existingMuseum);
        await context.SaveChangesAsync();

        return existingMuseum;
    }
}
