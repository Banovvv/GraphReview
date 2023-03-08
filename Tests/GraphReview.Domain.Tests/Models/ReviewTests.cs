using AutoFixture;
using GraphReview.Application.Tests.Helpers;
using GraphReview.Domain.Models;

namespace GraphReview.Domain.Tests.Models
{
    public class ReviewTests
    {
        private readonly IFixture _fixture;
        public ReviewTests()
        {
            _fixture = TestHelper.SetupFixture();
        }

        [Fact]
        public void GivenCorrectParamaters_WhenConstructorIsInvoked_AReviewIsCreated()
        {
            // Arrange
            var attendees = _fixture.Build<Employee>().CreateMany(2);
            var startTime = DateTime.UtcNow;
            var duration = 60;

            // Act
            var review = new Review(startTime, duration)
            {
                Attendees = attendees.ToList()
            };

            // Assert
            review.Should().NotBeNull();
            review.Id.Should().NotBeNullOrWhiteSpace();
            review.Attendees.Should().NotBeEmpty();
            review.Attendees.Should().HaveCount(attendees.Count());
            review.StartTime.Should().Be(startTime);
            review.Duration.Should().Be(duration);
            review.EndTime.Should().Be(review.StartTime.AddMinutes(review.Duration));
        }
    }
}
