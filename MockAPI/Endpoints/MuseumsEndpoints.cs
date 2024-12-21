using MockAPI.Dtos;
using MockAPI.Services;

namespace MockAPI.Endpoints;

public static class MuseumsEndpoints
{
    public static RouteGroupBuilder MapMuseumsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("museums");
        const string getMuseumEndpoint = "GetMuseum";

        group.WithTags("museums");

        group.MapGet("/", async (IMuseumsService museumsService) =>
        {
            var museums = await museumsService.GetMuseums();
            return Results.Ok(museums);
        });

        group.MapGet("/{id:int}", async (int id, IMuseumsService museumsService) =>
        {
            var museum = await museumsService.GetMuseumById(id);
            return museum is null ? Results.NotFound() : Results.Ok(museum);
        }).WithName(getMuseumEndpoint);

        group.MapPost("/", async (CreateMuseumDto createdMuseum, IMuseumsService museumsService) =>
        {
            var museum = await museumsService.CreateMuseum(createdMuseum);
            return Results.CreatedAtRoute(getMuseumEndpoint, new { Id = museum.Id }, museum);
        });

        group.MapPut("/{id:int}", async (int id, UpdateMuseumDto updatedMuseum, IMuseumsService museumsService) =>
        {
            var existingMuseum = await museumsService.UpdateMuseumById(id, updatedMuseum);
            return !existingMuseum ? Results.NotFound() : Results.NoContent();
        });

        group.MapDelete("/{id:int}", async (int id, IMuseumsService museumsService) =>
        {
            try
            {
                var museum = await museumsService.DeleteMuseumById(id);
                return !museum ? Results.NotFound() : Results.NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        return group;
    }
}
