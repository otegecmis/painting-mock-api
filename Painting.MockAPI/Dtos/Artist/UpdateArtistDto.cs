using System.ComponentModel.DataAnnotations;

namespace Painting.MockAPI.Dtos.Artist;

public record class UpdateArtistDto(
    [Required] [StringLength(50)] string Name
);
