using Painting.MockAPI.Entities;
using Painting.MockAPI.Repositories;
using Painting.MockAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Painting.MockAPI.Tests
{
    public class ArtistRepositoryTests
    {
        private readonly ArtistRepository _repository;
        private readonly ApplicationDbContext _context;
        private readonly Museum _testMuseum;

        public ArtistRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid()
                .ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new ArtistRepository(_context);
            _testMuseum = new Museum { Name = "Test Museum" };

            _context.Museums.Add(_testMuseum);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllArtists()
        {
            var artist = new Artist { Name = "Test Artist" };

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(artist.Name, result.First().Name);
        }

        [Fact]
        public async Task GetAllArtistsWithArtworks()
        {
            var artist = new Artist { Name = "Test Artist" };

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            var artwork = new Artwork
            {
                Name = "Test Artwork",
                ArtistId = artist.Id,
                MuseumId = _testMuseum.Id
            };

            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            var artistDto = result.First();

            Assert.NotEmpty(artistDto.Artworks);
            Assert.Equal(artwork.Name, artistDto.Artworks.First().Name);
        }

        [Fact]
        public async Task GetArtistById()
        {
            var artist = new Artist { Name = "Test Artist" };

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            var result = await _repository.GetById(artist.Id);

            Assert.NotNull(result);
            Assert.Equal(artist.Id, result.Id);
            Assert.Equal(artist.Name, result.Name);
        }

        [Fact]
        public async Task GetArtistByInvalidId()
        {
            var result = await _repository.GetById(-1);
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateArtist()
        {
            var newArtist = new Artist { Name = "Test Artist" };
            var result = await _repository.Create(newArtist);

            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal(newArtist.Name, result.Name);
        }

        [Fact]
        public async Task UpdateArtist()
        {
            var artist = new Artist { Name = "Original Name" };

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            var updatedArtist = new Artist { Id = artist.Id, Name = "Updated Name" };
            var result = await _repository.UpdateById(artist.Id, updatedArtist);

            Assert.NotNull(result);
            Assert.Equal(updatedArtist.Name, result.Name);
        }

        [Fact]
        public async Task UpdateArtistWithInvalidId()
        {
            var updatedArtist = new Artist { Id = -1, Name = "Updated Name" };
            var result = await _repository.UpdateById(-1, updatedArtist);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteArtist()
        {
            var artist = new Artist { Name = "Test Artist" };

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            var result = await _repository.DeleteById(artist.Id);
            var deletedArtist = await _context.Artists.FindAsync(artist.Id);

            Assert.NotNull(result);
            Assert.Null(deletedArtist);
        }

        [Fact]
        public async Task DeleteArtistWithInvalidId()
        {
            var result = await _repository.DeleteById(-1);
            Assert.Null(result);
        }
    }
}
