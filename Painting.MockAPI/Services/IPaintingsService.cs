using Painting.MockAPI.Entities;
using Painting.MockAPI.Dtos;

namespace Painting.MockAPI.Services;

public interface IPaintingsService
{
    Task<List<PaintingDto>> GetPaintings();
    Task<PaintingDto?> GetPaintingById(int id);
    Task<Entities.Painting> CreatePainting(CreatePaintingDto createdPainting);
    Task<bool> UpdatePaintingById(int id, UpdatePaintingDto updatedPainting);
    Task<bool> DeletePaintingById(int id);
}
