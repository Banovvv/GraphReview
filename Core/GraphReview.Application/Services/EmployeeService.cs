using GraphReview.Application.Abstractions.Employees;
using GraphReview.Domain.Exceptions;
using GraphReview.Domain.Models;
using GraphReview.Domain.UnitOfWork;

namespace GraphReview.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> AddAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Delete(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAllByDepartmentAsync(string departmentId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.EmployeeRepository.GetByIdAsync(id, cancellationToken) ??
                throw new EmployeeNotFoundException("Employee not found!");
        }
    }
}
