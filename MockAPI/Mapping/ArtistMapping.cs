using MockAPI.DTOs;
using MockAPI.Entities;

namespace MockAPI.Mapping;

public static class ArtistMapping
{
    public static ArtistDTO ToArtistDetailDTO(this Artist artist)
    {
        return new(
             artist.Id,
             artist.Name,
             artist.Paintings?.Select(p => new PaintingWithoutArtistDTO(
                 p.Id,
                 p.Name,
                 p.Museum.Name
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
