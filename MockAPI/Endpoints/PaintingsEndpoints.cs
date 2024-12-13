using Microsoft.EntityFrameworkCore;
using MockAPI.Data;
using MockAPI.Mapping;

namespace MockAPI.Endpoints;

public static class PaintingsEndpoints
{
    public static RouteGroupBuilder MapPaintingsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("paintings");
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

        return group;
    }
}
