using MockAPI.DTOs;
using MockAPI.Entities;

namespace MockAPI.Mapping;

public static class MuseumMapping
{
    public static MuseumDTO ToMuseumDetailDTO(this Museum museum)
    {
        return new(
            museum.Id,
            museum.Name,
            museum.Paintings?.Select(p => new PaintingWithoutMuseumDTO(
                p.Id,
                p.Name,
                p.Artist.Name
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
