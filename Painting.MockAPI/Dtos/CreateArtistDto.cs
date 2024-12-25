using System.ComponentModel.DataAnnotations;

namespace Painting.MockAPI.Dtos;

public record class CreateArtistDto(
    [Required] [StringLength(50)] string Name
);
