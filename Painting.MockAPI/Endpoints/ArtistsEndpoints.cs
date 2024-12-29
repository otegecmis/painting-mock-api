using Painting.MockAPI.Dtos.Artist;
using Painting.MockAPI.Entities;
using Painting.MockAPI.Interfaces;

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
            return artist is null ? Results.NotFound() : Results.Ok(artist);
        }).WithName(GetArtistEndpoint);

        group.MapPost("/", async (Artist createdArtist, IArtistRepository artistRepository) =>
        {
            var artist = await artistRepository.Create(createdArtist);
            return Results.CreatedAtRoute(GetArtistEndpoint, new { artist.Id }, artist);
        });

        group.MapPut("/{id:int}", async (int id, UpdateArtistDto updatedArtist, IArtistRepository artistRepository) =>
        {
            var artist = await artistRepository.UpdateById(id, updatedArtist);
            return artist is null ? Results.NotFound() : Results.Ok(artist);
        });

        group.MapDelete("/{id:int}", async (int id, IArtistRepository artistRepository) =>
        {
            var artist = await artistRepository.DeleteById(id);
            return artist is null ? Results.NotFound() : Results.NoContent();
        });

        return group;
    }
}
