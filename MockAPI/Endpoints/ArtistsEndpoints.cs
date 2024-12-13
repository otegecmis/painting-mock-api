using Microsoft.EntityFrameworkCore;
using MockAPI.Data;
using MockAPI.Mapping;

namespace MockAPI.Endpoints;

public static class ArtistsEndpoints
{
    public static RouteGroupBuilder MapArtistsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("artists");
        group.WithTags("artists");

        group.MapGet("/", async (PaintingContext dbContext) =>
        {
            var artists = await dbContext.Artists
                                  .Include(m => m.Paintings)
                                  .ThenInclude(p => p.Museum)
                                  .AsNoTracking()
                                  .Select(a => a.ToArtistDetailDTO())
                                  .ToListAsync();

            return Results.Ok(artists);
        });

        return group;
    }
}
