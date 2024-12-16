using MockAPI.DTOs;
using MockAPI.Entities;

namespace MockAPI.Services;

public interface IArtistsService
{
    Task<List<ArtistDTO>> GetAllArtistsAsync();
    Task<ArtistDTO?> GetArtistByIdAsync(int Id);
    Task<Artist> CreateArtistAsync(CreateArtistDTO createdArtist);
    Task<bool> UpdateArtistAsync(int Id, UpdateArtistDTO updatedArtist);
    Task<bool> DeleteArtistAsync(int Id);
}
