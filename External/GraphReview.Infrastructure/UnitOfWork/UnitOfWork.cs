using GraphReview.Domain.Repositories;
using GraphReview.Domain.UnitOfWork;

namespace GraphReview.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
        }

        public IReviewRepository ReviewRepository => throw new NotImplementedException();

        public IEmployeeRepository EmployeeRepository => throw new NotImplementedException();

        public IDepartmentRepository DepartmentRepository => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
