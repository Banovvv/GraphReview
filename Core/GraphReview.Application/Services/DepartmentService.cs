using GraphReview.Application.Abstractions.Departments;
using GraphReview.Application.Constants;
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

        public async Task<bool> AddAsync(Department department, CancellationToken cancellationToken = default)
        {
            department.Id = Guid.NewGuid().ToString();

            await _unitOfWork.DepartmentRepository
                .AddAsync(department, cancellationToken);

            await _unitOfWork
                .SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<Department> AddEmployeeAsync(string departmentId, string employeeId, CancellationToken cancellationToken = default)
        {
            var department = await _unitOfWork.DepartmentRepository
                .GetByIdAsync(departmentId, cancellationToken) ??
                throw new DepartmentNotFoundException(string.Format(ValidationMessages.DepartmentNotFound, departmentId));

            var employee = await _unitOfWork.EmployeeRepository
                .GetByIdAsync(employeeId, cancellationToken) ??
                throw new EmployeeNotFoundException(string.Format(ValidationMessages.EmployeeNotFound, employeeId));

            _unitOfWork.DepartmentRepository.AddEmployee(department, employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return department;
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var department = await _unitOfWork.DepartmentRepository
                .GetByIdAsync(id, cancellationToken) ??
                throw new DepartmentNotFoundException(string.Format(ValidationMessages.DepartmentNotFound, id));

            _unitOfWork.DepartmentRepository
                .Delete(department);

            await _unitOfWork
                .SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.DepartmentRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Department?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.DepartmentRepository.GetByIdAsync(id, cancellationToken) ??
                throw new DepartmentNotFoundException(string.Format(ValidationMessages.DepartmentNotFound, id));
        }
    }
}
