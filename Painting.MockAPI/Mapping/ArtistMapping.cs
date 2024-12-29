using Painting.MockAPI.Dtos.Artist;
using Painting.MockAPI.Dtos.Artwork;
using Painting.MockAPI.Entities;

namespace Painting.MockAPI.Mapping;

public static class ArtistMapping
{
    public static ArtistDto ToArtistDetailDto(this Artist artist)
    {
        return new ArtistDto(
            artist.Id,
            artist.Name,
            artist.Artworks?.Select(artwork => new ArtworkWithoutArtistDto(
                artwork.Id,
                artwork.Name,
                artwork.Museum.Name
            )).ToList()
        );
    }
}
