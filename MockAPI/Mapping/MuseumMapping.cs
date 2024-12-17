using MockAPI.Entities;
using MockAPI.DTOs;

namespace MockAPI.Mapping;

public static class MuseumMapping
{
    public static MuseumDTO ToMuseumDetailDTO(this Museum museum)
    {
        return new(
            museum.Id,
            museum.Name,
            museum.Paintings?.Select(painting => new PaintingWithoutMuseumDTO(
                painting.Id,
                painting.Name,
                painting.Artist.Name
            )).ToList()
        );
    }

    public static Museum ToEntity(this CreateMuseumDTO museum)
    {
        return new Museum()
        {
            Name = museum.Name
        };
    }

    public static Museum ToEntity(this UpdateMuseumDTO updatedMuseum, int Id)
    {
        return new Museum()
        {
            Id = Id,
            Name = updatedMuseum.Name
        };
    }
}
