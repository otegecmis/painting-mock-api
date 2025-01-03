using System.ComponentModel.DataAnnotations;

namespace Painting.MockAPI.Dtos.Museum;

public record class UpdateMuseumDto(
    [Required] [StringLength(100)] string Name
);
