using Painting.MockAPI.Dtos.Artist;
using Painting.MockAPI.Entities;

namespace Painting.MockAPI.Interfaces;

public interface IArtistRepository
{
    Task<List<ArtistDto>> GetAll();
    Task<Artist?> GetById(int id);
    Task<Artist> Create(Artist createdArtist);
    Task<Artist?> UpdateById(int id, Artist updatedArtist);
    Task<Artist?> DeleteById(int id);
}
