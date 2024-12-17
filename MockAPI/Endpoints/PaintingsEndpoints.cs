using MockAPI.DTOs;
using MockAPI.Services;

namespace MockAPI.Endpoints;

public static class PaintingsEndpoints
{
    public static RouteGroupBuilder MapPaintingsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("paintings");
        var getPaintingEndpoint = "GetPainting";

        group.WithTags("Paintings");

        group.MapGet("/", async (IPaintingsService paintingService) =>
        {
            var paintings = await paintingService.GetPaintings();
            return Results.Ok(paintings);
        });

        group.MapGet("/{Id}", async (int Id, IPaintingsService paintingService) =>
        {
            var painting = await paintingService.GetPaintingById(Id);

            if (painting is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(painting);
        }).WithName(getPaintingEndpoint);

        group.MapPost("/", async (CreatePaintingDTO createdPainting, IPaintingsService paintingService) =>
        {
            var painting = await paintingService.CreatePainting(createdPainting);
            return Results.CreatedAtRoute(getPaintingEndpoint, new { Id = painting.Id }, painting);
        });

        group.MapPut("/{Id}", async (int Id, UpdatePaintingDTO updatedPainting, IPaintingsService paintingService) =>
        {
            var painting = await paintingService.UpdatePainting(Id, updatedPainting);

            if (!painting)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        group.MapDelete("/{Id}", async (int Id, IPaintingsService paintingService) =>
        {
            var painting = await paintingService.DeletePainting(Id);

            if (!painting)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        return group;
    }
}
