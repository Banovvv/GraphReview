using AutoFixture;
using GraphReview.Api.Controllers;
using GraphReview.Application.Abstractions.Employees;
using GraphReview.Application.Tests.Helpers;
using GraphReview.Domain.Models;
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


            // Assert
        }
    }
}
