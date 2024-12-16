using MockAPI.DTOs;
using MockAPI.Entities;

namespace MockAPI.Services;

public interface IPaintingService
{
    Task<List<PaintingDTO>> GetAllPaintingsAsync();
    Task<PaintingDTO?> GetPaintingByIdAsync(int id);
    Task<Painting> CreatePaintingAsync(CreatePaintingDTO createPainting);
    Task<bool> UpdatePaintingAsync(int Id, UpdatePaintingDTO updatedPainting);
    Task<bool> DeletePaintingAsync(int Id);
}
