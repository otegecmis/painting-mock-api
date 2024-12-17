using MockAPI.Entities;
using MockAPI.DTOs;

namespace MockAPI.Mapping;

public static class PaintingMapping
{
    public static PaintingDTO ToPaintingDetailDTO(this Painting painting)
    {
        return new(
            painting.Id,
            painting.Name,
            painting.Artist.Name,
            painting.Museum.Name
        );
    }

    public static Painting ToEntity(this CreatePaintingDTO painting)
    {
        return new Painting
        {
            Name = painting.Name,
            ArtistId = painting.ArtistId,
            MuseumId = painting.MuseumId
        };
    }

    public static Painting ToEntity(this UpdatePaintingDTO painting, int id)
    {
        return new Painting
        {
            Id = id,
            Name = painting.Name,
            ArtistId = painting.ArtistId,
            MuseumId = painting.MuseumId
        };
    }
}
