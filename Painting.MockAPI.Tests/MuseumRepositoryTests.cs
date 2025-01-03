using Painting.MockAPI.Entities;
using Painting.MockAPI.Repositories;
using Painting.MockAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Painting.MockAPI.Tests
{
    public class MuseumRepositoryTests
    {
        private readonly MuseumRepository _repository;
        private readonly ApplicationDbContext _context;
        private readonly Artist _testArtist;

        public MuseumRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid()
                .ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new MuseumRepository(_context);
            _testArtist = new Artist { Name = "Test Artist" };

            _context.Artists.Add(_testArtist);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllMuseums()
        {
            var museum = new Museum { Name = "Test Museum" };

            _context.Museums.Add(museum);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(museum.Name, result.First().Name);
        }

        [Fact]
        public async Task GetAllMuseumsWithArtworks()
        {
            var museum = new Museum { Name = "Test Museum" };

            _context.Museums.Add(museum);
            await _context.SaveChangesAsync();

            var artwork = new Artwork
            {
                Name = "Test Artwork",
                ArtistId = _testArtist.Id,
                MuseumId = museum.Id
            };

            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            var museumDto = result.First();

            Assert.NotEmpty(museumDto.Artworks);
            Assert.Equal(artwork.Name, museumDto.Artworks.First().Name);
        }

        [Fact]
        public async Task GetMuseumById()
        {
            var museum = new Museum { Name = "Test Museum" };

            _context.Museums.Add(museum);
            await _context.SaveChangesAsync();

            var result = await _repository.GetById(museum.Id);

            Assert.NotNull(result);
            Assert.Equal(museum.Id, result.Id);
            Assert.Equal(museum.Name, result.Name);
        }

        [Fact]
        public async Task GetMuseumByInvalidId()
        {
            var result = await _repository.GetById(-1);
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateMuseum()
        {
            var newMuseum = new Museum { Name = "Test Museum" };
            var result = await _repository.Create(newMuseum);

            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
            Assert.Equal(newMuseum.Name, result.Name);
        }

        [Fact]
        public async Task UpdateMuseum()
        {
            var museum = new Museum { Name = "Original Name" };

            _context.Museums.Add(museum);
            await _context.SaveChangesAsync();

            var updatedMuseum = new Museum { Id = museum.Id, Name = "Updated Name" };
            var result = await _repository.UpdateById(museum.Id, updatedMuseum);

            Assert.NotNull(result);
            Assert.Equal(updatedMuseum.Name, result.Name);
        }

        [Fact]
        public async Task UpdateMuseumWithInvalidId()
        {
            var updatedMuseum = new Museum { Id = -1, Name = "Updated Name" };
            var result = await _repository.UpdateById(-1, updatedMuseum);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteMuseum()
        {
            var museum = new Museum { Name = "Test Museum" };

            _context.Museums.Add(museum);
            await _context.SaveChangesAsync();

            var result = await _repository.DeleteById(museum.Id);
            var deletedMuseum = await _context.Museums.FindAsync(museum.Id);

            Assert.NotNull(result);
            Assert.Null(deletedMuseum);
        }

        [Fact]
        public async Task DeleteMuseumWithInvalidId()
        {
            var result = await _repository.DeleteById(-1);
            Assert.Null(result);
        }
    }
}
