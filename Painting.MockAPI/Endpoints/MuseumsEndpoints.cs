using Painting.MockAPI.Dtos.Museum;
using Painting.MockAPI.Interfaces;
using Painting.MockAPI.Mapping;

namespace Painting.MockAPI.Endpoints;

public static class MuseumsEndpoints
{
    private const string GetMuseumEndpoint = "GetMuseum";

    public static RouteGroupBuilder MapMuseumsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("museums");
        group.WithTags("museums");

        group.MapGet("/", async (IMuseumRepository museumRepository) =>
        {
            var museums = await museumRepository.GetAll();
            return Results.Ok(museums);
        });

        group.MapGet("/{id:int}", async (int id, IMuseumRepository museumRepository) =>
        {
            var museum = await museumRepository.GetById(id);
            return museum is null ? Results.NotFound() : Results.Ok(museum.ToMuseumDetailDto());
        }).WithName(GetMuseumEndpoint);

        group.MapPost("/", async (CreateMuseumDto createdMuseum, IMuseumRepository museumRepository) =>
        {
            var museum = await museumRepository.Create(createdMuseum.ToEntity());
            return Results.CreatedAtRoute(GetMuseumEndpoint, new { museum.Id }, museum.ToMuseumWithoutArtworksDto());
        });

        group.MapPut("/{id:int}", async (int id, UpdateMuseumDto updatedMuseum, IMuseumRepository museumRepository) =>
        {
            var museum = await museumRepository.UpdateById(id, updatedMuseum.ToEntity(id));
            return museum is null ? Results.NotFound() : Results.Ok(museum.ToMuseumWithoutArtworksDto());
        });

        group.MapDelete("/{id:int}", async (int id, IMuseumRepository museumRepository) =>
        {
            var museum = await museumRepository.GetById(id);

            if (museum is null) return Results.NotFound();
            if (museum!.Artworks.Count > 0) return Results.BadRequest();

            var deletedMuseum = await museumRepository.DeleteById(id);
            return deletedMuseum is null ? Results.NotFound() : Results.NoContent();
        });

        return group;
    }
}
