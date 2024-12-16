using MockAPI.DTOs;
using MockAPI.Services;

namespace MockAPI.Endpoints;

public static class ArtistsEndpoints
{
    public static RouteGroupBuilder MapArtistsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("artists");
        var getArtistEndpointName = "GetArtist";

        group.WithTags("artists");

        group.MapGet("/", async (IArtistsService artistsService) =>
        {
            var artists = await artistsService.GetAllArtistsAsync();
            return Results.Ok(artists);
        });

        group.MapGet("/{id}", async (int Id, IArtistsService artistsService) =>
        {
            var artist = await artistsService.GetArtistByIdAsync(Id);

            if (artist is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(artist);
        }).WithName(getArtistEndpointName);

        group.MapPost("/", async (CreateArtistDTO createdArtist, IArtistsService artistsService) =>
        {
            var artist = await artistsService.CreateArtistAsync(createdArtist);
            return Results.CreatedAtRoute(getArtistEndpointName, new { Id = artist.Id }, artist);
        });

        group.MapPut("/{id}", async (int Id, UpdateArtistDTO updatedArtist, IArtistsService artistsService) =>
        {
            var artist = await artistsService.UpdateArtistAsync(Id, updatedArtist);

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
                var deleteResult = await artistsService.DeleteArtistAsync(Id);

                if (!deleteResult)
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
