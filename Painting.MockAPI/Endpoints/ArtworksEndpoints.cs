using Painting.MockAPI.Dtos.Artwork;
using Painting.MockAPI.Interfaces;
using Painting.MockAPI.Mapping;

namespace Painting.MockAPI.Endpoints;

public static class ArtworksEndpoints
{
    private const string GetArtworkEndpoint = "GetArtwork";

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
            return artwork is null ? Results.NotFound() : Results.Ok(artwork.ToArtworkDetailDto());
        }).WithName(GetArtworkEndpoint);

        group.MapPost("/", async (CreateArtworkDto createdArtwork, IArtworkRepository artworkRepository) =>
        {
            try
            {
                var artworks = await artworkRepository.Create(createdArtwork.ToEntity());
                return Results.CreatedAtRoute(GetArtworkEndpoint, new { artworks.Id }, artworks);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{id:int}", async (int id, UpdateArtworkDto updatedArtwork, IArtworkRepository artworkRepository) =>
        {
            var artwork = await artworkRepository.UpdateById(id, updatedArtwork.ToEntity(id));
            if (artwork is null) return Results.NotFound();

            return Results.Ok(artwork.ToArtworkDetailDto());
        });

        group.MapDelete("/{id:int}", async (int id, IArtworkRepository artworkRepository) =>
        {
            var artwork = await artworkRepository.DeleteById(id);
            return artwork is null ? Results.NotFound() : Results.NoContent();
        });

        return group;
    }
}
