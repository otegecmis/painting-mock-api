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
}
