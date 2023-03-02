using AutoFixture;
using GraphReview.Application.Services;
using GraphReview.Application.Tests.Helpers;
using GraphReview.Domain.Models;
using GraphReview.Domain.Repositories;
using GraphReview.Domain.UnitOfWork;
using Moq;

namespace GraphReview.Application.Tests.Services
{
    public class ReviewServiceTests
    {
        private readonly IFixture _fixture;
        private ReviewService _reviewService;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IReviewRepository> _reviewRepository;

        public ReviewServiceTests()
        {
            _fixture = TestHelper.SetupFixture();

            _reviewRepository = _fixture.Freeze<Mock<IReviewRepository>>();

            _unitOfWork = _fixture.Freeze<Mock<IUnitOfWork>>();
            _unitOfWork.Setup(x => x.ReviewRepository)
                .Returns(_reviewRepository.Object);

            _reviewService = new ReviewService(_unitOfWork.Object);
        }

        [Fact]
        public async Task GivenValidId_WhenGetByIdAsyncIsInvoked_ThenEntityIsReturned()
        {
            // Arrange
            var review = _fixture.Build<Review>().Create();
            _unitOfWork.Setup(x=>x.ReviewRepository.GetByIdAsync(review.Id, default)).ReturnsAsync(review);

            // Act
            var result = await _reviewService.GetByIdAsync(review.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(review.Id);
        }
    }
}
