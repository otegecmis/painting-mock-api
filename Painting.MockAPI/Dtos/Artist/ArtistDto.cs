using Painting.MockAPI.Dtos.Artwork;

namespace Painting.MockAPI.Dtos.Artist;

public record ArtistDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ArtworkWithoutArtistDto> Artworks { get; set; }

    public ArtistDto(int id, string name, List<ArtworkWithoutArtistDto>? artworks = null)
    {
        Id = id;
        Name = name;
        Artworks = artworks ?? new List<ArtworkWithoutArtistDto>();
    }
}
