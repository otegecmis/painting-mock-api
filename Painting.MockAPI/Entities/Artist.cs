using System.ComponentModel.DataAnnotations;

namespace Painting.MockAPI.Entities;

public class Artist
{
    public int Id { get; init; }
    [MaxLength(100)] public required string Name { get; set; }
    public List<Artwork> Artworks { get; set; } = [];
}
