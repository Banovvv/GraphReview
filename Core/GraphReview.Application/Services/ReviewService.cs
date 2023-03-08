using GraphReview.Application.Abstractions.Employees;
using GraphReview.Application.Abstractions.Reviews;
using GraphReview.Application.Constants;
using GraphReview.Domain.Exceptions;
using GraphReview.Domain.Models;
using GraphReview.Domain.UnitOfWork;

namespace GraphReview.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeService _employeeService;

        public ReviewService(IUnitOfWork unitOfWork, IEmployeeService employeeService)
        {
            _unitOfWork = unitOfWork;
            _employeeService = employeeService;
        }

        public async Task<bool> AddAsync(List<string> attendeeIds, DateTime startTime, int duration, CancellationToken cancellationToken = default)
        {

            var review = new Review(startTime, duration)
            {
                Id = Guid.NewGuid().ToString()
            };

            foreach (var id in attendeeIds)
            {
                var employee = await _employeeService.GetByIdAsync(id);
                review.Attendees.Add(employee);
            }

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
                throw new ReviewNotFoundException(string.Format(ValidationMessages.ReviewNotFound, id));

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
                throw new ReviewNotFoundException(string.Format(ValidationMessages.ReviewNotFound, id));
        }
    }
}
