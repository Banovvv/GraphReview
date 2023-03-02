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

        [Fact]
        public async Task WhenGetAllAsyncIsInvoked_ThenFullListOfEmployeesIsReturned()
        {
            // Arrange
            var employees = _fixture.Build<Employee>().CreateMany(5);
            _unitOfWork.Setup(x => x.EmployeeRepository.GetAllAsync(default)).ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(5);
        }

        [Fact]
        public async Task GivenValidDepartmentId_WhenGetAllByDepartmentAsyncIsInvoked_ThenFullListOfEmployeesIsReturned()
        {
            // Arrange
            var department = _fixture.Build<Department>().Create();
            var employees = _fixture.Build<Employee>()
                .With(x => x.DepartmentId, department.Id)
                .CreateMany(5);
            _unitOfWork.Setup(x => x.DepartmentRepository.GetByIdAsync(department.Id, default)).ReturnsAsync(department);
            _unitOfWork.Setup(x => x.EmployeeRepository.GetAllByDepartmentAsync(department.Id, default)).ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetAllByDepartmentAsync(department.Id);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(5);
        }

        [Fact]
        public async Task GivenInvalidDepartmentId_WhenGetAllByDepartmentAsyncIsInvoked_ThenExceptionIsThrown()
        {
            // Arrange
            var departmentId = Guid.NewGuid().ToString();
            _unitOfWork.Setup(x => x.DepartmentRepository.GetByIdAsync(departmentId, default)).ReturnsAsync((Department)null);

            // Act
            Func<Task> act = async () => await _employeeService.GetAllByDepartmentAsync(departmentId);

            // Assert
            await act.Should()
                .ThrowAsync<DepartmentNotFoundException>()
                .WithMessage("Department not found!");
        }

        [Fact]
        public async Task GivenValidId_WhenDeleteAsyncIsInvoked_ThenEntityIsDeleted()
        {
            // Arrange
            var employee = _fixture.Build<Employee>().Create();
            _unitOfWork.Setup(x => x.EmployeeRepository.GetByIdAsync(employee.Id, default)).ReturnsAsync(employee);

            // Act
            var result = await _employeeService.DeleteAsync(employee.Id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GivenInvalidId_WhenDeleteAsyncIsInvoked_ThenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            _unitOfWork.Setup(x => x.EmployeeRepository.GetByIdAsync(id, default)).ReturnsAsync((Employee)null);

            // Act
            Func<Task> act = async () => await _employeeService.DeleteAsync(id);

            // Assert
            await act.Should()
                .ThrowAsync<EmployeeNotFoundException>()
                .WithMessage("Employee not found!");
        }

        [Fact]
        public async Task GivenValidEntity_WhenAddAsyncIsInvoked_ThenEntityIsAdded()
        {
            // Arrange
            var employee = _fixture.Build<Employee>().Create();

            // Act
            var result = await _employeeService.AddAsync(employee);

            // Assert
            result.Should().BeTrue();
        }
    }
}
