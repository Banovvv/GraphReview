using GraphReview.Domain.Models;

namespace GraphReview.Application.Abstractions.Employees
{
    public interface IEmployeeService
    {
        Task<Employee> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> GetAllByDepartmentAsync(string departmentId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> AddAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
