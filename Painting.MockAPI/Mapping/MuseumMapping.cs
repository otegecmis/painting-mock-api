using Painting.MockAPI.Dtos.Artwork;
using Painting.MockAPI.Dtos.Museum;
using Painting.MockAPI.Entities;

namespace Painting.MockAPI.Mapping;

public static class MuseumMapping
{
    public static MuseumDto ToMuseumDetailDto(this Museum museum)
    {
        return new MuseumDto(
            museum.Id,
            museum.Name,
            museum.Artworks.Select(artwork => new ArtworkWithoutMuseumDto(
                artwork.Id,
                artwork.Name,
                artwork.Artist.Name
            )).ToList()
        );
    }

    public static MuseumSummaryDto ToMuseumWithoutArtworksDto(this Museum museum)
    {
        return new MuseumSummaryDto(
            museum.Id,
            museum.Name
        );
    }

    public static Museum ToEntity(this CreateMuseumDto museum)
    {
        return new Museum
        {
            Name = museum.Name
        };
    }

    public static Museum ToEntity(this UpdateMuseumDto museum, int id)
    {
        return new Museum
        {
            Id = id,
            Name = museum.Name
        };
    }
}
