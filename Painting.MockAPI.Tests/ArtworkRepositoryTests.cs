using Painting.MockAPI.Entities;
using Painting.MockAPI.Repositories;
using Painting.MockAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Painting.MockAPI.Tests
{
    public class ArtworkRepositoryTests
    {
        private readonly ArtworkRepository _repository;
        private readonly ApplicationDbContext _context;
        private readonly Artist _testArtist;
        private readonly Museum _testMuseum;

        public ArtworkRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid()
                .ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new ArtworkRepository(_context);
            _testArtist = new Artist { Name = "Test Artist" };

            _context.Artists.Add(_testArtist);

            _testMuseum = new Museum { Name = "Test Museum" };

            _context.Museums.Add(_testMuseum);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllArtworks()
        {
            var artwork = new Artwork
            {
                Name = "Test Artwork",
                ArtistId = _testArtist.Id,
                MuseumId = _testMuseum.Id
            };

            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(artwork.Name, result.First().Name);
        }

        [Fact]
        public async Task GetArtworkById()
        {
            var artwork = new Artwork
            {
                Name = "Test Artwork",
                ArtistId = _testArtist.Id,
                MuseumId = _testMuseum.Id
            };

            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();

            var result = await _repository.GetById(artwork.Id);

            Assert.NotNull(result);
            Assert.Equal(artwork.Id, result.Id);
            Assert.Equal(artwork.Name, result.Name);
        }

        [Fact]
        public async Task GetArtworkByInvalidId()
        {
            var result = await _repository.GetById(-1);
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateArtwork()
        {
            var newArtwork = new Artwork
            {
                Name = "Test Artwork",
                ArtistId = _testArtist.Id,
                MuseumId = _testMuseum.Id
            };

            var result = await _repository.Create(newArtwork);

            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal(newArtwork.Name, result.Name);
            Assert.Equal(_testArtist.Id, result.ArtistId);
            Assert.Equal(_testMuseum.Id, result.MuseumId);
        }

        [Fact]
        public async Task CreateArtworkWithInvalidArtist()
        {
            var newArtwork = new Artwork
            {
                Name = "Test Artwork",
                ArtistId = -1,
                MuseumId = _testMuseum.Id
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => _repository.Create(newArtwork));
        }

        [Fact]
        public async Task CreateArtworkWithInvalidMuseum()
        {
            var newArtwork = new Artwork
            {
                Name = "Test Artwork",
                ArtistId = _testArtist.Id,
                MuseumId = -1
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => _repository.Create(newArtwork));
        }

        [Fact]
        public async Task UpdateArtwork()
        {
            var artwork = new Artwork
            {
                Name = "Original Name",
                ArtistId = _testArtist.Id,
                MuseumId = _testMuseum.Id
            };

            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();

            var updatedArtwork = new Artwork
            {
                Id = artwork.Id,
                Name = "Updated Name",
                ArtistId = _testArtist.Id,
                MuseumId = _testMuseum.Id
            };

            var result = await _repository.UpdateById(artwork.Id, updatedArtwork);

            Assert.NotNull(result);
            Assert.Equal(updatedArtwork.Name, result.Name);
        }

        [Fact]
        public async Task UpdateArtworkWithInvalidId()
        {
            var updatedArtwork = new Artwork
            {
                Id = -1,
                Name = "Updated Name",
                ArtistId = _testArtist.Id,
                MuseumId = _testMuseum.Id
            };

            var result = await _repository.UpdateById(-1, updatedArtwork);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteArtwork()
        {
            var artwork = new Artwork
            {
                Name = "Test Artwork",
                ArtistId = _testArtist.Id,
                MuseumId = _testMuseum.Id
            };

            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();

            var result = await _repository.DeleteById(artwork.Id);
            var deletedArtwork = await _context.Artworks.FindAsync(artwork.Id);

            Assert.NotNull(result);
            Assert.Null(deletedArtwork);
        }

        [Fact]
        public async Task DeleteArtworkWithInvalidId()
        {
            var result = await _repository.DeleteById(-1);
            Assert.Null(result);
        }
    }
}