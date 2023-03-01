using GraphReview.Domain.Exceptions.Base;

namespace GraphReview.Domain.Exceptions
{
    public class DepartmentNotFoundException : NotFoundException
    {
        public DepartmentNotFoundException(string message)
            : base(message)
        {
        }
    }
}
