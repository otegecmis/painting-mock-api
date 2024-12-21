using MockAPI.Entities;
using MockAPI.Dtos;

namespace MockAPI.Services;

public interface IArtistsService
{
    Task<List<ArtistDto>> GetArtists();
    Task<ArtistDto?> GetArtistById(int id);
    Task<Artist> CreateArtist(CreateArtistDto createdArtist);
    Task<bool> UpdateArtistById(int id, UpdateArtistDto updatedArtist);
    Task<bool> DeleteArtistById(int id);
}
