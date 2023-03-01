using GraphReview.Domain.Exceptions.Base;

namespace GraphReview.Domain.Exceptions
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(string message)
            : base(message)
        {
        }
    }
}
