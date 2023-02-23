using GraphReview.Application.Models;

namespace GraphReview.Application.Tests.Models
{
    public class EmailObjectTests
    {
        [Fact]
        public void WithValidParameters_WhenConstructorIsInvoked_ThenObjectIsCreated()
        {
            // Arrange
            var from = "a@contoso.com";
            var replyTo = "a@contoso.com";
            var subject = "Meeting";
            var body = "Awesome body";
            var recepients = new List<string>
            {
                "b@contoso.com",
                "c@contoso.com"
            };

            // Act
            var email = new EmailObject(from, replyTo, subject, body, recepients);

            // Assert
            email.Should().NotBeNull();
            email.From.Should().Be(from);
            email.ReplyTo.Should().Be(replyTo);
            email.Subject.Should().Be(subject);
            email.Body.Should().Be(body);
            email.Recipients.Should().NotBeNull();
            email.Recipients.Should().NotBeEmpty();
            email.Recipients.Count.Should().Be(recepients.Count);
            email.Recipients.Should().BeEquivalentTo(recepients);
        }
    }
}
