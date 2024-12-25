using System.ComponentModel.DataAnnotations;

namespace Painting.MockAPI.Dtos;

public record class UpdateArtistDto(
    [Required] [StringLength(50)] string Name
);