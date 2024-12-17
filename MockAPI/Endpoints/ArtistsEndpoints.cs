using MockAPI.DTOs;
using MockAPI.Services;

namespace MockAPI.Endpoints;

public static class ArtistsEndpoints
{
    public static RouteGroupBuilder MapArtistsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("artists");
        var getArtistEndpoint = "GetArtist";

        group.WithTags("artists");

        group.MapGet("/", async (IArtistsService artistsService) =>
        {
            var artists = await artistsService.GetArtists();
            return Results.Ok(artists);
        });

        group.MapGet("/{id}", async (int Id, IArtistsService artistsService) =>
        {
            var artist = await artistsService.GetArtistById(Id);

            if (artist is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(artist);
        }).WithName(getArtistEndpoint);

        group.MapPost("/", async (CreateArtistDTO createdArtist, IArtistsService artistsService) =>
        {
            var artist = await artistsService.CreateArtist(createdArtist);
            return Results.CreatedAtRoute(getArtistEndpoint, new { Id = artist.Id }, artist);
        });

        group.MapPut("/{id}", async (int Id, UpdateArtistDTO updatedArtist, IArtistsService artistsService) =>
        {
            var artist = await artistsService.UpdateArtist(Id, updatedArtist);

            if (!artist)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int Id, IArtistsService artistsService) =>
        {
            try
            {
                var artist = await artistsService.DeleteArtist(Id);

                if (!artist)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return group;
    }
}
