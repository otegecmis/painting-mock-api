namespace MockAPI.DTOs;

public record class ArtistDTO
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<PaintingWithoutArtistDTO> Paintings { get; init; }

    public ArtistDTO(int id, string name, List<PaintingWithoutArtistDTO>? paintings = null)
    {
        Id = id;
        Name = name;
        Paintings = paintings ?? new List<PaintingWithoutArtistDTO>();
    }
}
