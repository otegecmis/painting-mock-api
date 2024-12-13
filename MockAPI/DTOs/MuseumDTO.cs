namespace MockAPI.DTOs;

public record MuseumDTO
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<PaintingWithoutMuseumDTO> Paintings { get; init; }

    public MuseumDTO(int id, string name, List<PaintingWithoutMuseumDTO>? paintings = null)
    {
        Id = id;
        Name = name;
        Paintings = paintings ?? new List<PaintingWithoutMuseumDTO>();
    }
}
