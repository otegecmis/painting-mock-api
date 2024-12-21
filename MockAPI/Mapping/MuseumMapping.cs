using MockAPI.Entities;
using MockAPI.Dtos;

namespace MockAPI.Mapping;

public static class MuseumMapping
{
    public static MuseumDto ToMuseumDetailDto(this Museum museum)
    {
        return new MuseumDto(
            museum.Id,
            museum.Name,
            museum.Paintings.Select(painting => new PaintingWithoutMuseumDto(
                painting.Id,
                painting.Name,
                painting.Artist.Name
            )).ToList()
        );
    }

    public static Museum ToEntity(this CreateMuseumDto museum)
    {
        return new Museum()
        {
            Name = museum.Name
        };
    }

    public static Museum ToEntity(this UpdateMuseumDto updatedMuseum, int id)
    {
        return new Museum()
        {
            Id = id,
            Name = updatedMuseum.Name
        };
    }
}
