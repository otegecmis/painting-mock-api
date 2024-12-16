using MockAPI.DTOs;
using MockAPI.Services;

namespace MockAPI.Endpoints;

public static class PaintingsEndpoints
{
    public static RouteGroupBuilder MapPaintingsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("paintings");
        var getPaintingEndpointName = "GetPainting";

        group.WithTags("Paintings");

        group.MapGet("/", async (IPaintingService paintingService) =>
        {
            var paintings = await paintingService.GetAllPaintingsAsync();
            return Results.Ok(paintings);
        });

        group.MapGet("/{Id}", async (int Id, IPaintingService paintingService) =>
        {
            var painting = await paintingService.GetPaintingByIdAsync(Id);

            if (painting is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(painting);
        }).WithName(getPaintingEndpointName);

        group.MapPost("/", async (CreatePaintingDTO createdPainting, IPaintingService paintingService) =>
        {
            var painting = await paintingService.CreatePaintingAsync(createdPainting);
            return Results.CreatedAtRoute(getPaintingEndpointName, new { Id = painting.Id }, painting);
        });

        group.MapPut("/{Id}", async (int Id, UpdatePaintingDTO updatedPainting, IPaintingService paintingService) =>
        {
            var painting = await paintingService.UpdatePaintingAsync(Id, updatedPainting);

            if (!painting)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        group.MapDelete("/{Id}", async (int Id, IPaintingService paintingService) =>
        {
            var painting = await paintingService.DeletePaintingAsync(Id);

            if (!painting)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        return group;
    }
}
