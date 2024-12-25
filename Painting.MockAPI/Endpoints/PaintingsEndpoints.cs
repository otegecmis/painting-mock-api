using Painting.MockAPI.Dtos;
using Painting.MockAPI.Services;

namespace Painting.MockAPI.Endpoints;

public static class PaintingsEndpoints
{
    public static RouteGroupBuilder MapPaintingsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("paintings");
        const string getPaintingEndpoint = "GetPainting";

        group.WithTags("Paintings");

        group.MapGet("/", async (IPaintingsService paintingService) =>
        {
            var paintings = await paintingService.GetPaintings();
            return Results.Ok(paintings);
        });

        group.MapGet("/{id:int}", async (int id, IPaintingsService paintingService) =>
        {
            var painting = await paintingService.GetPaintingById(id);
            return painting is null ? Results.NotFound() : Results.Ok(painting);
        }).WithName(getPaintingEndpoint);

        group.MapPost("/", async (CreatePaintingDto createdPainting, IPaintingsService paintingService) =>
        {
            var painting = await paintingService.CreatePainting(createdPainting);
            return Results.CreatedAtRoute(getPaintingEndpoint, new { Id = painting.Id }, painting);
        });

        group.MapPut("/{id:int}",
            async (int id, UpdatePaintingDto updatedPainting, IPaintingsService paintingService) =>
            {
                var painting = await paintingService.UpdatePaintingById(id, updatedPainting);
                return !painting ? Results.NotFound() : Results.NoContent();
            });

        group.MapDelete("/{id:int}", async (int id, IPaintingsService paintingService) =>
        {
            var painting = await paintingService.DeletePaintingById(id);
            return !painting ? Results.NotFound() : Results.NoContent();
        });

        return group;
    }
}
