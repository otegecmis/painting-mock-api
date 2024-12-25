namespace Painting.MockAPI.Entities;

public class Painting
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public int ArtistId { get; init; }
    public int MuseumId { get; init; }

    public Artist Artist { get; init; } = null!;
    public Museum Museum { get; init; } = null!;
}
