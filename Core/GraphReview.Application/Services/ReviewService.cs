using GraphReview.Application.Abstractions.Reviews;
using GraphReview.Domain.Exceptions;
using GraphReview.Domain.Models;
using GraphReview.Domain.UnitOfWork;

namespace GraphReview.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAsync(Review review, CancellationToken cancellationToken = default)
        {
            review.Id = Guid.NewGuid().ToString();

            await _unitOfWork.ReviewRepository
                .AddAsync(review, cancellationToken);

            await _unitOfWork
                .SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var review = await _unitOfWork.ReviewRepository
                .GetByIdAsync(id, cancellationToken) ??
                throw new ReviewNotFoundException("Review not found!");

            _unitOfWork.ReviewRepository
                .Delete(review);

            await _unitOfWork
                .SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ReviewRepository
                .GetAllAsync(cancellationToken);
        }

        public async Task<Review> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ReviewRepository
                .GetByIdAsync(id, cancellationToken) ??
                throw new ReviewNotFoundException("Review not found!");
        }
    }
}
