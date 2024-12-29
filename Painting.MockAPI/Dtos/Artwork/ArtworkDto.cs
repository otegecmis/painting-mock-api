namespace Painting.MockAPI.Dtos.Artwork;

public record class ArtworkDto(int Id, string Name, string Artist, string Museum);

public record class ArtworkWithoutMuseumDto(int Id, string Name, string Artist);

public record class ArtworkWithoutArtistDto(int Id, string Name, string Museum);
