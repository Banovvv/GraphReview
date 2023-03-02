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

        public async Task<bool> AddAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            employee.Id = Guid.NewGuid().ToString();

            await _unitOfWork.EmployeeRepository
                .AddAsync(employee, cancellationToken);

            await _unitOfWork
                .SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var employee = await _unitOfWork.EmployeeRepository
                .GetByIdAsync(id, cancellationToken) ??
                throw new EmployeeNotFoundException("Employee not found!"); ;

            _unitOfWork.EmployeeRepository
                .Delete(employee);

            await _unitOfWork
                .SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.EmployeeRepository
                .GetAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetAllByDepartmentAsync(string departmentId, CancellationToken cancellationToken = default)
        {
            var deparment = await _unitOfWork.DepartmentRepository
                .GetByIdAsync(departmentId, cancellationToken) ??
                throw new DepartmentNotFoundException("Department not found!");

            return await _unitOfWork.EmployeeRepository
                .GetAllByDepartmentAsync(departmentId, cancellationToken);
        }

        public async Task<Employee> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.EmployeeRepository
                .GetByIdAsync(id, cancellationToken) ??
                throw new EmployeeNotFoundException("Employee not found!");
        }
    }
}
