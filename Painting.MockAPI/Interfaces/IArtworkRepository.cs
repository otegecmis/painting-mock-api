using Painting.MockAPI.Dtos.Artwork;
using Painting.MockAPI.Entities;

namespace Painting.MockAPI.Interfaces;

public interface IArtworkRepository
{
    Task<List<ArtworkDto>> GetAll();
    Task<Artwork?> GetById(int id);
    Task<Artwork> Create(Artwork createdArtwork);
    Task<Artwork?> UpdateById(int id, Artwork updatedArtwork);
    Task<Artwork?> DeleteById(int id);
}
