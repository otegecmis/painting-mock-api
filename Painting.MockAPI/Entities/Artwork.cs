using System.ComponentModel.DataAnnotations;

namespace Painting.MockAPI.Entities;

public class Artwork
{
    public int Id { get; init; }
    [MaxLength(100)] public required string Name { get; set; }
    public int ArtistId { get; set; }
    public int MuseumId { get; set; }

    public Artist Artist { get; set; } = null!;
    public Museum Museum { get; set; } = null!;
}
