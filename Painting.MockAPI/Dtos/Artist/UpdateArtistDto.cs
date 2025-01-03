using System.ComponentModel.DataAnnotations;

namespace Painting.MockAPI.Dtos.Artist;

public record UpdateArtistDto(
    [Required] [StringLength(50)] string Name
);
