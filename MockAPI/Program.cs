using MockAPI.Data;
using MockAPI.Endpoints;
using MockAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PaintingMockAPI");

builder.Services.AddSqlite<PaintingContext>(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPaintingService, PaintingService>();
builder.Services.AddScoped<IArtistsService, ArtistsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Painting Mock API");
    });
}

app.MapArtistsEndpoints().WithTags("Artists");
app.MapMuseumsEndpoints().WithTags("Museums");
app.MapPaintingsEndpoints().WithTags("Paintings");

await app.MigrateDbAsync();

app.Run();
