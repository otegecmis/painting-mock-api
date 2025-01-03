using System.ComponentModel.DataAnnotations;

namespace Painting.MockAPI.Dtos.Artwork;

public record CreateArtworkDto(
    [Required] [StringLength(100)] string Name,
    [Required] int ArtistId,
    [Required] int MuseumId
);
