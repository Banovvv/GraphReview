using GraphReview.Domain.Models;

namespace GraphReview.Application.Abstractions.Reviews
{
    public interface IReviewService
    {
        Task<Review> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> AddAsync(List<string> attendeeIds, DateTime startTime, int duration, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
