using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using GraphReview.Api.Controllers;
using GraphReview.Application.Abstractions.Employees;
using GraphReview.Application.Tests.Helpers;
using GraphReview.Contracts.Employee;
using GraphReview.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GraphReview.Api.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly IFixture _fixture;
        private readonly EmployeeController _employeeController;
        private readonly Mock<IEmployeeService> _employeeService;

        public EmployeeControllerTests()
        {
            _fixture = TestHelper.SetupFixture();

            _employeeService = _fixture.Freeze<Mock<IEmployeeService>>();

            _employeeController = new EmployeeController(_employeeService.Object);
        }

        [Fact]
        public async Task GivenExistingEmployee_WhenGetEmployeeByIdAsyncIsInvoked_ThenOkIsReturned()
        {
            // Arrange
            var employee = _fixture.Build<Employee>().Create();
            _employeeService.Setup(x => x.GetByIdAsync(employee.Id, default)).ReturnsAsync(employee);

            // Act
            var result = await _employeeController.GetEmployeeByIdAsync(employee.Id);
            var resultObject = GetObjectResultContent<EmployeeResponse>(result);

            // Assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Result.Should().BeOfType<OkObjectResult>();
            }

            using (new AssertionScope())
            {
                resultObject.Id.Should().Be(employee.Id);
                resultObject.FirstName.Should().Be(employee.FirstName);
                resultObject.LastName.Should().Be(employee.LastName);
                resultObject.Email.Should().Be(employee.Email);
            }
        }

        [Fact]
        public async Task GivenNonExistingEmployee_WhenGetEmployeeByIdAsyncIsInvoked_ThenNotFoundIsReturned()
        {
            // Arrange
            var employee = _fixture.Build<Employee>().Create();
            _employeeService.Setup(x => x.GetByIdAsync(employee.Id, default)).ReturnsAsync((Employee)null);

            // Act
            var result = await _employeeController.GetEmployeeByIdAsync(employee.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Result.Should().BeOfType<NotFoundResult>();
            }
        }

        public static T? GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T?)((ObjectResult?)result?.Result)?.Value;
        }
    }
}
