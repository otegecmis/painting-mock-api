namespace Painting.MockAPI.Dtos.Museum;

using System.ComponentModel.DataAnnotations;

public record class UpdateMuseumDto(
    [Required] [StringLength(100)] string Name
);
