namespace MockAPI.Entities;

public class Artist
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Painting> Paintings { get; set; } = new();
}
