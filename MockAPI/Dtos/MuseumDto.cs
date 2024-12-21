namespace MockAPI.Dtos;

public record MuseumDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<PaintingWithoutMuseumDto> Paintings { get; init; }

    public MuseumDto(int id, string name, List<PaintingWithoutMuseumDto>? paintings = null)
    {
        Id = id;
        Name = name;
        Paintings = paintings ?? new List<PaintingWithoutMuseumDto>();
    }
}
