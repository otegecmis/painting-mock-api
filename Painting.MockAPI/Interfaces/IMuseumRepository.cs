using Painting.MockAPI.Dtos.Museum;
using Painting.MockAPI.Entities;

namespace Painting.MockAPI.Interfaces;

public interface IMuseumRepository
{
    Task<List<MuseumDto>> GetAll();
    Task<Museum?> GetById(int id);
    Task<Museum> Create(Museum createdMuseum);
    Task<Museum?> UpdateById(int id, Museum updatedMuseum);
    Task<Museum?> DeleteById(int id);
}
