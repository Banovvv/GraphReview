namespace GraphReview.Domain.Exceptions.Base
{
    public class BaseCustomException : Exception
    {
        public BaseCustomException(string message)
            : base(message)
        {
        }

        public int ErrorCode { get; set; }

        public override string ToString()
        {
            return $"{ErrorCode}-{GetType().Name}: {Message}";
        }
    }
}
