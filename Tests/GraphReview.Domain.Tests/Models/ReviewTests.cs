using GraphReview.Domain.Models;

namespace GraphReview.Domain.Tests.Models
{
    public class ReviewTests
    {
        [Fact]
        public void GivenCorrectParamaters_WhenConstructorIsInvoked_AReviewIsCreated()
        {
            // Arrange
            var reviewerId = Guid.NewGuid().ToString();
            var revieweeId = Guid.NewGuid().ToString();
            var startTime = DateTime.UtcNow;
            var duration = 60;

            // Act
            var review = new Review(reviewerId, revieweeId, startTime, duration);

            // Assert
            review.Should().NotBeNull();
            review.Id.Should().NotBeNullOrWhiteSpace();
            review.ReviewerId.Should().Be(reviewerId);
            review.RevieweeId.Should().Be(revieweeId);
            review.StartTime.Should().Be(startTime);
            review.Duration.Should().Be(duration);
            review.EndTime.Should().Be(review.StartTime.AddMinutes(review.Duration));
        }
    }
}
