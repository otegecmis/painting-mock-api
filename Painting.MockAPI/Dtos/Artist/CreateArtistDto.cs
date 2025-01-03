using System.ComponentModel.DataAnnotations;

namespace Painting.MockAPI.Dtos.Artist;

public record CreateArtistDto(
    [Required] [StringLength(50)] string Name
);
