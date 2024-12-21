using MockAPI.Entities;
using MockAPI.Dtos;

namespace MockAPI.Services;

public interface IMuseumsService
{
    Task<List<MuseumDto>> GetMuseums();
    Task<MuseumDto?> GetMuseumById(int id);
    Task<Museum> CreateMuseum(CreateMuseumDto createdMuseum);
    Task<bool> UpdateMuseumById(int id, UpdateMuseumDto updatedMuseum);
    Task<bool> DeleteMuseumById(int id);
}
