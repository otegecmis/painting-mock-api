using Microsoft.EntityFrameworkCore;
using MockAPI.Data;
using MockAPI.DTOs;
using MockAPI.Entities;
using MockAPI.Mapping;

namespace MockAPI.Endpoints;

public static class PaintingsEndpoints
{
    public static RouteGroupBuilder MapPaintingsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("paintings");
        var getPaintingEndpointName = "GetPainting";

        group.WithTags("Paintings");

        group.MapGet("/", async (PaintingContext dbContext) =>
        {
            var paintings = await dbContext.Paintings
                                  .Include(p => p.Artist)
                                  .Include(p => p.Museum)
                                  .AsNoTracking()
                                  .Select(p => p.ToPaintingDetailDTO())
                                  .ToListAsync();

            return Results.Ok(paintings);
        });

        group.MapGet("/{id}", async (int id, PaintingContext dbContext) =>
        {
            var painting = await dbContext.Paintings
                                          .Include(p => p.Artist)
                                          .Include(p => p.Museum)
                                          .FirstOrDefaultAsync(p => p.Id == id);

            if (painting is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(painting.ToPaintingDetailDTO());
        }).WithName(getPaintingEndpointName);

        group.MapPost("/", async (CreatePaintingDTO newPainting, PaintingContext dbContext) =>
        {
            Painting painting = newPainting.ToEntity();
            dbContext.Paintings.Add(painting);

            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(getPaintingEndpointName, new { Id = painting.Id }, painting);
        });

        group.MapPut("/{id}", async (int id, UpdatePaintingDTO updatedPainting, PaintingContext dbContext) =>
        {
            var existingPainting = await dbContext.Paintings.FindAsync(id);

            if (existingPainting is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingPainting).CurrentValues.SetValues(updatedPainting.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, PaintingContext dbContext) =>
        {
            var existingPainting = await dbContext.Paintings.FindAsync(id);

            if (existingPainting is null)
            {
                return Results.NotFound();
            }

            dbContext.Paintings.Remove(existingPainting);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        return group;
    }
}
