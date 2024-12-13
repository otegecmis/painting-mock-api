using MockAPI.DTOs;
using MockAPI.Entities;

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
}
