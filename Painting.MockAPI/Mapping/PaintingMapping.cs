using Painting.MockAPI.Entities;
using Painting.MockAPI.Dtos;

namespace Painting.MockAPI.Mapping;

public static class PaintingMapping
{
    public static PaintingDto ToPaintingDetailDto(this Entities.Painting painting)
    {
        return new PaintingDto(
            painting.Id,
            painting.Name,
            painting.Artist.Name,
            painting.Museum.Name
        );
    }

    public static Entities.Painting ToEntity(this CreatePaintingDto painting)
    {
        return new Entities.Painting
        {
            Name = painting.Name,
            ArtistId = painting.ArtistId,
            MuseumId = painting.MuseumId
        };
    }

    public static Entities.Painting ToEntity(this UpdatePaintingDto painting, int id)
    {
        return new Entities.Painting
        {
            Id = id,
            Name = painting.Name,
            ArtistId = painting.ArtistId,
            MuseumId = painting.MuseumId
        };
    }
}
