using System.ComponentModel.DataAnnotations;

namespace MockAPI.Dtos;

public record class UpdateArtistDto(
    [Required] [StringLength(50)] string Name
);