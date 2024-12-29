using Painting.MockAPI.Dtos.Artwork;
using Painting.MockAPI.Entities;
using Painting.MockAPI.Interfaces;

namespace Painting.MockAPI.Endpoints;

public static class ArtworksEndpoints
{
    private const string GetPaintingEndpoint = "GetPainting";

    public static RouteGroupBuilder MapArtworksEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("artworks");
        group.WithTags("Artworks");

        group.MapGet("/", async (IArtworkRepository artworkRepository) =>
        {
            var artworks = await artworkRepository.GetAll();
            return Results.Ok(artworks);
        });

        group.MapGet("/{id:int}", async (int id, IArtworkRepository artworkRepository) =>
        {
            var artwork = await artworkRepository.GetById(id);
            return artwork is null ? Results.NotFound() : Results.Ok(artwork);
        }).WithName(GetPaintingEndpoint);

        group.MapPost("/", async (Artwork createdPainting, IArtworkRepository artworkRepository) =>
        {
            var artworks = await artworkRepository.Create(createdPainting);
            return Results.CreatedAtRoute(GetPaintingEndpoint, new { Id = artworks.Id }, artworks);
        });

        group.MapPut("/{id:int}",
            async (int id, UpdateArtworkDto updatedArtwork, IArtworkRepository artworkRepository) =>
            {
                var artwork = await artworkRepository.UpdateById(id, updatedArtwork);
                return artwork is null ? Results.NotFound() : Results.Ok(artwork);
            });

        group.MapDelete("/{id:int}", async (int id, IArtworkRepository artworkRepository) =>
        {
            var artwork = await artworkRepository.DeleteById(id);
            return artwork is null ? Results.NotFound() : Results.NoContent();
        });

        return group;
    }
}
