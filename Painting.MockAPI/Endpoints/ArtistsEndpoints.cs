using Painting.MockAPI.Dtos;
using Painting.MockAPI.Services;

namespace Painting.MockAPI.Endpoints;

public static class ArtistsEndpoints
{
    public static RouteGroupBuilder MapArtistsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("artists");
        const string getArtistEndpoint = "GetArtist";

        group.WithTags("artists");

        group.MapGet("/", async (IArtistsService artistsService) =>
        {
            var artists = await artistsService.GetArtists();
            return Results.Ok(artists);
        });

        group.MapGet("/{id:int}", async (int id, IArtistsService artistsService) =>
        {
            var artist = await artistsService.GetArtistById(id);
            return artist is null ? Results.NotFound() : Results.Ok(artist);
        }).WithName(getArtistEndpoint);

        group.MapPost("/", async (CreateArtistDto createdArtist, IArtistsService artistsService) =>
        {
            var artist = await artistsService.CreateArtist(createdArtist);
            return Results.CreatedAtRoute(getArtistEndpoint, new { artist.Id }, artist);
        });

        group.MapPut("/{id:int}", async (int id, UpdateArtistDto updatedArtist, IArtistsService artistsService) =>
        {
            var artist = await artistsService.UpdateArtistById(id, updatedArtist);
            return !artist ? Results.NotFound() : Results.NoContent();
        });

        group.MapDelete("/{id:int}", async (int id, IArtistsService artistsService) =>
        {
            try
            {
                var artist = await artistsService.DeleteArtistById(id);
                return !artist ? Results.NotFound() : Results.NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return group;
    }
}
