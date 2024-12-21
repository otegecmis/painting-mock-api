namespace MockAPI.Entities;

public class Museum
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public List<Painting> Paintings { get; init; } = [];
}
