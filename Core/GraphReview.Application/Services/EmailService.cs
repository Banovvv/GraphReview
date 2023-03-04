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

        public Task<Message> SendEmailAsync(EmailObject emailObject)
        {
            throw new NotImplementedException();
        }
    }
}
