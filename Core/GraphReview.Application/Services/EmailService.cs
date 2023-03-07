using Azure.Identity;
using GraphReview.Application.Abstractions.Email;
using GraphReview.Application.Models;
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
                _configuration["MicrosofrGraph:ClientSecret"],
                options);

            _graphClient = new GraphServiceClient(
                token,
                new[] { "https://graph.microsoft.com/.default" });
        }

        public Task<Event> ScheduleEventAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Message> SendEmailAsync(EmailObject emailObject)
        {
            var message = new Message();

            if (!string.IsNullOrEmpty(emailObject.From))
            {
                message.From = new Recipient()
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = emailObject.From,
                    }
                };
            }

            if (!string.IsNullOrEmpty(emailObject.ReplyTo))
            {
                message.ReplyTo = new List<Recipient>()
                {
                    new Recipient()
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = emailObject.ReplyTo,
                        }
                    }
                };
            }

            message.Subject = emailObject.Subject ?? string.Empty;

            message.Body = new ItemBody
            {
                ContentType = BodyType.Html,
                Content = emailObject.Body ?? string.Empty
            };

            if (emailObject.Recipients != null)
            {
                var recepients = new List<Recipient>();

                foreach (string recipient in emailObject.Recipients)
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
            }

            await _graphClient
                .Users[emailObject.From]
                .SendMail(message, true)
                .Request()
                .PostAsync();

            return message;
        }
    }
}
