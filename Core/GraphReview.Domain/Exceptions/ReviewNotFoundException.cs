using GraphReview.Domain.Exceptions.Base;

namespace GraphReview.Domain.Exceptions
{
    public class ReviewNotFoundException : NotFoundException
    {
        public ReviewNotFoundException(string message)
            : base(message)
        {
        }
    }
}
