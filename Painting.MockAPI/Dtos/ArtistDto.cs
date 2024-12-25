namespace Painting.MockAPI.Dtos;

public record class ArtistDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<PaintingWithoutArtistDto> Paintings { get; init; }

    public ArtistDto(int id, string name, List<PaintingWithoutArtistDto>? paintings = null)
    {
        Id = id;
        Name = name;
        Paintings = paintings ?? new List<PaintingWithoutArtistDto>();
    }
}
