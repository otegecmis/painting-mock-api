namespace MockAPI.DTOs;

public record class PaintingDTO(int Id, string Name, string Artist, string Museum);
public record class PaintingWithoutMuseumDTO(int Id, string Name, string Artist);
public record class PaintingWithoutArtistDTO(int Id, string Name, string Museum);
