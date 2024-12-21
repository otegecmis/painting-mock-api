using MockAPI.Entities;
using MockAPI.Dtos;

namespace MockAPI.Mapping;

public static class ArtistMapping
{
    public static ArtistDto ToArtistDetailDto(this Artist artist)
    {
        return new ArtistDto(
            artist.Id,
            artist.Name,
            artist.Paintings?.Select(painting => new PaintingWithoutArtistDto(
                painting.Id,
                painting.Name,
                painting.Museum.Name
            )).ToList()
        );
    }

    public static Artist ToEntity(this CreateArtistDto artist)
    {
        return new Artist()
        {
            Name = artist.Name
        };
    }

    public static Artist ToEntity(this UpdateArtistDto updatedArtist, int id)
    {
        return new Artist()
        {
            Id = id,
            Name = updatedArtist.Name
        };
    }
}
