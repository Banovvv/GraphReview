using AutoFixture;
using FluentAssertions;
using GraphReview.Application.Tests.Helpers;
using GraphReview.Domain.Models;
using GraphReview.Infrastructure.Data;
using GraphReview.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GraphReview.Infrastructure.Tests.Repositories
{
    public class GenericRepositoryTests
    {
        private readonly IFixture _fixture;
        private readonly Employee _employee;
        private readonly ApplicationDbContext _context;
        private readonly GenericRepository<Employee> _repository;

        public GenericRepositoryTests()
        {
            _fixture = TestHelper.SetupFixture();

            _employee = _fixture.Build<Employee>()
                .Create();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureCreated();

            _repository = new GenericRepository<Employee>(_context);
        }

        [Fact]
        public async Task GivenValidEntity_WhenAddAsyncIsInvoked_ThenEntityIsAddedToDatabase()
        {
            // Act
            await _repository.AddAsync(_employee);
            await _context.SaveChangesAsync();
            var entities = await _repository.GetAllAsync();

            // Assert
            entities.Count().Should().Be(1);
        }

        [Fact]
        public async Task GivenValidEntity_WhenDeleteIsInvoked_ThenEntityIsDeletedFromDatabase()
        {
            // Arrange
            await _repository.AddAsync(_employee);
            await _context.SaveChangesAsync();

            // Act
            _repository.Delete(_employee);
            await _context.SaveChangesAsync();
            var entities = await _repository.GetAllAsync();

            // Assert
            entities.Count().Should().Be(0);
        }

        [Fact]
        public async Task GivenValidEntityId_WhenGetByIdAsyncIsInvoked_ThenEntityIsFetchedFromDatabase()
        {
            // Arrange
            await _repository.AddAsync(_employee);
            await _context.SaveChangesAsync();

            // Act
            var entity = await _repository.GetByIdAsync(_employee.Id, default);

            // Assert
            entity.Should().NotBeNull();
            entity.Should().BeEquivalentTo(_employee);
        }

        [Fact]
        public async Task GivenValidEntity_WhenUpdateIsInvoked_ThenEntityIsUpdatedInDatabase()
        {
            // Arrange
            await _repository.AddAsync(_employee);
            await _context.SaveChangesAsync();

            // Act
            var entity = await _repository.GetByIdAsync(_employee.Id, default);

            var unchangedEntity = new Employee(entity.FirstName, entity.LastName, entity.Email)
            {
                Id = entity.Id
            };

            entity.FirstName = "John";

            _repository.Update(entity);
            await _context.SaveChangesAsync();

            entity = await _repository.GetByIdAsync(_employee.Id, default);

            // Assert
            entity.Should().NotBeNull();
            entity.Id.Should().Be(unchangedEntity.Id);
            entity.FirstName.Should().NotBe(unchangedEntity.FirstName);
            entity.LastName.Should().Be(unchangedEntity.LastName);
            entity.Email.Should().Be(unchangedEntity.Email);
        }
    }
}