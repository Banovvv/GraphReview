using Ardalis.GuardClauses;
using Azure.Identity;
using GraphReview.Application.Abstractions.Email;
using GraphReview.Application.Constants;
using GraphReview.Application.Models;
using GraphReview.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;

namespace GraphReview.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly GraphServiceClient _graphClient;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;

            var options = new TokenCredentialOptions()
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var token = new ClientSecretCredential(
                _configuration["MicrosofrGraph:TenantId"],
                _configuration["MicrosofrGraph:ClientId"],
                _configuration["MicrosofrGraph:ClientSecret"]);

            _graphClient = new GraphServiceClient(
                token,
                new[] { "https://graph.microsoft.com/.default" });
        }

        public async Task<Event> ScheduleEventAsync(Review review)
        {
            var meeting = new Event()
            {
                Subject = EmailConstants.EmailSubject,

                Start = new DateTimeTimeZone
                {
                    DateTime = review.StartTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    TimeZone = "UTC"
                },

                End = new DateTimeTimeZone
                {
                    DateTime = review.EndTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    TimeZone = "UTC"
                },

                Attendees = new List<Attendee>(),
            };

            var attendees = new List<Attendee>();

            foreach (var attendee in review.Attendees)
            {
                attendees.Add(new Attendee()
                {
                    Type = AttendeeType.Required,

                    EmailAddress = new EmailAddress
                    {
                        Address = Guard.Against.NullOrWhiteSpace(attendee.Email),
                    }
                });
            }

            meeting.Attendees = attendees.ToList();

            var organizer = review.Attendees
                .First()
                .Email;

            await _graphClient
                    .Users[organizer]
                    .Events
                    .Request()
                    .AddResponseAsync(meeting);

            return meeting;
        }

        public async Task<Message> SendEmailAsync(EmailObject emailObject)
        {
            ArgumentNullException.ThrowIfNull(emailObject, nameof(emailObject));

            var message = new Message
            {
                From = new Recipient()
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = Guard.Against.NullOrWhiteSpace(emailObject.From),
                    }
                },

                ReplyTo = new List<Recipient>()
                {
                    new Recipient()
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = Guard.Against.NullOrWhiteSpace(emailObject.ReplyTo),
                        }
                    }
                },

                Subject = Guard.Against.NullOrWhiteSpace(emailObject.Subject),

                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = Guard.Against.NullOrWhiteSpace(emailObject.Body)
                }
            };

            var recepients = new List<Recipient>();

            foreach (string recipient in Guard.Against.NullOrEmpty(emailObject.Recipients))
            {
                recepients.Add(new Recipient()
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = recipient,
                    }
                });
            }

            message.ToRecipients = new List<Recipient>(recepients);

            await _graphClient
                .Users[emailObject.From]
                .SendMail(message, true)
                .Request()
                .PostAsync();

            return message;
        }
    }
}
