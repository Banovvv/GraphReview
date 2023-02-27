using GraphReview.Application.Models;
using Microsoft.Graph;

namespace GraphReview.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task<Event> ScheduleEventAsync();

        Task<Message> SendEmailAsync(EmailObject emailObject);
    }
}
