using GraphReview.Application.Models;
using GraphReview.Domain.Models;
using Microsoft.Graph;

namespace GraphReview.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task<Event> ScheduleEventAsync(Review review);

        Task<Message> SendEmailAsync(EmailObject emailObject);
    }
}
