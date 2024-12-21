using MockAPI.Entities;
using MockAPI.Dtos;

namespace MockAPI.Mapping;

public static class PaintingMapping
{
    public static PaintingDto ToPaintingDetailDto(this Painting painting)
    {
        return new PaintingDto(
            painting.Id,
            painting.Name,
            painting.Artist.Name,
            painting.Museum.Name
        );
    }

    public static Painting ToEntity(this CreatePaintingDto painting)
    {
        return new Painting
        {
            Name = painting.Name,
            ArtistId = painting.ArtistId,
            MuseumId = painting.MuseumId
        };
    }

    public static Painting ToEntity(this UpdatePaintingDto painting, int id)
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
