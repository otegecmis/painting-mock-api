using MockAPI.Entities;
using MockAPI.DTOs;

namespace MockAPI.Services;

public interface IMuseumsService
{
    Task<List<MuseumDTO>> GetMuseums();
    Task<MuseumDTO?> GetMuseumById(int Id);
    Task<Museum> CreateMuseum(CreateMuseumDTO createdMuseum);
    Task<bool> UpdateMuseum(int Id, UpdateMuseumDTO updatedMuseum);
    Task<bool> DeleteMuseum(int Id);
}
