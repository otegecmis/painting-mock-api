using MockAPI.DTOs;
using MockAPI.Entities;

namespace MockAPI.Services;

public interface IMuseumsService
{
    Task<List<MuseumDTO>> GetAllMuseumsAsync();
    Task<MuseumDTO?> GetMuseumByIdAsync(int Id);
    Task<Museum> CreateMuseumAsync(CreateMuseumDTO createdMuseum);
    Task<bool> UpdateMuseumAsync(int Id, UpdateMuseumDTO updatedMuseum);
    Task<bool> DeleteMuseumAsync(int Id);
}
