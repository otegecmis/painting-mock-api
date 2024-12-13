namespace MockAPI.Entities;

public class Painting
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int ArtistId { get; set; }
    public int MuseumId { get; set; }

    public Artist Artist { get; set; } = null!;
    public Museum Museum { get; set; } = null!;
}
