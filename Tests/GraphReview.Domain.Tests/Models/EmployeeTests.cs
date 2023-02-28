using GraphReview.Domain.Models;

namespace GraphReview.Domain.Tests.Models
{
    public class EmployeeTests
    {
        [Fact]
        public void GivenCorrectParamaters_WhenConstructorIsInvoked_AnEmployeeIsCreated()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var email = "john@doe.com";

            // Act
            var employee = new Employee(firstName, lastName, email);

            // Assert
            employee.Should().NotBeNull();
            employee.Id.Should().NotBeNullOrWhiteSpace();
            employee.Email.Should().Be(email);
            employee.FirstName.Should().Be(firstName);
            employee.LastName.Should().Be(lastName);
            employee.Reviews.Should().NotBeNull();
        }
    }
}
