using GraphReview.Domain.Models;

namespace GraphReview.Domain.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllByDepartmentAsync(string departmentId, CancellationToken cancellationToken = default);
    }
}
