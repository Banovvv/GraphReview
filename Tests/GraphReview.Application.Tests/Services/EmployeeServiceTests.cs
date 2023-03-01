using AutoFixture;
using GraphReview.Application.Abstractions.Employees;
using GraphReview.Application.Services;
using GraphReview.Application.Tests.Helpers;
using GraphReview.Domain.Exceptions;
using GraphReview.Domain.Models;
using GraphReview.Domain.Repositories;
using GraphReview.Domain.UnitOfWork;
using Moq;

namespace GraphReview.Application.Tests.Services
{
    public class EmployeeServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IEmployeeService _employeeService;
        private readonly Mock<IEmployeeRepository> _employeeRepository;

        public EmployeeServiceTests()
        {
            _fixture = TestHelper.SetupFixture();

            _employeeRepository = _fixture.Freeze<Mock<IEmployeeRepository>>();

            _unitOfWork = _fixture.Freeze<Mock<IUnitOfWork>>();
            _unitOfWork.Setup(x => x.EmployeeRepository)
                .Returns(_employeeRepository.Object);

            _employeeService = new EmployeeService(_unitOfWork.Object);
        }

        [Fact]
        public async Task GivenValidId_WhenGetByIdAsyncIsInvoked_ThenEntityIsReturned()
        {
            // Arrange
            var employee = _fixture.Build<Employee>().Create();
            _unitOfWork.Setup(x => x.EmployeeRepository.GetByIdAsync(employee.Id, default)).ReturnsAsync(employee);

            // Act
            var result = await _employeeService.GetByIdAsync(employee.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(employee.Id);
        }

        [Fact]
        public async Task GivenInvalidId_WhenGetByIdAsyncIsInvoked_ThenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            _unitOfWork.Setup(x => x.EmployeeRepository.GetByIdAsync(id, default)).ReturnsAsync((Employee)null);

            // Act
            Func<Task> act = async () => await _employeeService.GetByIdAsync(id);

            // Assert
            await act.Should()
                .ThrowAsync<EmployeeNotFoundException>()
                .WithMessage("Employee not found!");
        }
    }
}
