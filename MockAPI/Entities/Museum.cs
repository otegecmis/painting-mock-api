namespace MockAPI.Entities;

public class Museum
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Painting> Paintings { get; set; } = new List<Painting>();
}
