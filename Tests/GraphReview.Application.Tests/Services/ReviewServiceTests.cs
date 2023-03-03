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
    public class ReviewServiceTests
    {
        private readonly IFixture _fixture;
        private readonly ReviewService _reviewService;
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
            _unitOfWork.Setup(x => x.ReviewRepository.GetByIdAsync(review.Id, default)).ReturnsAsync(review);

            // Act
            var result = await _reviewService.GetByIdAsync(review.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(review.Id);
        }

        [Fact]
        public async Task GivenInvalidId_WhenGetByIdAsyncIsInvoked_ThenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            _unitOfWork.Setup(x => x.ReviewRepository.GetByIdAsync(id, default)).ReturnsAsync((Review)null);

            // Act
            Func<Task> act = async () => await _reviewService.GetByIdAsync(id);

            // Assert
            await act.Should()
                .ThrowAsync<ReviewNotFoundException>()
                .WithMessage(string.Format(ValidationMessages.ReviewNotFound, id));
        }

        [Fact]
        public async Task WhenGetAllAsyncIsInvoked_ThenFullListOfEmployeesIsReturned()
        {
            // Arrange
            var reviews = _fixture.Build<Review>().CreateMany(4);
            _unitOfWork.Setup(x => x.ReviewRepository.GetAllAsync(default)).ReturnsAsync(reviews);

            // Act
            var result = await _reviewService.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(4);
        }

        [Fact]
        public async Task GivenValidId_WhenDeleteAsyncIsInvoked_ThenEntityIsDeleted()
        {
            // Arrange
            var review = _fixture.Build<Review>().Create();
            _unitOfWork.Setup(x => x.ReviewRepository.GetByIdAsync(review.Id, default)).ReturnsAsync(review);

            // Act
            var result = await _reviewService.DeleteAsync(review.Id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GivenInvalidId_WhenDeleteAsyncIsInvoked_ThenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            _unitOfWork.Setup(x => x.ReviewRepository.GetByIdAsync(id, default)).ReturnsAsync((Review)null);

            // Act
            Func<Task> act = async () => await _reviewService.DeleteAsync(id);

            // Assert
            await act.Should()
                .ThrowAsync<ReviewNotFoundException>()
                .WithMessage(string.Format(ValidationMessages.ReviewNotFound, id));
        }

        [Fact]
        public async Task GivenValidEntity_WhenAddAsyncIsInvoked_ThenEntityIsAdded()
        {
            // Arrange
            var review = _fixture.Build<Review>().Create();

            // Act
            var result = await _reviewService.AddAsync(review);

            // Assert
            result.Should().BeTrue();
        }
    }
}
