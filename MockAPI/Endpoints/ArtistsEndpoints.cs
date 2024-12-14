using Microsoft.EntityFrameworkCore;
using MockAPI.Data;
using MockAPI.DTOs;
using MockAPI.Entities;
using MockAPI.Mapping;

namespace MockAPI.Endpoints;

public static class ArtistsEndpoints
{
    public static RouteGroupBuilder MapArtistsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("artists");
        var getArtistEndpointName = "GetArtist";

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

        group.MapGet("/{id}", async (int id, PaintingContext dbContext) =>
        {
            var artist = await dbContext.Artists
                                        .Include(a => a.Paintings)
                                        .ThenInclude(p => p.Museum)
                                        .FirstOrDefaultAsync(a => a.Id == id);

            if (artist is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(artist!.ToArtistDetailDTO());
        }).WithName(getArtistEndpointName);

        group.MapPost("/", async (CreateArtistDTO newArtist, PaintingContext dbContext) =>
        {
            Artist artist = newArtist.ToEntity();
            dbContext.Artists.Add(artist);

            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(getArtistEndpointName, new { Id = artist.Id }, artist);
        });

        group.MapPut("/{id}", async (int Id, UpdateArtistDTO updatedArtist, PaintingContext dbContext) =>
        {
            var existingArtist = await dbContext.Artists.FindAsync(Id);

            if (existingArtist is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingArtist).CurrentValues.SetValues(updatedArtist.ToEntity(Id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, PaintingContext dbContext) =>
        {
            var existingArtist = await dbContext.Artists.FindAsync(id);

            if (existingArtist is null)
            {
                return Results.NotFound();
            }

            var countPaintings = dbContext.Paintings.Where(p => p.Artist.Id == id).Count();

            if (countPaintings > 0)
            {
                return Results.BadRequest("Cannot delete artist with paintings.");
            }

            await dbContext.Artists.Where(artist => artist.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }
}
