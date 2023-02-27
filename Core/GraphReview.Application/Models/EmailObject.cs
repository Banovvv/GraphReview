using Ardalis.GuardClauses;

namespace GraphReview.Application.Models
{
    public class EmailObject
    {
        public EmailObject(
            string from,
            string replyTo,
            string subject,
            string body,
            IEnumerable<string> recipients)
        {
            From = Guard.Against.NullOrWhiteSpace(from);
            ReplyTo = Guard.Against.NullOrWhiteSpace(replyTo);
            Subject = Guard.Against.NullOrWhiteSpace(subject);
            Body = Guard.Against.NullOrWhiteSpace(body);
            Recipients = Guard.Against.NullOrEmpty(recipients);
        }

        public string From { get; set; }
        public string ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Recipients { get; set; }
    }
}
