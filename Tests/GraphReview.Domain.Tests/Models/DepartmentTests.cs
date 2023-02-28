using GraphReview.Domain.Models;

namespace GraphReview.Domain.Tests.Models
{
    public class DepartmentTests
    {
        [Fact]
        public void GivenCorrectParamaters_WhenConstructorIsInvoked_ADepartmentIsCreated()
        {
            // Arrange
            var name = "Development";

            // Act
            var department = new Department(name);

            // Assert
            department.Should().NotBeNull();
            department.Id.Should().NotBeNullOrWhiteSpace();
            department.Name.Should().Be(name);
            department.Employees.Should().NotBeNull();
        }
    }
}
