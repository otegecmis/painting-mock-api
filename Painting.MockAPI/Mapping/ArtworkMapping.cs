using Painting.MockAPI.Dtos.Artwork;
using Painting.MockAPI.Entities;

namespace Painting.MockAPI.Mapping;

public static class ArtworkMapping
{
    public static ArtworkDto ToArtworkDetailDto(this Artwork artwork)
    {
        return new ArtworkDto(
            artwork.Id,
            artwork.Name,
            artwork.Artist.Name,
            artwork.Museum.Name
        );
    }

    public static Artwork ToEntity(this UpdateArtworkDto artwork, int id)
    {
        return new Artwork
        {
            Id = id,
            Name = artwork.Name,
            ArtistId = artwork.ArtistId,
            MuseumId = artwork.MuseumId
        };
    }
}
