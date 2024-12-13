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
}
