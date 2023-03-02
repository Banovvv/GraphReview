using GraphReview.Application.Abstractions.Email;
using GraphReview.Application.Models;
using Microsoft.Graph;

namespace GraphReview.Application.Services
{
    public class EmailService : IEmailService
    {
        public Task<Event> ScheduleEventAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendEmailAsync(EmailObject emailObject)
        {
            throw new NotImplementedException();
        }
    }
}
