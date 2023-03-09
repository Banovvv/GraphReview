using GraphReview.Application.Abstractions.Email;
using GraphReview.Application.Abstractions.Employees;
using GraphReview.Application.Abstractions.Reviews;
using GraphReview.Application.Constants;
using GraphReview.Application.Models;
using GraphReview.Domain.Exceptions;
using GraphReview.Domain.Models;
using GraphReview.Domain.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace GraphReview.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly string _defaultSender;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeService;

        public ReviewService(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IConfiguration configuration,
            IEmployeeService employeeService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _configuration = configuration;
            _employeeService = employeeService;

            _defaultSender = _configuration["MicrosofrGraph:DefaultSender"] ?? string.Empty;
        }

        public async Task<bool> AddAsync(List<string> attendeeIds, DateTime startTime, int duration, CancellationToken cancellationToken = default)
        {
            var review = new Review(startTime, duration)
            {
                Id = Guid.NewGuid().ToString()
            };

            foreach (var id in attendeeIds)
            {
                var employee = await _employeeService.GetByIdAsync(id, cancellationToken);

                var email = new EmailObject(
                    _defaultSender,
                    _defaultSender,
                    string.Empty,
                    string.Empty,
                    new List<string>() { employee.Email });

                await _emailService.SendEmailAsync(email);

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
