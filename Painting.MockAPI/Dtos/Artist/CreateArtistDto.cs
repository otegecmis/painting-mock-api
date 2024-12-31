using System.ComponentModel.DataAnnotations;

namespace Painting.MockAPI.Dtos.Artist;

public record class CreateArtistDto(
    [Required][StringLength(50)] string Name
);
