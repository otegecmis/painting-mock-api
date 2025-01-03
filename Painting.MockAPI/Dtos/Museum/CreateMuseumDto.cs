using System.ComponentModel.DataAnnotations;

namespace Painting.MockAPI.Dtos.Museum;

public record CreateMuseumDto(
    [Required] [StringLength(100)] string Name
);
