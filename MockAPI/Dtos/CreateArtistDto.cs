using System.ComponentModel.DataAnnotations;

namespace MockAPI.Dtos;

public record class CreateArtistDto(
    [Required] [StringLength(50)] string Name
);
