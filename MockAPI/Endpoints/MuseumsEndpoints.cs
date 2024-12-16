using MockAPI.DTOs;
using MockAPI.Services;

namespace MockAPI.Endpoints;

public static class MuseumsEndpoints
{
    public static RouteGroupBuilder MapMuseumsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("museums");
        var getMuseumEndpointName = "GetMuseum";

        group.WithTags("museums");

        group.MapGet("/", async (IMuseumsService museumsService) =>
        {
            var museums = await museumsService.GetAllMuseumsAsync();

            return Results.Ok(museums);
        });

        group.MapGet("/{id}", async (int Id, IMuseumsService museumsService) =>
        {
            var museum = await museumsService.GetMuseumByIdAsync(Id);

            if (museum is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(museum);
        }).WithName(getMuseumEndpointName);

        group.MapPost("/", async (CreateMuseumDTO createMuseum, IMuseumsService museumsService) =>
        {
            var museum = await museumsService.CreateMuseumAsync(createMuseum);

            return Results.CreatedAtRoute(getMuseumEndpointName, new { Id = museum.Id }, museum);
        });

        group.MapPut("/{Id}", async (int Id, UpdateMuseumDTO updatedMuseum, IMuseumsService museumsService) =>
        {
            var existingMuseum = await museumsService.UpdateMuseumAsync(Id, updatedMuseum);

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
                var deleteResult = await museumsService.DeleteMuseumAsync(Id);

                if (!deleteResult)
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
