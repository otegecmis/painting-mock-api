using Painting.MockAPI.Dtos.Artist;
using Painting.MockAPI.Interfaces;
using Painting.MockAPI.Mapping;

namespace Painting.MockAPI.Endpoints;

public static class ArtistsEndpoints
{
    private const string GetArtistEndpoint = "GetArtist";

    public static RouteGroupBuilder MapArtistsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("artists");
        group.WithTags("artists");

        group.MapGet("/", async (IArtistRepository artistRepository) =>
        {
            var artists = await artistRepository.GetAll();
            return Results.Ok(artists);
        });

        group.MapGet("/{id:int}", async (int id, IArtistRepository artistRepository) =>
        {
            var artist = await artistRepository.GetById(id);
            return artist is null ? Results.NotFound() : Results.Ok(artist.ToArtistDetailDto());
        }).WithName(GetArtistEndpoint);

        group.MapPost("/", async (CreateArtistDto createdArtist, IArtistRepository artistRepository) =>
        {
            var artist = await artistRepository.Create(createdArtist.ToEntity());
            return Results.CreatedAtRoute(GetArtistEndpoint, new { artist.Id }, artist.ToArtistWithoutArtworksDto());
        });

        group.MapPut("/{id:int}", async (int id, UpdateArtistDto updatedArtist, IArtistRepository artistRepository) =>
        {
            var artist = await artistRepository.UpdateById(id, updatedArtist.ToEntity(id));
            return artist is null ? Results.NotFound() : Results.Ok(artist.ToArtistWithoutArtworksDto());
        });

        group.MapDelete("/{id:int}", async (int id, IArtistRepository artistRepository) =>
        {
            var artist = await artistRepository.GetById(id);

            if (artist == null) return Results.NotFound();
            if (artist!.Artworks.Count > 0) return Results.BadRequest();

            var deletedArtist = await artistRepository.DeleteById(id);
            return deletedArtist is null ? Results.NotFound() : Results.NoContent();
        });

        return group;
    }
}
