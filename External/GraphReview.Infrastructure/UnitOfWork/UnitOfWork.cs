using GraphReview.Domain.Repositories;
using GraphReview.Domain.UnitOfWork;
using GraphReview.Infrastructure.Data;
using GraphReview.Infrastructure.Repositories;

namespace GraphReview.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ReviewRepository = new ReviewRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
            DepartmentRepository = new DepartmentRepository(_context);
        }

        public IReviewRepository ReviewRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }

        public IDepartmentRepository DepartmentRepository { get; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            disposed = true;
        }
    }
}
