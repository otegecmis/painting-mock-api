using MockAPI.Entities;
using MockAPI.DTOs;

namespace MockAPI.Services;

public interface IPaintingsService
{
    Task<List<PaintingDTO>> GetPaintings();
    Task<PaintingDTO?> GetPaintingById(int id);
    Task<Painting> CreatePainting(CreatePaintingDTO createdPainting);
    Task<bool> UpdatePainting(int Id, UpdatePaintingDTO updatedPainting);
    Task<bool> DeletePainting(int Id);
}
