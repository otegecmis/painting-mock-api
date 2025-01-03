using Painting.MockAPI.Dtos.Artwork;

namespace Painting.MockAPI.Dtos.Museum;

public record MuseumDto
{
    public MuseumDto(int id, string name, List<ArtworkWithoutMuseumDto>? artworks = null)
    {
        Id = id;
        Name = name;
        Artworks = artworks ?? new List<ArtworkWithoutMuseumDto>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public List<ArtworkWithoutMuseumDto> Artworks { get; set; }
}
