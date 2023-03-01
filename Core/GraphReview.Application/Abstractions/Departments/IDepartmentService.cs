using GraphReview.Domain.Models;

namespace GraphReview.Application.Abstractions.Departments
{
    public interface IDepartmentService
    {
        Task<Department> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> AddAsync(Department department, CancellationToken cancellationToken = default);
        void Delete(Department department);
    }
}
