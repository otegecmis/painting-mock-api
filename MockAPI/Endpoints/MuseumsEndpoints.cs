using Microsoft.EntityFrameworkCore;
using MockAPI.Data;
using MockAPI.DTOs;
using MockAPI.Entities;
using MockAPI.Mapping;

namespace MockAPI.Endpoints;

public static class MuseumsEndpoints
{
    public static RouteGroupBuilder MapMuseumsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("museums");
        var getMuseumEndpointName = "GetMuseum";

        group.WithTags("museums");

        group.MapGet("/", async (PaintingContext dbContext) =>
        {
            var museums = await dbContext.Museums
                                  .Include(m => m.Paintings)
                                  .ThenInclude(p => p.Artist)
                                  .AsNoTracking()
                                  .Select(m => m.ToMuseumDetailDTO())
                                  .ToListAsync();

            return Results.Ok(museums);
        });

        group.MapGet("/{id}", async (int Id, PaintingContext dbContext) =>
        {
            var museum = await dbContext.Museums.Include(m => m.Paintings)
                                                .ThenInclude(m => m.Artist)
                                                .FirstOrDefaultAsync(m => m.Id == Id);

            if (museum is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(museum.ToMuseumDetailDTO());
        }).WithName(getMuseumEndpointName);

        group.MapPost("/", async (CreateMuseumDTO newMuseum, PaintingContext dbContext) =>
        {
            Museum museum = newMuseum.ToEntity();
            dbContext.Museums.Add(museum);

            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(getMuseumEndpointName, new { Id = museum.Id }, museum);
        });

        group.MapPut("/{Id}", async (int Id, UpdateMuseumDTO updatedMuseum, PaintingContext dbContext) =>
        {
            var existingMuseum = await dbContext.Museums.FindAsync(Id);

            if (existingMuseum is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingMuseum).CurrentValues.SetValues(updatedMuseum.ToEntity(Id));
            return Results.NoContent();
        });

        group.MapDelete("/{Id}", async (int Id, PaintingContext dbContext) =>
        {
            var existingMuseum = await dbContext.Museums.FindAsync(Id);

            if (existingMuseum is null)
            {
                return Results.NotFound();
            }

            var countPaintings = dbContext.Paintings.Where(m => m.Museum.Id == Id).Count();

            if (countPaintings > 0)
            {
                return Results.BadRequest("Cannot delete museum with paintings.");
            }

            await dbContext.Museums.Where(museum => museum.Id == Id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }
}
