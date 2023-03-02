using GraphReview.Domain.Repositories;

namespace GraphReview.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IReviewRepository ReviewRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
