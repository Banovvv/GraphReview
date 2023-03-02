using GraphReview.Domain.Models;

namespace GraphReview.Application.Abstractions.Reviews
{
    public interface IReviewService
    {
        Task<Review> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> AddAsync(Review review, CancellationToken cancellationToken = default);
        Task Delete(string id, CancellationToken cancellationToken = default);
    }
}
