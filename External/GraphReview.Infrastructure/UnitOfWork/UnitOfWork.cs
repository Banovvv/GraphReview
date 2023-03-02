﻿using GraphReview.Domain.Repositories;
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
            EmployeeRepository = new EmployeeRepository(_context);
        }

        public IReviewRepository ReviewRepository => throw new NotImplementedException();

        public IEmployeeRepository EmployeeRepository { get; }

        public IDepartmentRepository DepartmentRepository => throw new NotImplementedException();

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
