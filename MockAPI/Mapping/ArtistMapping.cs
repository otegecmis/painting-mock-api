using MockAPI.Entities;
using MockAPI.DTOs;

namespace MockAPI.Mapping;

public static class ArtistMapping
{
    public static ArtistDTO ToArtistDetailDTO(this Artist artist)
    {
        return new(
            artist.Id,
            artist.Name,
            artist.Paintings?.Select(painting => new PaintingWithoutArtistDTO(
                painting.Id,
                painting.Name,
                painting.Museum.Name
            )).ToList()
         );
    }

    public static Artist ToEntity(this CreateArtistDTO artist)
    {
        return new Artist()
        {
            Name = artist.Name
        };
    }

    public static Artist ToEntity(this UpdateArtistDTO updatedArtist, int Id)
    {
        return new Artist()
        {
            Id = Id,
            Name = updatedArtist.Name
        };
    }
}
