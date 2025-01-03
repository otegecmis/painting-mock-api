namespace Painting.MockAPI.Dtos.Museum;

using System.ComponentModel.DataAnnotations;

public record CreateMuseumDto(
    [Required] [StringLength(100)] string Name
);
