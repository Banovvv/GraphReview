using GraphReview.Application.Abstractions.Reviews;
using GraphReview.Domain.Models;

namespace GraphReview.Application.Services
{
    public class ReviewService : IReviewService
    {
        public Task<bool> AddAsync(Review review, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
