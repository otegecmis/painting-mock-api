using MockAPI.Entities;
using MockAPI.Dtos;

namespace MockAPI.Services;

public interface IPaintingsService
{
    Task<List<PaintingDto>> GetPaintings();
    Task<PaintingDto?> GetPaintingById(int id);
    Task<Painting> CreatePainting(CreatePaintingDto createdPainting);
    Task<bool> UpdatePaintingById(int id, UpdatePaintingDto updatedPainting);
    Task<bool> DeletePaintingById(int id);
}
