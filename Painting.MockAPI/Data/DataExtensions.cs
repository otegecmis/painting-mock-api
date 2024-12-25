using Microsoft.EntityFrameworkCore;

namespace Painting.MockAPI.Data;

public static class DataExtensions
{
    public static async Task MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();
    }
}
