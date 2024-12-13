using Microsoft.EntityFrameworkCore;
using MockAPI.Data;
using MockAPI.Mapping;

namespace MockAPI.Endpoints;

public static class MuseumsEndpoints
{
    public static RouteGroupBuilder MapMuseumsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("museums");
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

        return group;
    }
}
