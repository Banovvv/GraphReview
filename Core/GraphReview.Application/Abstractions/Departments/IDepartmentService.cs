using GraphReview.Domain.Models;
using System.Security.Cryptography.X509Certificates;

namespace GraphReview.Application.Abstractions.Departments
{
    public interface IDepartmentService
    {
        Task<Department?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> AddAsync(Department department, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Department> AddEmployeeAsync(string departmentId, string employeeId, CancellationToken cancellationToken = default);
    }
}
