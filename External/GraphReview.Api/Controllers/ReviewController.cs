using GraphReview.Application.Abstractions.Email;
using GraphReview.Application.Abstractions.Reviews;
using GraphReview.Contracts.Review;
using Microsoft.AspNetCore.Mvc;

namespace GraphReview.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService, IEmailService emailService)
        {
            _emailService = emailService;
            _reviewService = reviewService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetAllAsync()
        {
            var reviews = await _reviewService.GetAllAsync();

            var response = reviews
                .Select(x => new ReviewResponse()
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime
                });

            return Ok(response);
        }
    }
}
