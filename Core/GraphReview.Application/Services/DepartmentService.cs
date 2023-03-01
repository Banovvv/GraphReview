using GraphReview.Application.Abstractions.Departments;
using GraphReview.Domain.Exceptions;
using GraphReview.Domain.Models;
using GraphReview.Domain.UnitOfWork;

namespace GraphReview.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> AddAsync(Department department, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Delete(Department department)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.DepartmentRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Department?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.DepartmentRepository.GetByIdAsync(id, cancellationToken) ??
                throw new DepartmentNotFoundException("Department not found!");
        }
    }
}
