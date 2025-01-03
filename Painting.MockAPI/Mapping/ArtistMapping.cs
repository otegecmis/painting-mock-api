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

    public static ArtistSummaryDto ToArtistWithoutArtworksDto(this Artist artist)
    {
        return new ArtistSummaryDto(
            artist.Id,
            artist.Name
        );
    }

    public static Artist ToEntity(this CreateArtistDto artist)
    {
        return new Artist
        {
            Name = artist.Name
        };
    }

    public static Artist ToEntity(this UpdateArtistDto updatedArtist, int id)
    {
        return new Artist
        {
            Id = id,
            Name = updatedArtist.Name
        };
    }
}
