using MockAPI.DTOs;
using MockAPI.Services;

namespace MockAPI.Endpoints;

public static class MuseumsEndpoints
{
    public static RouteGroupBuilder MapMuseumsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("museums");
        var getMuseumEndpoint = "GetMuseum";

        group.WithTags("museums");

        group.MapGet("/", async (IMuseumsService museumsService) =>
        {
            var museums = await museumsService.GetMuseums();
            return Results.Ok(museums);
        });

        group.MapGet("/{id}", async (int Id, IMuseumsService museumsService) =>
        {
            var museum = await museumsService.GetMuseumById(Id);

            if (museum is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(museum);
        }).WithName(getMuseumEndpoint);

        group.MapPost("/", async (CreateMuseumDTO createdMuseum, IMuseumsService museumsService) =>
        {
            var museum = await museumsService.CreateMuseum(createdMuseum);
            return Results.CreatedAtRoute(getMuseumEndpoint, new { Id = museum.Id }, museum);
        });

        group.MapPut("/{Id}", async (int Id, UpdateMuseumDTO updatedMuseum, IMuseumsService museumsService) =>
        {
            var existingMuseum = await museumsService.UpdateMuseum(Id, updatedMuseum);

            if (!existingMuseum)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        group.MapDelete("/{Id}", async (int Id, IMuseumsService museumsService) =>
        {
            try
            {
                var museum = await museumsService.DeleteMuseum(Id);

                if (!museum)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            }
            catch (InvalidOperationException ex)
            {

                return Results.BadRequest(ex.Message);
            }
        });

        return group;
    }
}
