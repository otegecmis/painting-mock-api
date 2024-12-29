using Painting.MockAPI.Data;
using Painting.MockAPI.Endpoints;
using Painting.MockAPI.Interfaces;
using Painting.MockAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SQLConnectionString");

builder.Services.AddSqlite<ApplicationDbContext>(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();
builder.Services.AddScoped<IMuseumRepository, MuseumRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Painting Mock API"); });
}

app.MapArtistsEndpoints().WithTags("Artists");
app.MapMuseumsEndpoints().WithTags("Museums");
app.MapArtworksEndpoints().WithTags("Artworks");

await app.MigrateDatabase();

app.Run();
