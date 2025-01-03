namespace Painting.MockAPI.Dtos.Artwork;

public record ArtworkDto(
    int Id,
    string Name,
    string Artist,
    string Museum
);

public record ArtworkWithoutMuseumDto(
    int Id,
    string Name,
    string Artist
);

public record ArtworkWithoutArtistDto(
    int Id,
    string Name,
    string Museum
);
