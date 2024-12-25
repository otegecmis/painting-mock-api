namespace Painting.MockAPI.Entities;

public class Artist
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public List<Painting> Paintings { get; init; } = [];
}
