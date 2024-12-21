using MockAPI.Data;
using MockAPI.Services;
using MockAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PaintingMockAPI");

builder.Services.AddSqlite<PaintingContext>(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPaintingsService, PaintingsService>();
builder.Services.AddScoped<IMuseumsService, MuseumsService>();
builder.Services.AddScoped<IArtistsService, ArtistsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Painting Mock API"); });
}

app.MapArtistsEndpoints().WithTags("Artists");
app.MapMuseumsEndpoints().WithTags("Museums");
app.MapPaintingsEndpoints().WithTags("Paintings");

await app.MigrateDatabase();

app.Run();
