using AutoFixture;
using GraphReview.Application.Constants;
using GraphReview.Application.Services;
using GraphReview.Application.Tests.Helpers;
using GraphReview.Domain.Exceptions;
using GraphReview.Domain.Models;
using GraphReview.Domain.Repositories;
using GraphReview.Domain.UnitOfWork;
using Moq;

namespace GraphReview.Application.Tests.Services
{
    public class DepartmentServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly DepartmentService _departmentService;
        private readonly Mock<IDepartmentRepository> _departmentRepository;

        public DepartmentServiceTests()
        {
            _fixture = TestHelper.SetupFixture();

            _departmentRepository = _fixture.Freeze<Mock<IDepartmentRepository>>();

            _unitOfWork = _fixture.Freeze<Mock<IUnitOfWork>>();
            _unitOfWork.Setup(x => x.DepartmentRepository)
                .Returns(_departmentRepository.Object);

            _departmentService = new DepartmentService(_unitOfWork.Object);
        }

        [Fact]
        public async Task GivenValidId_WhenGetByIdAsyncIsInvoked_ThenEntityIsReturned()
        {
            // Arrange
            var department = _fixture.Build<Department>().Create();
            _unitOfWork.Setup(x => x.DepartmentRepository.GetByIdAsync(department.Id, default)).ReturnsAsync(department);

            // Act
            var result = await _departmentService.GetByIdAsync(department.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(department.Id);
        }

        [Fact]
        public async Task GivenInvalidId_WhenGetByIdAsyncIsInvoked_ThenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            _unitOfWork.Setup(x => x.DepartmentRepository.GetByIdAsync(id, default)).ReturnsAsync((Department)null);

            // Act
            Func<Task> act = async () => await _departmentService.GetByIdAsync(id);

            // Assert
            await act.Should()
                .ThrowAsync<DepartmentNotFoundException>()
                .WithMessage(string.Format(ValidationMessages.DepartmentNotFound, id));
        }

        [Fact]
        public async Task WhenGetAllAsyncIsInvoked_ThenFullListOfEmployeesIsReturned()
        {
            // Arrange
            var departments = _fixture.Build<Department>().CreateMany(3);
            _unitOfWork.Setup(x => x.DepartmentRepository.GetAllAsync(default)).ReturnsAsync(departments);

            // Act
            var result = await _departmentService.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(3);
        }

        [Fact]
        public async Task GivenValidId_WhenDeleteAsyncIsInvoked_ThenEntityIsDeleted()
        {
            // Arrange
            var department = _fixture.Build<Department>().Create();
            _unitOfWork.Setup(x => x.DepartmentRepository.GetByIdAsync(department.Id, default)).ReturnsAsync(department);

            // Act
            var result = await _departmentService.DeleteAsync(department.Id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GivenInvalidId_WhenDeleteAsyncIsInvoked_ThenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            _unitOfWork.Setup(x => x.DepartmentRepository.GetByIdAsync(id, default)).ReturnsAsync((Department)null);

            // Act
            Func<Task> act = async () => await _departmentService.DeleteAsync(id);

            // Assert
            await act.Should()
                .ThrowAsync<DepartmentNotFoundException>()
                .WithMessage(string.Format(ValidationMessages.DepartmentNotFound, id));
        }

        [Fact]
        public async Task GivenValidEntity_WhenAddAsyncIsInvoked_ThenEntityIsAdded()
        {
            // Arrange
            var department = _fixture.Build<Department>().Create();

            // Act
            var result = await _departmentService.AddAsync(department);

            // Assert
            result.Should().BeTrue();
        }
    }
}
