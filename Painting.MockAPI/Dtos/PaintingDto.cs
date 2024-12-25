namespace Painting.MockAPI.Dtos;

public record class PaintingDto(int Id, string Name, string Artist, string Museum);
public record class PaintingWithoutMuseumDto(int Id, string Name, string Artist);
public record class PaintingWithoutArtistDto(int Id, string Name, string Museum);
