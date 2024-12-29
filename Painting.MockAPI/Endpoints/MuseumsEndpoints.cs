using Painting.MockAPI.Dtos.Museum;
using Painting.MockAPI.Entities;
using Painting.MockAPI.Interfaces;

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
            return museum is null ? Results.NotFound() : Results.Ok(museum);
        }).WithName(GetMuseumEndpoint);

        group.MapPost("/", async (Museum createdMuseum, IMuseumRepository museumRepository) =>
        {
            var museum = await museumRepository.Create(createdMuseum);
            return Results.CreatedAtRoute(GetMuseumEndpoint, new { museum.Id }, museum);
        });

        group.MapPut("/{id:int}",
            async (int id, UpdateMuseumDto updatedMuseumDto, IMuseumRepository museumRepository) =>
            {
                var museum = await museumRepository.UpdateById(id, updatedMuseumDto);
                return museum is null ? Results.NotFound() : Results.Ok(museum);
            });

        group.MapDelete("/{id:int}", async (int id, IMuseumRepository museumRepository) =>
        {
            var museum = await museumRepository.DeleteById(id);
            return museum is null ? Results.NotFound() : Results.NoContent();
        });

        return group;
    }
}
