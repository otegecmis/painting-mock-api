using MockAPI.Entities;
using MockAPI.DTOs;

namespace MockAPI.Services;

public interface IArtistsService
{
    Task<List<ArtistDTO>> GetArtists();
    Task<ArtistDTO?> GetArtistById(int Id);
    Task<Artist> CreateArtist(CreateArtistDTO createdArtist);
    Task<bool> UpdateArtist(int Id, UpdateArtistDTO updatedArtist);
    Task<bool> DeleteArtist(int Id);
}
