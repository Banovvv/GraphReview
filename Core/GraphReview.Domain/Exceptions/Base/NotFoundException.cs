namespace GraphReview.Domain.Exceptions.Base
{
    public abstract class NotFoundException : BaseCustomException
    {
        public NotFoundException(string message)
            : base(message)
        {
            ErrorCode = 404;
        }
    }
}
